using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class UIGuideLogManager : MonoBehaviour
{
    private static UIGuideLogManager instance = null;
    public static UIGuideLogManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private GuideLogManager guideLogManager;
    private List<GuideLogRecord> guideLogRecordList = new List<GuideLogRecord>();
    private List<string> bookPages = new List<string>();
    [SerializeField] private IdealBook idealBook;
    [SerializeField] private GameObject pageObject;
    private RectTransform pageRect;
    private TextMeshProUGUI pageTMP;

    
    private static float titleFontSize = 55.0f;
    private static float bigFontSize = 32.0f;
    private static float bigIndent = 0.0f;
    private static float middleFontSize = 28.0f;
    private static float middleIndent = 20.0f;
    private static float smallFontSize = 24.0f;
    private static float smallIndent = 40.0f;

    private static int redHigh = 250;
    private static int colorLevel = 5;
    private static int redLow = 150;


    private void Awake(){
        if(Instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
        pageTMP = pageObject.GetComponent<TextMeshProUGUI>();
        pageRect = pageObject.GetComponent<RectTransform>();
    }

    public void OpenBook(){
        guideLogManager = GuideLogManager.Instance;
        UpdateBookPage();
        idealBook.UpdateBookPage(bookPages);
    }

    private void UpdateBookPage(){
        guideLogRecordList = guideLogManager.guideLogRecordList;
        bookPages = new List<string>();
        // 겉 표지 Front
        int nowAttempt = -1;
        if(CountAttempts.Instance != null){
            nowAttempt = CountAttempts.Instance.GetAttemptCount() + 1;
        }
        bookPages.Add($"\n\n\n<align=center><size={titleFontSize}>실종 기록 일지</size></align>\n\n<align=right><size={bigFontSize}>-{nowAttempt}번째 실종자-</size></align>");
        bookPages.Add("");
        int bookindex = 1;
        for(int i = 0 ; i < guideLogRecordList.Count; i++){
            GuideLog guideLog = guideLogManager.GetGuideLog(guideLogRecordList[i].GetGuideLogID());
            string curStr = bookPages[bookindex];
            // 시도 횟수가 text에 들어가는 경우 치환처리
            string guideText = guideLog.GetGuideText();
            int attempt = guideLogRecordList[i].GetAttempt();
            if(guideText.Contains("$attempt")){
                guideText = guideText.Replace("$attempt", attempt.ToString());
            }

            // TO DO 규칙 대중 하에 따라 크기 및 bold 처리
            string sizeStart = "", sizeEnd = "</size>";
            string indentStart = "", indentEnd = "</indent>";
            string colorStart = "", colorEnd = "";
            if(guideLog.GetID() % 100 != 0){    // 소 규칙
                sizeStart = $"<size={smallFontSize}>";
                indentStart = $"<indent={smallIndent}>";
                int colorDegree = Mathf.Abs(nowAttempt - attempt);
                int redDegree = (int)Mathf.Lerp(redHigh, redLow, (float)colorDegree / colorLevel);

                string hexColor = ColorUtility.ToHtmlStringRGB(new Color(redDegree / 255.0f, 0.4f, 0.4f));
                if(attempt <= -2){
                    hexColor = "FFFFFF00";
                }
                colorStart = $"<color=#{hexColor}>";
                colorEnd = "</color>";
            }
            else if(guideLog.GetID() % 10000 != 0){ // 중 규칙
                sizeStart = $"<size={middleFontSize}>";
                indentStart = $"<indent={middleIndent}>";
            }
            else{   // 대 규칙
                sizeStart = $"<size={bigFontSize}>";
                indentStart = $"<indent={bigIndent}>";
            }

            
            string nextStr = curStr + sizeStart + indentStart + colorStart + guideText + colorEnd + indentEnd + sizeEnd + "\n";
            if(CheckOverFlow(nextStr)){
                bookPages.Add("");
                bookindex++;
                i--;
            }
            else{
                bookPages[bookindex] = nextStr;
            }
        }
        // 내지는 짝수여야 하므로
        if(bookindex % 2 == 1){
            bookPages.Add("");
        }
        // 겉 표지 Back
        bookPages.Add("");
    }

    private bool CheckOverFlow(string str){
        pageTMP.text = str;
            
        Vector2 rectSize = pageRect.rect.size;
        Vector2 textSize = pageTMP.GetPreferredValues();

        return textSize.y > rectSize.y;
    }

    public string GetBookPage(int index){
        return bookPages[index];
    }
}
