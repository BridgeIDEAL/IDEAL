using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetect : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    private float maxDistance = 3.0f;

    private float sphereRadius = 0.5f;

    private Vector3 playerVector;

    private RaycastHit hit = new RaycastHit();

    private GameObject newGameObject = null;
    private GameObject oldGameObject = null;

    private Coroutine interactionCoroutine;
    [SerializeField] private UIInteraction uIInteraction;

    public float requiredTimeRatio = 1.0f;

    void Update(){

        playerVector = playerCamera.transform.localRotation * Vector3.forward;
        if(Physics.SphereCast(transform.position, sphereRadius, playerVector, out hit, maxDistance)){
            // SphereCast에 감지된 경우
            Transform objectHit = hit.transform;
            newGameObject = hit.transform.gameObject;

            InteractionObject newInteractionObject = newGameObject.GetComponent<InteractionObject>();
            InteractionObject oldInteractionObject = null;
            if(oldGameObject != null){
                oldInteractionObject = oldGameObject.GetComponent<InteractionObject>();
            }

            if(!System.Object.ReferenceEquals(newGameObject, oldGameObject)){
                // newGameObject와 oldGameObject가 다를 경우 다르거나 새로운 오브젝트를 가르키는 것이므로
                // 새로운 IO에게는 Ray에 감지됨을 알리고
                newInteractionObject?.DetectedRay();
                // 예전 IO에게는 Ray에서 벗어남을 알린다
                oldInteractionObject?.OutOfRay();

                // interactionCoroutine이 진행중이라면 정지한다.
                if(interactionCoroutine != null){
                    StopInteractionCoroutine();
                }
            }

            if(Input.GetKeyDown(KeyCode.E)){
                if(interactionCoroutine != null){
                    StopInteractionCoroutine();
                }
                interactionCoroutine = StartCoroutine(ActivateInteractionCoroutine(newInteractionObject));
            }
            else if(Input.GetKeyUp(KeyCode.E)){
                if(interactionCoroutine != null){
                    StopInteractionCoroutine();
                }
            }

            // 다음 프레임을 위해 newGameObject를 oldGameObject에 넣어줌
            oldGameObject = newGameObject;
        } else{
            // SphereCast에 감지되지 않은 경우
            // interactionCoroutine이 진행중이라면 중지한다.
            if(interactionCoroutine != null){
                StopInteractionCoroutine();
            }
            
            // 예전 IO에게 Ray에서 벗어남을 알린다.
            if(oldGameObject != null){
                InteractionObject oldInteractionObject = oldGameObject.GetComponent<InteractionObject>();
                oldInteractionObject?.OutOfRay();
            }
            
            newGameObject = null;
            oldGameObject = null;
        }

    }

    private IEnumerator ActivateInteractionCoroutine(InteractionObject interactionObject){
        if(interactionObject == null){
            yield break;
        }
        float requiredTime = interactionObject.GetRequiredTime() * requiredTimeRatio;
        
        if(requiredTime <= 0.0f){
            interactionObject.DetectedInteraction();
            yield break;
        }
        float stepTimer = 0.0f;
        while(stepTimer <= requiredTime){
            uIInteraction.SetProgressFillAmount(stepTimer / requiredTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        uIInteraction.SetProgressFillAmount(0.0f);
        interactionObject.DetectedInteraction();
    }

    private void StopInteractionCoroutine(){
        uIInteraction.SetProgressFillAmount(0.0f);
        StopCoroutine(interactionCoroutine);
    }
}
