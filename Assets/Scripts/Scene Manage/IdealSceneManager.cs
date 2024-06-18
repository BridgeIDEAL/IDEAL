using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IdealSceneManager : MonoBehaviour
{
    private static IdealSceneManager instance = null;
    public static IdealSceneManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }
    [SerializeField] private List<GameObject> lobbyObjectList;
    private List<string> lobbyObjectNameList;

    private SceneObjectController prototypeObjectController;
    private SceneObjectController prototype2ObjectController;

    private GameManager prototypeGameManager;
    private GameManager prototype2GameManager;
    private GameManager currentGameManager;

    public GameManager CurrentGameManager{
        get{
            if(currentGameManager == null) return null;
            return currentGameManager;
        }
    }

    [SerializeField] private ActivationLogManager activationLogManager;
    [SerializeField] private HealthPointManager healthPointManager;
    [SerializeField] private MentalPointManager mentalPointManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private EquipmentManager equipmentManager;
    [SerializeField] private PenaltyPointManager penaltyPointManager;
    [SerializeField] private ReformPointManager reformPointManager;
    [SerializeField] private ConversationPointManager conversationPointManager;
    [SerializeField] private AudioSource lobbyBGMBox;
    private float soundFadeTime = 1.4f;
    private float soundInitVolume = 0.0f;
    private float fadeEffectTime = 0.7f;
    private Coroutine loadCoroutine;
    private Coroutine soundCoroutine;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this.gameObject);
        }
        SceneManager.sceneLoaded += AfterSceneLoaded;
        
        activationLogManager.Init();
        healthPointManager.Init();
        mentalPointManager.Init();
        inventory.Init();
        equipmentManager.Init();
        penaltyPointManager.Init();
        reformPointManager.Init();
        conversationPointManager.Init();

        lobbyObjectNameList = new List<string>();
        for(int i = 0 ; i < lobbyObjectList.Count; i++){
            lobbyObjectNameList.Add(lobbyObjectList[i].name);
        }
        soundInitVolume = lobbyBGMBox.volume;
    }


    private void AfterSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "Prototype" || scene.name == "Prototype_Second"){
            Cursor.lockState = CursorLockMode.Locked;
            GuideLogManager.Instance.guideLogUpdated = false;

        }
        else if(scene.name == "Lobby"){
            Cursor.lockState = CursorLockMode.None;

            
            for(int i = 0 ; i < lobbyObjectNameList.Count; i++){
                lobbyObjectList.Add(GameObject.Find(lobbyObjectNameList[i]));
            }

            activationLogManager.EnterAnotherSceneInit(true);
            healthPointManager.EnterAnotherSceneInit(true);
            mentalPointManager.EnterAnotherSceneInit(true);
            inventory.EnterAnotherSceneInit(true);
            equipmentManager.EnterAnotherSceneInit(true);
            penaltyPointManager.EnterAnotherSceneInit(true);
            reformPointManager.EnterAnotherSceneInit(true);
            conversationPointManager.EnterAnotherSceneInit(true);
            LobbyBGMFade(true);
        }
    }

    public void LoadGameScene(){
        // 현재는 프로토타입 Load
        if(loadCoroutine != null){
            StopCoroutine(loadCoroutine);
        }
        loadCoroutine = StartCoroutine(LoadGameSceneCoroutine());
    }

    IEnumerator LoadGameSceneCoroutine(){
        LoadingImageManager.Instance.SetActiveLoadingImage(true);
        LoadingImageManager.Instance.StartIntroText();
        yield return null;

        AsyncOperation loadPrototype = SceneManager.LoadSceneAsync("Prototype", LoadSceneMode.Additive);
        loadPrototype.allowSceneActivation = false;
        AsyncOperation loadPrototype2 = SceneManager.LoadSceneAsync("Prototype_Second", LoadSceneMode.Additive);
        loadPrototype2.allowSceneActivation = false;

        Debug.Log("Load 2 Scenes Start!");
        while(!(loadPrototype.progress >= 0.9f && loadPrototype2.progress >= 0.9f)){
            yield return null;
        }

        Debug.Log("Load 2 Scenes Done!");
        
        loadPrototype.allowSceneActivation = true;
        loadPrototype2.allowSceneActivation = true;
        while(!(loadPrototype.isDone && loadPrototype2.isDone)){
            yield return null;
        }
        for(int i = 0 ; i < lobbyObjectList.Count; i++){
            lobbyObjectList[i].SetActive(false);
        }
        lobbyObjectList = new List<GameObject>();
        
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Prototype_Second"));
        prototype2GameManager = GameObject.Find("GameManager2").GetComponent<GameManager>();
        currentGameManager = prototype2GameManager;
        prototype2GameManager.Init();
        yield return null;
        yield return null;      // UImanager 등 프레임 여유가 필요한 스크립트들이 있음
        prototype2ObjectController = GameObject.Find("SceneObjectController2").GetComponent<SceneObjectController>();
        prototype2ObjectController.SceneObjectsSetActive(false);


        Debug.Log("~~~~~~~~~~~~~~~~~~~Change");

        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Prototype"));
        prototypeGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentGameManager = prototypeGameManager;
        prototypeGameManager.Init();
        ImplementScriptHub(prototypeGameManager);
        prototypeObjectController = GameObject.Find("SceneObjectController").GetComponent<SceneObjectController>();
        
        // 한 프레임 쉬지 않고 바로 IngameFade 호출 시 해당 코루틴이 한 루틴만 돌고 오류남
        yield return null;

        // Scene이 준비되었다고 LoadingImageManager에게 알리기
        LoadingImageManager.Instance.LoadEnded();

        // LoadingImage를 종료하고자 하는지 변수로 체크
        while(true){
            if(LoadingImageManager.Instance.goNext){
                break;
            }
            yield return null;
        }

        prototypeGameManager.scriptHub.uIManager.IngameFadeInEffect();
        prototypeGameManager.scriptHub.ambienceSoundManager.SoundFadeIn(true);
        LobbyBGMFade(false);

        yield return null;
        LoadingImageManager.Instance.SetActiveLoadingImage(false);
        SceneManager.UnloadSceneAsync("Lobby");
    }

    private void ImplementScriptHub(GameManager gameManager){
        activationLogManager.scriptHub = gameManager.scriptHub;
        activationLogManager.EnterAnotherSceneInit(false);
        healthPointManager.scriptHub = gameManager.scriptHub;
        healthPointManager.EnterAnotherSceneInit(false);
        mentalPointManager.scriptHub = gameManager.scriptHub;
        mentalPointManager.EnterAnotherSceneInit(false);
        inventory.scriptHub = gameManager.scriptHub;
        inventory.EnterAnotherSceneInit(false);
        equipmentManager.scriptHub = gameManager.scriptHub;
        equipmentManager.EnterAnotherSceneInit(false);
        penaltyPointManager.scriptHub = gameManager.scriptHub;
        penaltyPointManager.EnterAnotherSceneInit(false);
        reformPointManager.scriptHub = gameManager.scriptHub;
        reformPointManager.EnterAnotherSceneInit(false);
        conversationPointManager.EnterAnotherSceneInit(false);
    }
    

    public void ChangeAnotherGameScene(string currentSceneName, string destSceneName, Vector3 destPosition, Vector3 destRotation){
        if(loadCoroutine != null){
            StopCoroutine(loadCoroutine);
        }
        loadCoroutine = StartCoroutine(ChangeAnotherGameSceneCoroutine(currentSceneName, destSceneName, destPosition, destRotation));
    }

    IEnumerator ChangeAnotherGameSceneCoroutine(string currentSceneName, string destSceneName, Vector3 destPosition, Vector3 destRotation){
        // 화면 fade Out 효과 넣기
        Image fadeFilter = LoadingImageManager.Instance.fadeFilter;
        float stepTimer = 0.0f;
        Color color = fadeFilter.color;
        float startAlpha = 0.0f;
        float endAlpha = 1.0f;
        while(stepTimer <= fadeEffectTime){
            color.a = Mathf.Lerp(startAlpha, endAlpha, stepTimer / fadeEffectTime);
            fadeFilter.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        color.a = endAlpha;
        fadeFilter.color = color;

        // 현재 씬 비활성화 및 다음 씬 활성화
        if(currentSceneName == "Prototype"){
            prototypeObjectController.SceneObjectsSetActive(false);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(destSceneName));
            currentGameManager = prototype2GameManager;
            currentGameManager.scriptHub.thirdPersonController.TelePortPositionRotation(destPosition, destRotation);
            prototype2ObjectController.SceneObjectsSetActive(true);
            ImplementScriptHub(currentGameManager);
            currentGameManager.scriptHub.uIManager.IngameFadeInEffect();
        }
        else if(currentSceneName == "Prototype_Second"){
            prototype2ObjectController.SceneObjectsSetActive(false);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(destSceneName));
            currentGameManager = prototypeGameManager;
            currentGameManager.scriptHub.thirdPersonController.TelePortPositionRotation(destPosition, destRotation);
            prototypeObjectController.SceneObjectsSetActive(true);
            ImplementScriptHub(currentGameManager);
            currentGameManager.scriptHub.uIManager.IngameFadeInEffect();
        }

        // 화면 fade In 효과 넣기
        stepTimer = 0.0f;
        color = fadeFilter.color;
        startAlpha = 1.0f;
        endAlpha = 0.0f;
        while(stepTimer <= fadeEffectTime){
            color.a = Mathf.Lerp(startAlpha, endAlpha, stepTimer / fadeEffectTime);
            fadeFilter.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        color.a = endAlpha;
        fadeFilter.color = color;

    }

    private void LobbyBGMFade(bool isFadeIn){
        if(soundCoroutine != null){
            StopCoroutine(soundCoroutine);
        }
        soundCoroutine = StartCoroutine(LobbyBGMFadeCoroutine(isFadeIn));
    }

    IEnumerator LobbyBGMFadeCoroutine(bool isFadeIn){
        float stepTimer = 0.0f;
        float startVolume = isFadeIn ? 0.0f : soundInitVolume;
        float destVolume = isFadeIn ? soundInitVolume : 0.0f;
        if(isFadeIn){
            lobbyBGMBox.Play();
        }
        while(stepTimer < soundFadeTime){
            lobbyBGMBox.volume = Mathf.Lerp(startVolume, destVolume, stepTimer / soundFadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        lobbyBGMBox.volume = destVolume;
        if(!isFadeIn){
            lobbyBGMBox.Stop();
        }
    }

    public void LoadLobbyScene(){
        SceneManager.LoadScene("Lobby");
    }


    public void ExitGame(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("isPlaying = false");
#else
        Application.Quit();
        Debug.Log("Application.Quit");
#endif
    }
}
