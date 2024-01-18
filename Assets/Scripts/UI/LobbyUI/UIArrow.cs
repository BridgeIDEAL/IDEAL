using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIArrow : MonoBehaviour
{
    [SerializeField] private Image arrowImage;

    private bool currentMouseOver = false;
    private bool parentMouseOver = false;

    private Coroutine colorCoroutine = null;
    private float colorTime = 0.3f;

    [SerializeField] private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    [SerializeField] private EventSystem eventSystem;

    private bool CheckIfMouseOverUI(){
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEventData, results);

        return results.Count > 0;
    }
    void Update() {
        parentMouseOver = currentMouseOver;
        currentMouseOver = CheckIfMouseOverUI();
        if(parentMouseOver == false && currentMouseOver == true){
            if(colorCoroutine != null){
                StopCoroutine(colorCoroutine);
            }
            colorCoroutine = StartCoroutine(GetColorCoroutine());
        }
        else if(parentMouseOver == true && currentMouseOver == false){
            if(colorCoroutine != null){
                StopCoroutine(colorCoroutine);
            }
            colorCoroutine = StartCoroutine(LoseColorCoroutine());
        }
    }

    private IEnumerator GetColorCoroutine(){
        float stepTimer = 0.0f;
        Color color = arrowImage.color;
        while(stepTimer <= colorTime){
            color.a = Mathf.Lerp(0.0f, 1.0f, stepTimer / colorTime);
            arrowImage.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        color.a = 1.0f;
        arrowImage.color = color;
    }

    private IEnumerator LoseColorCoroutine(){
        float stepTimer = 0.0f;
        Color color = arrowImage.color;
        while(stepTimer <= colorTime){
            color.a = Mathf.Lerp(1.0f, 0.0f, stepTimer / colorTime);
            arrowImage.color = color;
            stepTimer += Time.deltaTime;
            yield return null;
        }
        color.a = 0.0f;
        arrowImage.color = color;
    }
}
