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
        "<경고: 이 화면을 함부로 넘기지 마시오.>\n<해당 경고문이 보이신다면 꼼꼼히 읽을 것을 권고합니다. 부주의로 인한 행동은 다음의 결과들을 불러올 수 있습니다.>\n\t-\t적은 출혈이나 상처를 동반한 경미한 부상\n\t-\t생명을 위협할만한 심각한 부상\n\t-\t사망\n\t-\t■■■■■■■■■■■ (■■■■■)",
        "0. 이 경고문이 보인다면 당신은 자인고등학교에 있습니다. 당신이 누구와 어디에서 무엇을 하고 있었던 중요하지 않습니다. \n당신은 자인고등학교에 있습니다.",
        "1. 자인고등학교는 20XX년 불의의 화재 사고로 인해 폐교되었지만, 최근 정상적인 모습으로 현실에 다시 나타나는 현상이 확인되었습니다. \n자인고등학교가 전소되어 사라졌다는 사실은 변하지 않으며, 자인고등학교는 더 이상 현실에 존재하는 공간이 아닙니다. \n다시 한번 반복합니다. 자인고등학교는 존재하지 않습니다.",
        "2. 본격적으로 입장하기 전, 정문 야외 게시판을 꼭 확인한 후 입장하십시오.\n적합하게 행동한다면 학교 내부의 존재들은 당신에게 큰 관심을 갖지 않을 것입니다.",
        "3. 지금까지 자인고등학교에서 확인된 유일한 생존 방법은 4층 방송실에서 하교 종을 울린 후, 정문으로 다시 내려와 탈출하는 방법입니다. \n그 이외의 방법들을 시도했을 때의 결과는 생존을 보장하지 못합니다.",
        "4. 위 방법으로 탈출할 만한 가능성이 전혀 보이지 않는 순간이 온다면, 손 안에 알약 하나를 발견하실 수 있을 것입니다. \n정말로 더 이상 앞으로 나아가지 못할 때에만 신중하게 복용하십시오.",
        "5. 다시 한 번 지침들을 모두 숙지했는지 확인한 후 입장하시기 바랍니다. \n저희 ■■■■■■■는 귀하의 무사귀환을 기원합니다.",
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
            yield return StartCoroutine(TypeText(introTexts[introTextStep]));
            StopCoroutine(typingSoundCoroutine);

            if(!skipPage)yield return new WaitForSeconds(1f); // 다음 텍스트로 넘어가기 전 대기 시간
            introTextStep++;

            if(introTextStep == 3){
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

    IEnumerator TypeText(string text){
        string currentText = introTextTMP.text;
        int cnt = 0;
        foreach (char letter in text.ToCharArray()){
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
                break;
            }
            introTextTMP.text += letter;
            cnt++;
            yield return new WaitForSeconds(0.1f); // 타이핑 속도 조절
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
