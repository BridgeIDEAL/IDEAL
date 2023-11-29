using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : AbstractInteraction
{
    [SerializeField] GameObject doorObject;
    [SerializeField] private string detectedStr;
    [SerializeField] private string successInteractionStr = "";
    [SerializeField] private string failInteractionStr = "";
    [SerializeField] private int needItem = 0;
    [SerializeField] private int activationLogNum = -1;
    
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(Inventory.Instance.UseItemWithItemCode(needItem)){
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
        doorObject.SetActive(false);
    }
}
