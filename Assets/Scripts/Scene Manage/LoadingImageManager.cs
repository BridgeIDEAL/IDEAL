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

    [SerializeField] private GameObject[] IntroImageObjects;
    [SerializeField] private AudioClip[] typingSounds;
    [SerializeField] private AudioSource audioSource;

    private string[] introTexts = new string[]{
        "\n<color=#ed2809><<경고: 해당 경고문이 보이신다면 꼼꼼히 읽을 것을 권고합니다.>\n</color>\n<<부주의로 인한 행동은 다음의 결과들을 불러올 수 있습니다.>\n똝\n• 적은 출혈이나 상처를 동반한 경미한 부상\n• 생명을 위협할만한 심각한 부상\n• 사망\n• ■■■■■■■■■■■ (■■■■■)똝",
        "\n0. 당신은 자인고등학교에 있습니다. 반복합니다.\n당신은 지금 자인고등학교에 있습니다.",
        "똝\n0. 당신은 자인고등학교에 있습니다. 반복합니다.\n당신은 지금 자인고등학교에 있습니다.\n<color=#ed2809>(<u>자인고등학교는 20XX년, 불의의 화재 사고로 폐교되었습니다.</u>)</color>똝",
        "1. 자인고등학교는 철저하게 학생들을 관리합니다.\n모든 학생들은 하교 시간 전까지는 학교를 나갈 수 없답니다.",
        "똝1. 자인고등학교는 철저하게 학생들을 관리합니다.\n모든 학생들은 하교 시간 전까지는 학교를 나갈 수 없답니다.\n<color=#ed2809><u>하교 시간 전에 하교를 시도할 경우,\n 자인고등학교는 철저하게 학생들을 관리한다는 것을 기억하십시오.</u></color>똝",
        "\n2. 하교를 위해서는 방송실에서 직접 하교 종을 울려야 합니다.\n자세한 방법은 똝[tab]똝키를 눌러 체크리스트에서 살펴보십시오.",
        "\n3. 교내 진입 전, 반드시 정문 옆 게시판을 꼼꼼히 확인하십시오.\n교내에서 명심해야 할 행동 수칙을 확인하실 수 있습니다.",
        "\n똝3. 교내 진입 전, 반드시 정문 옆 게시판을 꼼꼼히 확인하십시오.\n교내에서 명심해야 할 행동 수칙을 확인하실 수 있습니다.\n<color=#ed2809><u>이를 무시할 경우,\n 자인고등학교는 ■■하게 학생들을 관■?한다는 것을 ■억하십시오.</u></color>똝",
        "4. 만약 탈출이 불가능하다고 판단될 경우, 주머니 속을 확인하십시오.\n알약 하나가 들어있을 것이며,\n\n저희는 해당 알약을 반드시 복용하는 것을 추천드립니다.",
        "똝4. 만약 탈출이 불가능하다고 판단될 경우, 주머니 속을 확인하십시오.\n알약 하나가 들어있을 것이며,\n\n저희는 해당 알약을 반드시 복용하는 것을 추천드립니다.\n<color=#ed2809><u>훨씬 더 편안하게 ■■ ■■ 할 수 있을 것입니다.</u></color>똝",
        "\n\n다시 한번 지침들을 모두 숙지했는지\n확인한 후 입장하시기 바랍니다.\n\n 저희 ■■ ■■■■는 귀하의 무사귀환을 기원합니다.",
    };

    // 특수 기능 기호들이 그대로 출력되면 안되므로
    private string[] introSkipTexts = new string[]{
        "\n<color=#ed2809><경고: 해당 경고문이 보이신다면 꼼꼼히 읽을 것을 권고합니다.>\n</color>\n<부주의로 인한 행동은 다음의 결과들을 불러올 수 있습니다.>\n\n• 적은 출혈이나 상처를 동반한 경미한 부상\n• 생명을 위협할만한 심각한 부상\n• 사망\n• ■■■■■■■■■■■ (■■■■■)",
        "\n0. 당신은 자인고등학교에 있습니다. 반복합니다.\n당신은 지금 자인고등학교에 있습니다.",
        "\n0. 당신은 자인고등학교에 있습니다. 반복합니다.\n당신은 지금 자인고등학교에 있습니다.\n<color=#ed2809>(<u>자인고등학교는 20XX년, 불의의 화재 사고로 폐교되었습니다.</u>)</color>",
        "1. 자인고등학교는 철저하게 학생들을 관리합니다.\n모든 학생들은 하교 시간 전까지는 학교를 나갈 수 없답니다.",
        "1. 자인고등학교는 철저하게 학생들을 관리합니다.\n모든 학생들은 하교 시간 전까지는 학교를 나갈 수 없답니다.\n<color=#ed2809><u>하교 시간 전에 하교를 시도할 경우,\n 자인고등학교는 철저하게 학생들을 관리한다는 것을 기억하십시오.</u></color>",
        "\n2. 하교를 위해서는 방송실에서 직접 하교 종을 울려야 합니다.\n자세한 방법은 [tab]키를 눌러 체크리스트에서 살펴보십시오.",
        "\n3. 교내 진입 전, 반드시 정문 옆 게시판을 꼼꼼히 확인하십시오.\n교내에서 명심해야 할 행동 수칙을 확인하실 수 있습니다.",
        "\n3. 교내 진입 전, 반드시 정문 옆 게시판을 꼼꼼히 확인하십시오.\n교내에서 명심해야 할 행동 수칙을 확인하실 수 있습니다.\n<color=#ed2809><u>이를 무시할 경우,\n 자인고등학교는 ■■하게 학생들을 관■?한다는 것을 ■억하십시오.</u></color>",
        "4. 만약 탈출이 불가능하다고 판단될 경우, 주머니 속을 확인하십시오.\n알약 하나가 들어있을 것이며,\n\n저희는 해당 알약을 반드시 복용하는 것을 추천드립니다.",
        "4. 만약 탈출이 불가능하다고 판단될 경우, 주머니 속을 확인하십시오.\n알약 하나가 들어있을 것이며,\n\n저희는 해당 알약을 반드시 복용하는 것을 추천드립니다.\n<color=#ed2809><u>훨씬 더 편안하게 ■■ ■■ 할 수 있을 것입니다.</u></color>",
        "\n\n다시 한번 지침들을 모두 숙지했는지\n확인한 후 입장하시기 바랍니다.\n\n 저희 ■■ ■■■■는 귀하의 무사귀환을 기원합니다.",
    };

    private string stopText = "아무 키나 눌러 경고문 이어서 보기";
    private string loadedText = "아무 키나 눌러 게임 시작하기";

    private string loadingText = "경고문 불러오는 중";

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
        
        skipParagraph = false;
        while (introTextStep < introTexts.Length){
            if(typingSoundCoroutine != null){
                StopCoroutine(typingSoundCoroutine);
            }
            typingSoundCoroutine = StartCoroutine(PlayTypingSounds());
            
            foreach(GameObject imageObject in IntroImageObjects){
                if(imageObject != null){
                    imageObject.SetActive(false);
                }
            }

            yield return StartCoroutine(TypeText(introTexts[introTextStep], introTextStep));
            StopCoroutine(typingSoundCoroutine);

            
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
            if(Input.anyKey){
                skipParagraph = true;
            }
            
            if(skipParagraph){ 
                audioSource.Stop();
                introTextTMP.text = currentText + introSkipTexts[index];
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
