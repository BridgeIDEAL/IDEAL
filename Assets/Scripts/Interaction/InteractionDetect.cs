using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDetect : MonoBehaviour
{
    [SerializeField]
    private ScriptHub scriptHub;
    private Camera playerCamera;

    private float maxDistance = 1.75f;

    private float sphereRadius = 0.3f;

    private Vector3 playerVector;

    private RaycastHit hit = new RaycastHit();

    private GameObject newGameObject = null;
    private GameObject oldGameObject = null;

    private Coroutine interactionCoroutine;
    private UIInteraction uIInteraction;

    public float requiredTimeRatio = 1.0f;
    private string disableInteractionStr = "팔이 심하게 손상되어 상호작용이 불가능하다.";

    public void Init(){
        playerCamera = scriptHub.playerCamera;
        uIInteraction = scriptHub.uIInteraction;
    }

    public void SetOldGameObject(GameObject gameObject_){
        oldGameObject = gameObject_;
    }

    public void GameUpdate(){
        playerVector = playerCamera.transform.localRotation * Vector3.forward;
        // Interaciton, Monster, IgnoreRaycast 레이어만 충돌 체크 
        int layerMask = 1 << LayerMask.NameToLayer("Interaction") | 1 << LayerMask.NameToLayer("Monster") | 1 << LayerMask.NameToLayer("InteractionObstacle"); 
        if(Physics.SphereCast(playerCamera.transform.position, sphereRadius, playerVector, out hit, maxDistance, layerMask)){
            // SphereCast에 감지된 경우
            newGameObject = hit.transform.gameObject;
            if(newGameObject.layer == LayerMask.NameToLayer("InteractionObstacle")){
                // 방해물이 탐지된 경우
                // interactionCoroutine이 진행중이라면 중지한다.
                if(interactionCoroutine != null){
                    StopInteractionCoroutine();
                }
                
                // 예전 IO에게 Ray에서 벗어남을 알린다.
                if(oldGameObject != null){
                    InteractionObject oldInteractionObject_ = oldGameObject.GetComponent<InteractionObject>();
                    oldInteractionObject_?.OutOfRay();
                }
                
                newGameObject = null;
                oldGameObject = null;
                return;
            } 

            InteractionObject newInteractionObject = newGameObject.GetComponent<InteractionObject>();
            InteractionObject oldInteractionObject = null;
            if(oldGameObject != null){
                oldInteractionObject = oldGameObject.GetComponent<InteractionObject>();
            }

            if(!System.Object.ReferenceEquals(newGameObject, oldGameObject)){
                // newGameObject와 oldGameObject가 다를 경우 다르거나 새로운 오브젝트를 가르키는 것이므로
                // 예전 IO에게는 Ray에서 벗어남을 알리고
                oldInteractionObject?.OutOfRay();

                // TODO 몬스터에게 추격당하는지 판별되는 변수 필요
                // 몬스터에게 추격 중 몬스터와 상호작용(대화) 불가능
                if(newGameObject.layer == LayerMask.NameToLayer("Monster")) {
                    // TO DO ~~~~~~~~~~
                    // if (IdealSceneManager.Instance.CurrentGameManager.EntityEvent.IsChase)
                        return;
                }

                // 새로운 IO에게는 Ray에 감지됨을 알린다.
                newInteractionObject?.DetectedRay();
                

                // interactionCoroutine이 진행중이라면 정지한다.
                if(interactionCoroutine != null){
                    StopInteractionCoroutine();
                }
            }

            if(Input.GetKeyDown(KeyCode.E)){
                // UI가 가리고 있다면 상호작용 불가
                // 텍스트가 사라지는 중이면 상호작용 불가
                if(!uIInteraction.textGradiating && IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.CanInteraction()){
                    if(interactionCoroutine != null){
                    StopInteractionCoroutine();
                    }
                    interactionCoroutine = StartCoroutine(ActivateInteractionCoroutine(newInteractionObject));
                }   
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

        if (requiredTimeRatio >= 2.0f){ // 양 손 다 체력 0인 경우, 상호작용 불가능
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(disableInteractionStr);
        }
        else{
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
        
        
    }

    private void StopInteractionCoroutine(){
        uIInteraction.SetProgressFillAmount(0.0f);
        StopCoroutine(interactionCoroutine);
    }
}
