using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingImageManager : MonoBehaviour
{
    private static LoadingImageManager instance;
    public static LoadingImageManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    [SerializeField] private GameObject loadingImageObject;
    public Image fadeFilter;

    [SerializeField] private TextMeshProUGUI introTextTMP;
    [SerializeField] private GameObject introTextLoadedTextObject;
    private Coroutine introTextCoroutine;
    // private Coroutine typingSoundCoroutine;
    private int introTextStep = 0;

    [SerializeField] private GameObject[] IntroImageObjects;
    // [SerializeField] private AudioClip[] typingSounds;
    [SerializeField] private AudioSource bgmAudioSource;
    private float bgmAudioVolume;

    [SerializeField] private AudioSource talkAudioSource;
    [SerializeField] private AudioClip[] talkAudioClips;
    private Dictionary<int, int> introTalkDic = new Dictionary<int, int>{   // intro Index와 talk sound 연결 Dictionary
        {0, 0},
        {1, 1},
        {3, 2},
        {5, 3},
        {6, 4},
        {8, 5},
        {10, 6},
    };

    private string[] introTexts = new string[]{
        "\n<color=#ed2809>Warning: If you see this warning, \nwe recommend that you read it carefully.\n</color>\nCareless behaviors can lead to the following consequences.\n똝\n• minor injury with little bleeding or wound\n• serious life-threatening injuries\n• Death\n• ▊■▆■▊▆■▆■▊■ (■▊■▆▊)똝",
        "\n0. You are at Jain High School. We repeat.\nYou are at Jain High School now.",
        "똝\n0. You are at Jain High School. We repeat.\nYou are at Jain High School now.\n<color=#ed2809><u>(Jain High School was closed due to an unfortunate fire incident).</u></color>똝",
        "1. Jain High School manages students thoroughly.\nAll students are not allowed to leave school until the bell rings.",
        "똝1. Jain High School manages students thoroughly.\nAll students are not allowed to leave school until the bell rings.\n<color=#ed2809><u>If you attempt to leave before dismissal time,\n please remember that Jain High School is a strictly supervised school.</u></color>똝",
        "\n2. To leave the school, you have to ring the school bell from the broadcast room.\nFor more information, press [TAB] to read the Checklist.",
        "\n3. Be sure to check the bulletin board next to the main gate before entering.\nCheck the rules of conduct, which will keep you alive while exploring the school.",
        "\n똝3. Be sure to check the bulletin board next to the main gate before entering.\nCheck the rules of conduct, which will keep you alive while exploring the school.\n<color=#ed2809><u>\n iF YOU IGNORE THIS,\nREMEMBER J■■N HIGH sChoOL ■■■■■■ MA■AG■S sT?dENTS.</u></color>똝",
        "4. If it is deemed impossible to escape, please check inside your pocket.\nThere will be a special pill prepared for the last moment.\n\nWe strongly recommend that you take the pill for your own sake.",
        "똝4. If it is deemed impossible to escape, please check inside your pocket.\nThere will be a special pill prepared for the last moment.\n\nWe strongly recommend that you take the pill for your own sake.\n<color=#ed2809><u>You'll be able to ■■ ■■ much more comfortably.</u></color>똝",
        "\n\nPlease double check if you understood all the instructions\nagain before entering the school.\n\n We ■▊ ■■▆▊ wish you a safe return.",
    };

    // 특수 기능 기호들이 그대로 출력되면 안되므로
    private string[] introSkipTexts = new string[]{
        "\n<color=#ed2809>Warning: If you see this warning, \nwe recommend that you read it carefully.\n</color>\nCareless behaviors can lead to the following consequences.\n\n• minor injury with little bleeding or wound\n• serious life-threatening injuries\n• Death\n• ▊■▆■▊▆■▆■▊■ (■▊■▆▊)",
        "\n0. You are at Jain High School. We repeat.\nYou are at Jain High School now.",
        "\n0. You are at Jain High School. We repeat.\nYou are at Jain High School now.\n<color=#ed2809><u>(Jain High School was closed due to an unfortunate fire incident).</u></color>",
        "1. Jain High School manages students thoroughly.\nAll students are not allowed to leave school until the bell rings.",
        "1. Jain High School manages students thoroughly.\nAll students are not allowed to leave school until the bell rings.\n<color=#ed2809><u>If you attempt to leave before dismissal time,\n please remember that Jain High School is a strictly supervised school.</u></color>",
        "\n2. To leave the school, you have to ring the school bell from the broadcast room.\nFor more information, press [TAB] to read the Checklist.",
        "\n3. Be sure to check the bulletin board next to the main gate before entering.\nCheck the rules of conduct, which will keep you alive while exploring the school.",
        "\n3. Be sure to check the bulletin board next to the main gate before entering.\nCheck the rules of conduct, which will keep you alive while exploring the school.\n<color=#ed2809><u>\n iF YOU IGNORE THIS,\nREMEMBER J■■N HIGH sChoOL ■■■■■■ MA■AG■S sT?dENTS.</u></color>",
        "4. If it is deemed impossible to escape, please check inside your pocket.\nThere will be a special pill prepared for the last moment.\n\nWe strongly recommend that you take the pill for your own sake.",
        "4. If it is deemed impossible to escape, please check inside your pocket.\nThere will be a special pill prepared for the last moment.\n\nWe strongly recommend that you take the pill for your own sake.\n<color=#ed2809><u>You'll be able to ■■ ■■ much more comfortably.</u></color>",
        "\n\nPlease double check if you understood all the instructions\nagain before entering the school.\n\n We ■▊ ■■▆▊ wish you a safe return.",
    };

    private string stopText = "Press any key to Continue";
    private string loadedText = "Press any key to Start the Game.";

    private string loadingText = "Loading Warning Screen";

    private bool loadEnded = false;
    public bool goNext = false;

    private bool skipParagraph = false;
    private void Awake(){
        if(Instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
        introTextLoadedTextObject.SetActive(false);
        bgmAudioVolume = bgmAudioSource.volume;
    }

    private void Update(){
        if(introTextStep >= introTexts.Length && Input.anyKeyDown){
            introTextStep = 0;
            StartCoroutine(bgmAudioFadeOutCoroutine());
            goNext = true;
            loadingImageObject.SetActive(false);
        }
    }

    IEnumerator bgmAudioFadeOutCoroutine(){
        float stepTimer = 0.0f;
        float fadeTime = IdealSceneManager.Instance.soundFadeTime;
        float bgmAudioVol = bgmAudioSource.volume;

        while(stepTimer < fadeTime){
            bgmAudioSource.volume = Mathf.Lerp(bgmAudioVol, 0.0f, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        bgmAudioSource.Stop();
    }

    public void SetActiveLoadingImage(bool active){
        loadingImageObject.SetActive(active);
    }

    public void LoadEnded(){
        loadEnded = true;
    }


    public void StartIntroText(){
        if(introTextCoroutine != null){
            StopCoroutine(introTextCoroutine);
        }
        StartCoroutine(StartIntroTextCoroutine());
    }

    IEnumerator StartIntroTextCoroutine(){
        loadEnded = false;
        goNext = false;
        introTextStep = 0;
        introTextTMP.text = "";
        introTextLoadedTextObject.SetActive(false);
        
        // 로딩이 완료되기 전 로딩 중이라는 문구 출력
        introTextLoadedTextObject.GetComponent<TextMeshProUGUI>().text = loadingText;
        introTextLoadedTextObject.SetActive(true);

        // 로딩이 완료 되었을 때 타이핑 시작
        // 그렇지 않을 경우 타이핑 되다가 중간에 멈추는 현상 발생
        while(!loadEnded){
            // 로딩이 끝났는지 체크
            yield return null;
        }
        introTextLoadedTextObject.SetActive(false);

        IdealSceneManager.Instance.LobbyBGMFade(false);
        float stepTimer = 0.0f;
        float fadeTime = IdealSceneManager.Instance.soundFadeTime;

        bgmAudioSource.volume = 0.0f;
        bgmAudioSource.Play();
        while(stepTimer < fadeTime){
            bgmAudioSource.volume = Mathf.Lerp(0.0f, bgmAudioVolume, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        
        
        skipParagraph = false;
        while (introTextStep < introTexts.Length){
            // if(typingSoundCoroutine != null){
            //     StopCoroutine(typingSoundCoroutine);
            // }
            // typingSoundCoroutine = StartCoroutine(PlayTypingSounds());
            
            foreach(GameObject imageObject in IntroImageObjects){
                if(imageObject != null){
                    imageObject.SetActive(false);
                }
            }

            if(introTalkDic.ContainsKey(introTextStep) == true){
                talkAudioSource.clip = talkAudioClips[introTalkDic[introTextStep]];
                talkAudioSource.Play();
            }

            yield return StartCoroutine(TypeText(introTexts[introTextStep], introTextStep));
            // StopCoroutine(typingSoundCoroutine);

            
            if(IntroImageObjects[introTextStep] != null){
                IntroImageObjects[introTextStep].SetActive(true);
            }

            yield return new WaitForSeconds(0.1f);

            introTextStep++;

            if(introTextStep == introTexts.Length){
                introTextLoadedTextObject.GetComponent<TextMeshProUGUI>().text = loadedText;
                introTextLoadedTextObject.SetActive(true);
            }
            else{
                // 다음 텍스트로 넘길지 대기
                introTextLoadedTextObject.GetComponent<TextMeshProUGUI>().text = stopText;
                introTextLoadedTextObject.SetActive(true);
                while(true){
                    if(Input.anyKeyDown){
                        Input.ResetInputAxes();
                        break;
                    }
                    yield return null;
                }
                skipParagraph = false;
                introTextTMP.text = "";
                introTextLoadedTextObject.SetActive(false);
            }
        }
        yield return null;
    }

    IEnumerator TypeText(string text, int index){
        string currentText = introTextTMP.text;
        int cnt = 0;
        bool skipLetter = false;
        char previousLetter = '\0';
        bool artificialSkip = false;
        foreach (char letter in text.ToCharArray()){
            if(letter == '똝'){
                artificialSkip = !artificialSkip;
                continue;
            }

            if(letter == '<'){
                skipLetter = true;
                if(previousLetter == '<'){
                    skipLetter = false;
                    continue;
                }
            }
            if(skipLetter){
                if(letter == '>'){
                    skipLetter = false;
                }
            }
            // 키 입력을 받아서 첫 인트로가 아닌 경우 아무 키나 누르면 스킵됨
            // 10글자 넘어야 스킵이 되도록 하여 너무 연달아 스킵 되지 않도록 함
            // 처음에는 문단 단위 스킵 이후에는 페이지 단위 스킵
            if(Input.anyKey && CountAttempts.Instance.GetAttemptCount() > 1){
                skipParagraph = true;
            }
            
            if(skipParagraph){
                talkAudioSource.Stop();
                introTextTMP.text = currentText + introSkipTexts[index];
                break;
            }
            introTextTMP.text += letter;
            cnt++;
            previousLetter = letter;
            if(!skipLetter && !artificialSkip){
                yield return new WaitForSeconds(0.06f); // 타이핑 속도 조절
            }
        }
        skipParagraph = false;
        introTextTMP.text += " \n\n";
    }

    // IEnumerator PlayTypingSounds(){
    //     while (true){
    //         if (!audioSource.isPlaying){
    //             int index = Random.Range(0, typingSounds.Length);
    //             audioSource.PlayOneShot(typingSounds[index]);
    //             yield return new WaitForSeconds(typingSounds[index].length); // 사운드가 재생되는 동안 대기
    //         }
    //         yield return null;
    //     }
    // }

}
