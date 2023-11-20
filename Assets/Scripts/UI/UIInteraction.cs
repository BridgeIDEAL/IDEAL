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

    void Awake(){
        interactionText = interactionTextGameObject.GetComponent<TextMeshProUGUI>();
        progressImage.fillAmount = 0.0f;
    }
    
    public void SetTextActive(bool active){
        interactionTextGameObject.SetActive(active);
    }
    
    public void SetTextContents(string textContents){
        interactionText.text = textContents;
    }

    public void SetProgressFillAmount(float amount){
        progressImage.fillAmount = amount;
    }
}
