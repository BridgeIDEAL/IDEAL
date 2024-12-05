using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChangeScene : AbstractInteraction
{
    [SerializeField] private AudioClip lockDoorAudio;

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
    [SerializeField] TeleportPoint teleportPoint; // Jun
    protected override string GetDetectedString()
    {
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {
        // Prevent Door Teleport : When Princiapl Chase
        if (EntityDataManager.Instance.Controller.PrincipalChase)
        {
            string principalFailMessage = "추격에서 벗어나야 한다.";
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(principalFailMessage);
            return;
        }

        // Add By Jun Start~~~
        if (Inventory.Instance.FindItemIndex(Inventory.MasterMey) == -1)
        {
            if (Inventory.Instance.FindItemIndex(needItem) == -1 && needItem != 0)
            {
                audioSource.clip = lockDoorAudio;
                audioSource.Play();
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(failInteractionStr);
                return;
            }
        }

       
        if (onceActiveEvent && needItem!=0)
        {
            onceActiveEvent = false;
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.DespawnEntity("1F_PatrolGuard");
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.SpawnEntity("1F_StandGuard");
        }
        // ~~~End
        
        int itemIndex = Inventory.Instance.FindItemIndex(1107); // 교과서 오브젝트 3개 수집 안하고 이동 시 벌점 부과
        // 아이템이 없을 경우 Inventory.Instance.GetCurrentAmount(itemIndex) == -1
        if (Inventory.Instance.GetCurrentAmount(itemIndex) < 3 && !isInBuildingA)
        {
            // PenaltyPointManager.Instance.AddPenaltyPoint(1);
        }
        ProgressManager.Instance.UpdateCheckList(104, 1);
        IdealSceneManager.Instance.metalDoorSound.Play();
        EntityDataManager.Instance.Controller.DisableChaseGroup();
        // OnlyChase Disable => Here 
        if (HealthPointManager.Instance.chased)
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.ChaseEnd();
            HealthPointManager.Instance.chased = false;
            PenaltyPointManager.Instance.SetChase(false);
        }   

        IdealSceneManager.Instance.ChangeAnotherGameScene(currentSceneName, destSceneName, destPosition, destRotation);
        if (successInteractionStr != "")
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(successInteractionStr);
        }
        EventDataManager.Instance.Notice.CurrentTeleportPoint = teleportPoint; // Jun
    }
}
