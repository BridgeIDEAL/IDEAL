using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeLineConversation : MonoBehaviour
{
    [Header("타임라인 대화")]
    [SerializeField, Tooltip("지울 내용")] int curConversation = 0;
    [SerializeField, Tooltip("대화가 타이핑 되는 시간")] float typeTerm;
    [SerializeField, Tooltip("다음 대화가 시작되는 데 걸리는 시간")] float nextTypeTerm = 0.5f;
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
        tmpText.text = "";
        for (int i = 0; i < len; i++)
        {
            tmpText.text += conversationTexts[curConversation][i];
            yield return new WaitForSeconds(typeTerm);
        }
        curConversation += 1;
        yield return new WaitForSeconds(nextTypeTerm);
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

    public void SettingConversationOption(float _typeTerm, float _nextTypeTerm, string[] _conversationTexts)
    {
        typeTerm = _typeTerm;
        nextTypeTerm = _nextTypeTerm;
        conversationTexts = _conversationTexts;
    }
    #endregion
}
