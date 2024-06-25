using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeLineConversation : MonoBehaviour
{
    [Header("Ÿ�Ӷ��� ��ȭ")]
    [SerializeField, Tooltip("���� ����")] int curConversation = 0;
    [SerializeField, Tooltip("��ȭ�� Ÿ���� �Ǵ� �ӵ�")] float typeSpeed;
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
            // ��ȭ ����
            tmpText.gameObject.SetActive(false);
        }
        else
        {
            // ���� ��ȭ
            StartCoroutine(TypingText());
        }
    }
    #endregion
}
