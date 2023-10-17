using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInteraction : MonoBehaviour
{
    [SerializeField]
    GameObject interactionTextGameObject;

    TextMeshProUGUI interactionText;

    void Awake(){
        interactionText = interactionTextGameObject.GetComponent<TextMeshProUGUI>();
    }
    
    public void SetTextActive(bool active){
        interactionTextGameObject.SetActive(active);
    }
    
    public void SetTextContents(string textContents){
        interactionText.text = textContents;
    }


}
