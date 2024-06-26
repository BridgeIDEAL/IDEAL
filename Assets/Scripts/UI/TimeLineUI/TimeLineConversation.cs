using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeLineConversation : MonoBehaviour
{
    [Header("Ÿ�Ӷ��� ��ȭ")]
    [SerializeField, Tooltip("���� ����")] int curConversation = 0;
    [SerializeField, Tooltip("��ȭ�� Ÿ���� �Ǵ� �ð�")] float typeTerm;
    [SerializeField, Tooltip("���� ��ȭ�� ���۵Ǵ� �� �ɸ��� �ð�")] float nextTypeTerm = 0.5f;
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
            // ��ȭ ����
            tmpText.gameObject.SetActive(false);
        }
        else
        {
            // ���� ��ȭ
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
