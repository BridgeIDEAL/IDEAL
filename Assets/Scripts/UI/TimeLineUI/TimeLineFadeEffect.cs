using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeLineFadeEffect : MonoBehaviour
{
    [Header("페이드 효과")]
    float fadeTimer = 0f;
    [SerializeField] float fadeOutSpeed = 1f;
    [SerializeField] float fadeInSpeed = 2f;
    [SerializeField] Image fadeUI;
    [SerializeField] Color transparentColor;
    [SerializeField] Color opaqueColor;

    public void StartFadeOutEffect()
    {
        fadeUI.gameObject.SetActive(true);
        StartCoroutine(FadeOutCor());
    }
    public void StartFadeInEffect()
    {
        StartCoroutine(FadeInCor());
    }

    public IEnumerator FadeOutCor() // dark
    {
        fadeTimer = 0f;
        fadeUI.color = transparentColor;
        Color color = fadeUI.color;
        while (fadeTimer < fadeOutSpeed)
        {
            fadeTimer += Time.deltaTime;
            color.a = fadeTimer / fadeOutSpeed;
            fadeUI.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeInCor() // light
    {
        fadeTimer = fadeInSpeed;
        fadeUI.color = opaqueColor;
        Color color = fadeUI.color;
        while (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            color.a = fadeTimer / fadeInSpeed;
            fadeUI.color = color;
            yield return null;
        }
        fadeUI.gameObject.SetActive(false);
    }
}
