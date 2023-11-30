using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIngame : MonoBehaviour
{
    [SerializeField] private Image visualFilter;
    [SerializeField] private Image visualFilter_Red;
    private float hurtEffectTime = 1.0f;
    private float hurtEffectAlpha = 0.27058f;
    private Coroutine hurtCoroutine;

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
}
