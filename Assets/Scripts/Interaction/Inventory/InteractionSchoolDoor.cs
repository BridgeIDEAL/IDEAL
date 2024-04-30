using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSchoolDoor : AbstractInteraction
{
    [SerializeField] GameObject LeftDoorObject;
    [SerializeField] GameObject RightDoorObject;
    [SerializeField] private Vector3 LeftStartRotation;
    [SerializeField] private Vector3 RightStartRotation;
    [SerializeField] private Vector3 LeftDestRotation;
    [SerializeField] private Vector3 RightDestRotation;
    [SerializeField] private float openRequiredTime = 1.0f;
    private bool isOpen = false;
    private Coroutine moveCoroutine;
    [SerializeField] private string detectedStr;
    [SerializeField] private string successInteractionStr = "";
    [SerializeField] private string failInteractionStr = "";
    [SerializeField] private int needItem = 1110;
    [SerializeField] private int activationLogNum = -1;
    
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        if(isOpen) return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(Inventory.Instance.FindItemIndex(needItem) != -1){
            if(audioSource != null){
                audioSource.Play();
            }
            OpenDoor();
            if(activationLogNum != -1){
                ActivationLogManager.Instance.AddActivationLog(activationLogNum);
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
    }

    private IEnumerator OpenDoorCoroutine(){
        Vector3 LeftRotation, RightRotation;
        float stepTimer = 0.0f;
        while(stepTimer <= openRequiredTime){
            // 등속도 운동과 삼각함수 사용한 거 비교해보고 골라보기
            LeftRotation = Vector3.Lerp(LeftStartRotation, LeftDestRotation, stepTimer/openRequiredTime);
            RightRotation = Vector3.Lerp(RightStartRotation, RightDestRotation, stepTimer/openRequiredTime);
            LeftDoorObject.transform.rotation = Quaternion.Euler(LeftRotation);
            RightDoorObject.transform.rotation = Quaternion.Euler(RightRotation);
            stepTimer += Time.deltaTime;
            yield return null;
        }
    }
}
