using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : AbstractInteraction
{
    [SerializeField] private AudioClip unlockDoorAudio;
    [SerializeField] private AudioClip lockDoorAudio;
    
    [SerializeField] GameObject doorObject;
    [SerializeField] private Vector3 destPosition;
    public Vector3 DestPosition { set { destPosition = value; } }
    [SerializeField] private float openRequiredTime = 1.0f;
    private bool isOpen = false;
    private Coroutine moveCoroutine;
    [SerializeField] private string detectedStr;
    public string DetectStr { set { detectedStr = value; } }
    [SerializeField] private string successInteractionStr = "";
    public string SuccessInteractionStr { set { successInteractionStr = value; } }
    [SerializeField] private string failInteractionStr = "";
    public string FailInteractionStr { set { failInteractionStr = value; } }
    [SerializeField] private int needItem = 0;
    public int NeedItem { set { needItem = value; } }
    [SerializeField] private int activationLogNum = -1;
    
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        if(isOpen) return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(Inventory.Instance.UseItemWithItemCode(needItem)){
            if(audioSource != null){
                audioSource.clip = unlockDoorAudio;
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
            if(audioSource != null){
                audioSource.clip = lockDoorAudio;
                audioSource.Play();
            }
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
        Debug.Log("실행하는중..");
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
