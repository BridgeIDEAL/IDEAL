using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastTimeline : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField] Image fadeImage;
    [SerializeField] float decreaseView;
    [SerializeField] float increaseView;
    [SerializeField] float fadeoutTime;
    
    public void CallDecreaseFieldOfView() { StartCoroutine(DecreaseFieldOfView()); }
    [SerializeField, Header("FieldView Time")] float decreaseFieldViewTime; 
    IEnumerator DecreaseFieldOfView()
    {
        float timer = 0f;
        float startView = mainCam.fieldOfView;
        float endView = decreaseView;
        while (timer < decreaseFieldViewTime)
        {
            timer+=Time.deltaTime;
            mainCam.fieldOfView = Mathf.Lerp(startView, endView, timer / decreaseFieldViewTime);
            yield return null;
        }
        mainCam.fieldOfView = endView;
    }

    public void CallReturnFieldOfView() { StartCoroutine(IncreaseFieldOfView()); }
    IEnumerator IncreaseFieldOfView()
    {
        float timer = 0f;
        float startView = mainCam.fieldOfView;
        float endView = increaseView;
        while (timer < decreaseFieldViewTime)
        {
            timer += Time.deltaTime;
            mainCam.fieldOfView = Mathf.Lerp(startView, endView, timer / decreaseFieldViewTime);
            yield return null;
        }
        mainCam.fieldOfView = endView;
    }

    public void CallLastFadeOut() { StartCoroutine(FadeOut()); }
    IEnumerator FadeOut()
    {
        float timer = 0f;
        Color stColor = fadeImage.color;
        stColor.a = 0;
        while (timer < fadeoutTime)
        {
            timer += Time.deltaTime;
            stColor.a = Mathf.Lerp(0f, 1f, timer / fadeoutTime);
            fadeImage.color = stColor;
            yield return null;
        }
        fadeImage.color = stColor;
    }

    public void GameEnd() { IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOverWithVHSEffect(9); }
}

