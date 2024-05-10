using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChangeScene : AbstractInteraction
{
    [SerializeField] private AudioClip unlockDoorAudio;
    [SerializeField] private AudioClip lockDoorAudio;
    
    [SerializeField] GameObject doorObject;
    [SerializeField] private Vector3 destPosition;
    [SerializeField] private Vector3 destRotation;
    [SerializeField] private string currentSceneName;
    [SerializeField] private string destSceneName;
    [SerializeField] private string detectedStr;
    [SerializeField] private string successInteractionStr = "";
    [SerializeField] private string failInteractionStr = "";
    
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(audioSource != null){
            audioSource.clip = unlockDoorAudio;
            audioSource.Play();
        }
        int itemIndex = Inventory.Instance.FindItemIndex(1107); // 교과서 오브젝트 3개 수집 안하고 이동 시 벌점 부과
        if(itemIndex == -1 || Inventory.Instance.GetCurrentAmount(itemIndex) < 3){
            PenaltyPointManager.Instance.AddPenaltyPoint(1);
        }
        IdealSceneManager.Instance.ChangeAnotherGameScene(currentSceneName, destSceneName, destPosition, destRotation);
        if(successInteractionStr != ""){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(successInteractionStr);
        }
    }
}
