using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngame : MonoBehaviour
{
    [SerializeField] private Image visualFilter;
    [SerializeField] private Image visualFilter_Red;
    [SerializeField] private Image fadeFilter;
    private float hurtEffectTime = 1.0f;
    private float hurtEffectAlpha = 0.27058f;
    private float fadeEffectTime = 0.7f;
    private Coroutine hurtCoroutine;
    private Coroutine fadeCoroutine;

    public void SetVisualFilter(float ratio){
        Color color = visualFilter.color;
        color.a = ratio;
        visualFilter.color = color;
    }

    public void HurtEffect(){
        if(hurtCoroutine != null){
            StopCoroutine(hurtCoroutine);
        }
        hurtCoroutine = StartCoroutine(HurtEffectCoroutine());
    }

    private IEnumerator HurtEffectCoroutine(){
        float stepTimer = 0.0f;
        Color color = visualFilter_Red.color;
        float startAlpha = color.a;
        while(stepTimer <= hurtEffectTime / 2.0f){
            color.a = Mathf.Lerp(startAlpha, hurtEffectAlpha, stepTimer / (hurtEffectTime/2.0f));
            visualFilter_Red.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        stepTimer = 0.0f;
        while(stepTimer <= hurtEffectTime / 2.0f){
            color.a = Mathf.Lerp(hurtEffectAlpha, 0.0f, stepTimer / (hurtEffectTime/2.0f));
            visualFilter_Red.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        color.a = 0.0f;
        visualFilter_Red.color = color;
    }

    public void FadeOutInEffect(Action callback_){
        if(fadeCoroutine != null){
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOutInEffectCoroutine(callback_));
    }

    public void FadeInEffect(){
        if(fadeCoroutine != null){
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeInEffectCoroutine());
    }

    public void FadeOutEffect(){
        if(fadeCoroutine != null){
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOutEffectCoroutine());
    }

    private IEnumerator FadeInEffectCoroutine(){
        float stepTimer = 0.0f;
        Color color = fadeFilter.color;
        while(stepTimer <= fadeEffectTime){
            color.a = Mathf.Lerp(1.0f, 0.0f, stepTimer / fadeEffectTime);
            fadeFilter.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        color.a = 0.0f;
        fadeFilter.color = color;
    }

    private IEnumerator FadeOutEffectCoroutine(){
        float stepTimer = 0.0f;
        Color color = fadeFilter.color;
        while(stepTimer <= fadeEffectTime){
            color.a = Mathf.Lerp(0.0f, 1.0f, stepTimer / fadeEffectTime);
            fadeFilter.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        color.a = 1.0f;
        fadeFilter.color = color;
    }

    private IEnumerator FadeOutInEffectCoroutine(Action callback_ = null){
        yield return StartCoroutine(FadeOutEffectCoroutine());
        if(callback_ != null){
            callback_();
        }
        StartCoroutine(FadeInEffectCoroutine());
    }
}
