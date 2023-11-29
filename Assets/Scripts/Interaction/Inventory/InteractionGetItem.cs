using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetItem : AbstractInteraction
{
    [SerializeField] private InteractionItemData interactionItemData;
    [SerializeField] private string detectedStr;
    [SerializeField] private string afterInteractionStr = "";
    [SerializeField] private int activationLogNum = -1;
    [SerializeField] private float requiredTime = 1.0f;
    [SerializeField] private int availableCount = 1;
    public override float RequiredTime { get => requiredTime;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        Inventory.Instance.Add(interactionItemData, 1);
        if(activationLogNum != -1){
            ActivationLogManager.Instance.AddActivationLog(activationLogNum);
        }
        if(afterInteractionStr != ""){
            InteractionManager.Instance.uIInteraction.GradientText(afterInteractionStr);
        }
        availableCount--;
        if(availableCount < 1){
            Destroy(this.gameObject);
        }
    }
}
