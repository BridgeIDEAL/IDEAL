using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeLineConversation : MonoBehaviour
{
    [Header("타임라인 대화")]
    [SerializeField, Tooltip("지울 내용")] int curConversation = 0;
    [SerializeField, Tooltip("대화가 타이핑 되는 속도")] float typeSpeed;
    [SerializeField, TextArea] string[] conversationTexts;
    [SerializeField] TextMeshProUGUI tmpText;

    public void StartConversation()
    {
        tmpText.gameObject.SetActive(true);
        StartCoroutine(TypingText());
    }
   
    #region Type
    IEnumerator TypingText()
    {
        int len = conversationTexts[curConversation].Length;
        float timer = typeSpeed / len;
        tmpText.text = "";
        for (int i = 0; i < len; i++)
        {
            tmpText.text += conversationTexts[curConversation][i];
            yield return new WaitForSeconds(timer);
        }
        //yield return null;
        curConversation += 1;
        CheckNextConversation();
    }

    public void CheckNextConversation()
    {
        int len = conversationTexts.Length;
        if (curConversation >= len)
        {
            // 대화 종료
            tmpText.gameObject.SetActive(false);
        }
        else
        {
            // 다음 대화
            StartCoroutine(TypingText());
        }
    }
    #endregion
}
