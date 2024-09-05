using System.Collections;
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
    private Coroutine typingSoundCoroutine;
    private int introTextStep = 0;

    [SerializeField] private AudioClip[] typingSounds;
    [SerializeField] private AudioSource audioSource;

    private string[] introTexts = new string[]{
        "<<경고: 이 화면을 함부로 넘기지 마시오.>\n<<해당 경고문을 무시할 경우, 다음의 결과들을 불러올 수 있음.>똝\n\t-\t경미한 부상과 출혈\n\t-\t가벼운 환각 및 판단력 저하\n\t-\t신체 일부 소실\n\t-\t■■■ ■■■ ■■ ■■■■ (■■■■ ■■■■■)\n\t-\t사망 혹은 행방불명똝",
        "0. 당신은 자인고등학교에 있습니다. 반복합니다. 당신이 어디에 있었건, 당신은 <color=#ed2809>지금</color> 자인고등학교에 있습니다.",
        "1. 자인고등학교는 20XX년, 불의의 화재 사고로 폐교되었습니다.\n건물은 전소되었으며, 현실의 자인고등학교는 현재 존재하지 않습니다.\n반복합니다. 자인고등학교는 <color=#ed2809>존재하지 않습니다.</color>",
        "2. 현재 알려진 <color=#ed2809>유일한 탈출 방법</color>은, 4층의 방송실에서 하교종을 재생한 뒤 정문으로 하교하는 것입니다.\n이외의 방법으로 탈출을 시도하실 경우, 저희는 결과를 책임져드리지 않습니다.",
        "3. 만약 탈출이 <color=#ed2809>불가능</color>하다고 판단될 경우, 당신의 주머니 속을 확인하십시오.\n알약 하나가 들어있을 것이며, 저희는 해당 알약을 반드시 복용하는 것을 추천드립니다.\n훨씬 더 편안하게 생을 마감할 수 있을 것입니다.",
        "탈출을 위해 아래의 내용을 반드시 숙지하십시오",
        "1. 반드시 입장 전, 신체가 모두 정상이며 잘 움직여지는지 확인하십시오.\n<color=#ed2809>[WASD]</color>로 몸을 움직여보고, <color=#ed2809>[Mouse]</color>로 이곳저곳 둘러보십시오.",
        "2. 학교 내부에 입장하기 전, 반드시 정문 옆의 <color=#ed2809>게시판</color>을 꼼꼼히 확인하십시오.\n저희는 학교 내부 진입이 불가능하며, 게시판을 통해서만 도움을 드릴 수 있습니다.\n반복합니다. 현실에는 자인고등학교는 존재하지 않습니다.",
        "3. 당신이 확인하실 수 있는 정보는 세 가지입니다.\n<color=#ed2809>[TAB]</color>으로 다음 행동, 신체 상태, 그리고 획득하신 물건을 확인하십시오.",
        "내용을 <color=#ed2809>충분히 숙지</color>하셨다면, 학교 내부로 진입하시면 됩니다.\n저희 ■■■■ ■■■■■■은 당신의 무사 귀환을 기원합니다."
    };

    private string[] introTextsTemp = new string[]{
        "<경고: 이 화면을 함부로 넘기지 마시오.>\n<해당 경고문을 무시할 경우, 다음의 결과들을 불러올 수 있음.>\n\t-\t경미한 부상과 출혈\n\t-\t가벼운 환각 및 판단력 저하\n\t-\t신체 일부 소실\n\t-\t■■■ ■■■ ■■ ■■■■ (■■■■ ■■■■■)\n\t-\t사망 혹은 행방불명",
    };

    private string stopText = "아무 키나 눌러 경고문 이어서 보기";
    private string loadedText = "아무 키나 눌러 게임 시작하기";

    private string loadingText = "경고문 불러오는 중";

    private bool loadEnded = false;
    public bool goNext = false;

    private bool skipPage = false;
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
    }

    private void Update(){
        if(introTextStep >= introTexts.Length && Input.anyKeyDown){
            introTextStep = 0;
            goNext = true;
            loadingImageObject.SetActive(false);
        }
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
        
        skipPage = false;
        skipParagraph = false;
        while (introTextStep < introTexts.Length){
            if(typingSoundCoroutine != null){
                StopCoroutine(typingSoundCoroutine);
            }
            typingSoundCoroutine = StartCoroutine(PlayTypingSounds());
            yield return StartCoroutine(TypeText(introTexts[introTextStep], introTextStep));
            StopCoroutine(typingSoundCoroutine);

            if(!skipPage)yield return new WaitForSeconds(1f); // 다음 텍스트로 넘어가기 전 대기 시간
            introTextStep++;

            if(introTextStep == 5){
                // 다음 텍스트로 넘길지 대기
                introTextLoadedTextObject.GetComponent<TextMeshProUGUI>().text = stopText;
                introTextLoadedTextObject.SetActive(true);
                while(true){
                    if(Input.anyKeyDown) break;
                    yield return null;
                }
                skipPage = false;
                skipParagraph = false;
                introTextTMP.text = "";
                introTextLoadedTextObject.SetActive(false);
            }
            else if(introTextStep == introTexts.Length){
                introTextLoadedTextObject.GetComponent<TextMeshProUGUI>().text = loadedText;
                introTextLoadedTextObject.SetActive(true);
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
            if(Input.anyKey && cnt > 10 && CountAttempts.Instance.GetAttemptCount() > 1){
                skipPage = true;
            }
            else if(Input.anyKey && cnt > 10){
                skipParagraph = true;
            }
            
            if(skipPage || skipParagraph){ 
                audioSource.Stop();
                introTextTMP.text = currentText + text;
                if(index == 0){
                    introTextTMP.text = currentText + introTextsTemp[0];
                }
                break;
            }
            introTextTMP.text += letter;
            cnt++;
            previousLetter = letter;
            if(!skipLetter && !artificialSkip){
                yield return new WaitForSeconds(0.1f); // 타이핑 속도 조절
            }
        }
        skipParagraph = false;
        introTextTMP.text += " \n\n";
    }

    IEnumerator PlayTypingSounds(){
        while (true){
            if (!audioSource.isPlaying){
                int index = Random.Range(0, typingSounds.Length);
                audioSource.PlayOneShot(typingSounds[index]);
                yield return new WaitForSeconds(typingSounds[index].length); // 사운드가 재생되는 동안 대기
            }
            yield return null;
        }
    }

}
