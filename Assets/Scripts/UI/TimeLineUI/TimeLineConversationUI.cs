using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TimeLineConversationUI : MonoBehaviour
{
    [Header("Ÿ�Ӷ��� ��ȭ")]
    [SerializeField, Tooltip("���� ����")] int curConversation = 0;
    [SerializeField, Tooltip("��ȭ�� Ÿ���� �Ǵ� �ӵ�")] float typeSpeed;
    [SerializeField, TextArea] string[] conversationTexts;
    [SerializeField] TextMeshProUGUI tmpText;

    [Header("���̵� ȿ��")]
    float fadeTimer = 0f;
    [SerializeField] float fadeSpeed = 1f;
    [SerializeField] Image fadeUI;
    [SerializeField] Color fadeUIColor;


    [SerializeField] GameObject sitStudent;
    [SerializeField] GameObject standStudent;
    public void StartConversation()
    {
        tmpText.gameObject.SetActive(true);
        StartCoroutine(TypingText());
    }

    public void StartFadeOutEffect()
    {
        fadeUI.gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }
    public void StartFadeInEffect()
    {

        StartCoroutine(FadeIn());
        //fadeUI.gameObject.SetActive(false);
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

    #region Fade
    public IEnumerator FadeIn() // light
    {
        standStudent.SetActive(true);
        fadeTimer = fadeSpeed;
        Color color = fadeUIColor;
        while (fadeTimer >0)
        {
            color.a = fadeTimer / fadeSpeed;
            fadeUI.color = color;
            fadeTimer -= Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut() // dark
    {
        fadeTimer = 0f;
        Color color = fadeUIColor;
        while (fadeTimer < fadeSpeed)
        {
            fadeTimer += Time.deltaTime;
            color.a = fadeTimer / fadeSpeed;
            fadeUI.color = color;
            yield return null;
        }
        sitStudent.SetActive(false);
    }

    #endregion
}
