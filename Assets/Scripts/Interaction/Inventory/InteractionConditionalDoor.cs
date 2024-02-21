using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionConditionalDoor : AbstractInteraction
{
    [SerializeField] GameObject doorObject;
    [SerializeField] private Vector3 destPosition;
    [SerializeField] private float openRequiredTime = 1.0f;
    public bool canOpen = false;
    private bool isOpen = false;
    private Coroutine moveCoroutine;
    [SerializeField] private string detectedStr;
    [SerializeField] private string successInteractionStr = "";
    [SerializeField] private string failInteractionStr = "";
    [SerializeField] private int activationLogNum = -1;
    
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        if(isOpen) return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(canOpen){
            if(audioSource != null){
                audioSource.Play();
            }
            OpenDoor();
            if(activationLogNum != -1){
                ActivationLogManager.Instance.AddActivationLog(activationLogNum);
            }
            if(successInteractionStr != ""){
                InteractionManager.Instance.uIInteraction.GradientText(successInteractionStr);
            }
        }
        else{
            if(failInteractionStr != ""){
                InteractionManager.Instance.uIInteraction.GradientText(failInteractionStr);
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
        Vector3 startPos = doorObject.transform.position;
        float stepTimer = 0.0f;
        while(stepTimer <= openRequiredTime){
            // TO DO
            // 등속도 운동과 삼각함수 사용한 거 비교해보고 골라보기
            doorObject.transform.position = Vector3.Lerp(startPos, destPosition, stepTimer/openRequiredTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
    }
}
