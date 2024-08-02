using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : AbstractInteraction
{
    [SerializeField] private AudioClip unlockDoorAudio;
    [SerializeField] private AudioClip slidingDoorAudio;
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
        if(Inventory.Instance.UseItemWithItemCode(needItem) || needItem == 0 || ProgressManager.Instance.GetItemLogExist(needItem)){
            OpenDoor();
            if(activationLogNum != -1){
                //ActivationLogManager.Instance.AddActivationLog(activationLogNum);
            }
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(successInteractionStr);
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
        ProgressManager.Instance.SetDoorLog(this.transform.parent.parent.name + this.transform.name, 1);
    }

    private void Awake(){
        if(ProgressManager.Instance.GetDoorLog(this.transform.parent.parent.name + this.transform.name) == 1){
            doorObject.transform.localPosition = destPosition;
        }
    }

    private IEnumerator OpenDoorCoroutine(){
        Vector3 startPos = doorObject.transform.localPosition;
        float stepTimer = 0.0f;
        float moveTime = openRequiredTime * 0.75f;

        if(needItem != 0){
            if(audioSource != null){
                audioSource.clip = unlockDoorAudio;
                audioSource.Play();
            }
            yield return new WaitForSeconds(unlockDoorAudio.length);
        }
        

        if(audioSource != null){
            audioSource.clip = slidingDoorAudio;
            audioSource.Play();
        }
        while(stepTimer <= moveTime){
            // TO DO
            // 등속도 운동과 삼각함수 사용한 거 비교해보고 골라보기
            doorObject.transform.localPosition = Vector3.Lerp(startPos, destPosition, stepTimer/moveTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
    }
}
