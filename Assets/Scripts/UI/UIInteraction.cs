using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    GameObject interactionTextGameObject;

    TextMeshProUGUI interactionText;

    [SerializeField] private Image progressImage;

    private Coroutine textCoroutine;
    private float textCorTime = 1.5f;
    public bool textGradiating = false;

    void Awake(){
        interactionText = interactionTextGameObject.GetComponent<TextMeshProUGUI>();
        progressImage.fillAmount = 0.0f;
    }
    
    public void SetTextActive(bool active){
        if(textGradiating) return;
        interactionTextGameObject.SetActive(active);
    }
    
    public void SetTextContents(string textContents){
        if(textGradiating) return;
        interactionText.text = textContents;
    }

    public void SetProgressFillAmount(float amount){
        progressImage.fillAmount = amount;
    }

    public void GradientText(string textContents){
        if(textCoroutine != null){
            StopGradientTextCoroutine();
        }
        textCoroutine = StartCoroutine(GradientTextCoroutine(textContents));
    }

    private IEnumerator GradientTextCoroutine(string textContents){
        interactionText.alpha = 1.0f;
        SetTextContents(textContents);
        SetTextActive(true);
        float stepTimer = 0.0f;
        textGradiating = true;
        while(stepTimer <= textCorTime){
            interactionText.alpha = Mathf.Lerp(1.0f, 0.0f, stepTimer / textCorTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        textGradiating = false;
        SetTextActive(false);
        SetTextContents("");
        interactionText.alpha = 1.0f;
    }
    
    private void StopGradientTextCoroutine(){
        StopCoroutine(textCoroutine);
        textGradiating = false;
    }
}
