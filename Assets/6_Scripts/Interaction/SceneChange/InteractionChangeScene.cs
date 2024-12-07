using System;
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
        
        if(ProgressManager.Instance.checkListDic.ContainsKey(104) && ProgressManager.Instance.checkListDic[104] == -1){
            ProgressManager.Instance.UpdateCheckList(104, 1);
        }
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
