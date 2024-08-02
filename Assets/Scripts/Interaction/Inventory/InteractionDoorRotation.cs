using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionDoorRotation : AbstractInteraction
{
    [SerializeField] GameObject doorObject;
    [SerializeField] private Vector3 destRotation;
    [SerializeField] private float openRequiredTime = 0.5f;
    private bool isOpen = false;
    private Coroutine moveCoroutine;
    [SerializeField] private string detectedStr;
    [SerializeField] private string successInteractionStr = "";
    [SerializeField] private string failInteractionStr = "";
    [SerializeField] private int needItem = -1; // 아이템이 필요 없으면 -1
    [SerializeField] private int activationLogNum = -1;
    [SerializeField] private bool isKeyBox = false;
    
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        if(isOpen) return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(needItem == -1 || Inventory.Instance.FindItemIndex(needItem) != -1){
            if(audioSource != null){
                audioSource.Play();
            }
            OpenDoor();
            if(activationLogNum != -1){
                //ActivationLogManager.Instance.AddActivationLog(activationLogNum);
            }
            if(successInteractionStr != ""){
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(successInteractionStr);
            }
        }
        else{
            if(failInteractionStr != ""){
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(failInteractionStr);
            }
        }
    }

    private void OpenDoor(){
        isOpen = true;
        if(moveCoroutine != null){
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(OpenDoorCoroutine());
        if(isKeyBox){
            ProgressManager.Instance.SetDoorLog(SceneManager.GetActiveScene().name + transform.parent.parent.name + this.transform.parent.name, 1);
        }
        else{
            ProgressManager.Instance.SetDoorLog(SceneManager.GetActiveScene().name + transform.parent.parent.parent.parent.parent.name + this.transform.parent.parent.parent.name, 1);
        }
        
    }

    private void Awake(){
        if(isKeyBox){
            if(ProgressManager.Instance.GetDoorLog(SceneManager.GetActiveScene().name + transform.parent.parent.name + this.transform.parent.name) == 1){
                doorObject.transform.localRotation = Quaternion.Euler(destRotation);
                isOpen = true;
            }
        }
        else{
            if(ProgressManager.Instance.GetDoorLog(SceneManager.GetActiveScene().name + this.transform.parent.parent.parent.parent.parent.name + this.transform.parent.parent.parent.name) == 1){
                doorObject.transform.localRotation = Quaternion.Euler(destRotation);
                isOpen = true;
            }
        }
        
    }

    private IEnumerator OpenDoorCoroutine(){
        Vector3 startRotation = doorObject.transform.localRotation.eulerAngles;
        Vector3 Rotation;
        
        float stepTimer = 0.0f;
        while(stepTimer <= openRequiredTime){
            // 등속도 운동과 삼각함수 사용한 거 비교해보고 골라보기
            Rotation = Vector3.Lerp(startRotation, destRotation, stepTimer/openRequiredTime);
            doorObject.transform.localRotation = Quaternion.Euler(Rotation);
            stepTimer += Time.deltaTime;
            yield return null;
        }
    }
}
