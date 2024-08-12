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



    private GameManager prototypeGameManager;
    private GameManager prototype2GameManager;
    private GameManager currentGameManager;

    public GameManager CurrentGameManager{
        get{
            if(currentGameManager == null) return null;
            return currentGameManager;
        }
    }

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

    private void Start(){
        SceneManager.sceneLoaded += AfterSceneLoaded;
        AfterSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
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

            healthPointManager.EnterAnotherSceneInit(true);
            mentalPointManager.EnterAnotherSceneInit(true);
            inventory.EnterAnotherSceneInit(true);
            equipmentManager.EnterAnotherSceneInit(true);
            penaltyPointManager.EnterAnotherSceneInit(true);
            reformPointManager.EnterAnotherSceneInit(true);
            conversationPointManager.EnterAnotherSceneInit(true);
            LobbyBGMFade(true);

            ProgressManager.Instance.EnterAnotherSceneInit(true);
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

        AsyncOperation loadPrototype = SceneManager.LoadSceneAsync("Prototype", LoadSceneMode.Single);
        loadPrototype.allowSceneActivation = true;

        
        while(!loadPrototype.isDone){
            yield return null;
        }
        
        prototypeGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentGameManager = prototypeGameManager;
        prototypeGameManager.Init();
        ImplementScriptHub(prototypeGameManager);
        
        yield return null;

        // 인트로 텍스트가 나오는 동안 이동 및 UI 조정 불가능하도록 
        currentGameManager.scriptHub.thirdPersonController.MoveLock = true;
        currentGameManager.scriptHub.uIManager.uIInputLock = true;

        yield return null;

        // Scene이 준비되었다고 LoadingImageManager에게 알리기
        LoadingImageManager.Instance.LoadEnded();

        // LoadingImage를 종료하고자 하는지 변수로 체크
        while(!LoadingImageManager.Instance.goNext){
            yield return null;
        }
        
        currentGameManager.scriptHub.thirdPersonController.MoveLock = false;
        currentGameManager.scriptHub.uIManager.uIInputLock = false;

        PenaltyPointManager.Instance.watchIntroEnded = true;
        prototypeGameManager.scriptHub.ambienceSoundManager.SoundFadeIn(true);
        LobbyBGMFade(false);

        yield return null;

        // 화면 fade In 효과 넣기
        Image fadeFilter = LoadingImageManager.Instance.fadeFilter;
        float stepTimer = 0.0f;
        Color color = fadeFilter.color;
        float startAlpha = 1.0f;
        float endAlpha = 0.0f;
        while(stepTimer <= fadeEffectTime){
            color.a = Mathf.Lerp(startAlpha, endAlpha, stepTimer / fadeEffectTime);
            fadeFilter.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        color.a = endAlpha;
        fadeFilter.color = color;
        
        LoadingImageManager.Instance.SetActiveLoadingImage(false);
        EquipmentManager.Instance.EquipPillItem();
        EquipmentManager.Instance.EquipFlashLight();
    }

    private void ImplementScriptHub(GameManager gameManager){
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
        ProgressManager.Instance.EnterAnotherSceneInit(false);
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
            AsyncOperation loadPrototype2 = SceneManager.LoadSceneAsync("Prototype_Second", LoadSceneMode.Single);
            loadPrototype2.allowSceneActivation = true;
            while(!loadPrototype2.isDone){
                yield return null;
            }
            prototype2GameManager = GameObject.Find("GameManager2").GetComponent<GameManager>();
            currentGameManager = prototype2GameManager;
            currentGameManager.Init();
            ImplementScriptHub(currentGameManager);
            currentGameManager.scriptHub.thirdPersonController.TelePortPositionRotation(destPosition, destRotation);
            
        }
        else if(currentSceneName == "Prototype_Second"){
            AsyncOperation loadPrototype = SceneManager.LoadSceneAsync("Prototype", LoadSceneMode.Single);
            loadPrototype.allowSceneActivation = true;
            while(!loadPrototype.isDone){
                yield return null;
            }
            prototypeGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            currentGameManager = prototypeGameManager;
            currentGameManager.Init();
            ImplementScriptHub(currentGameManager);
            currentGameManager.scriptHub.thirdPersonController.TelePortPositionRotation(destPosition, destRotation);
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
