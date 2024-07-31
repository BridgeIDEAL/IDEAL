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
    // Add By Jun Start~~~
    [Header("Add By Jun : Use SteelDoorItem & Change Guard Monster")]
    [SerializeField] private int needItem;
    [SerializeField] private bool isInBuildingA;
    private bool onceActiveEvent = true;
    // ~~~End
    public override float RequiredTime { get => 1.0f; }

    protected override string GetDetectedString()
    {
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {
        // Add By Jun Start~~~
        if (Inventory.Instance.FindItemIndex(needItem) == -1 && needItem!=0)
        {
            if (failInteractionStr != "")
            {
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(failInteractionStr);
            }
            return;
        }
        if (onceActiveEvent && needItem!=0)
        {
            onceActiveEvent = false;
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.DespawnEntity("1F_PatrolGuard");
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.SpawnEntity("1F_StandGuard");
        }
        // ~~~End
        if (audioSource != null)
        {

            audioSource.clip = unlockDoorAudio;
            audioSource.Play();
        }
        int itemIndex = Inventory.Instance.FindItemIndex(1107); // 교과서 오브젝트 3개 수집 안하고 이동 시 벌점 부과
        // 아이템이 없을 경우 Inventory.Instance.GetCurrentAmount(itemIndex) == -1
        if (Inventory.Instance.GetCurrentAmount(itemIndex) < 3 && !isInBuildingA)
        {
            // PenaltyPointManager.Instance.AddPenaltyPoint(1);
        }
        if(isInBuildingA){
            ProgressManager.Instance.UpdateCheckList(104, 1);
        }
        IdealSceneManager.Instance.ChangeAnotherGameScene(currentSceneName, destSceneName, destPosition, destRotation);
        if (successInteractionStr != "")
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(successInteractionStr);
        }
    }
}
