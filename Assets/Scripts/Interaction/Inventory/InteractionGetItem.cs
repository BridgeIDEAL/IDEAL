using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetItem : AbstractInteraction
{
    public InteractionItemData interactionItemData;
    [SerializeField] private string detectedStr;
    [SerializeField] private string afterInteractionStr = "";
    [SerializeField] private int activationLogNum = -1;
    [SerializeField] private float requiredTime = 1.0f;
    [SerializeField] private int availableCount = 1;
    public override float RequiredTime { get => requiredTime;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction() {
        Inventory.Instance.Add(interactionItemData, 1);
        if(audioSource != null){
            audioSource.Play();
        }
        if (activationLogNum != -1) {
            ActivationLogManager.Instance.AddActivationLog(activationLogNum);
        }
        if (afterInteractionStr != "") {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(afterInteractionStr);
        }
        availableCount--;
        if (availableCount < 1) {
            Destroy(this.gameObject);
        }
        if (audioSource != null) {
            // Inventory.GetItemSound에서 아이템 획득 소리들 처리
        }            
    }
}
