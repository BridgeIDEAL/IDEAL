using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionCheckItemBeforeTalk : AbstractInteraction
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string detectedStr = "";
    [SerializeField] private string dialogueName = "";
    [SerializeField] private int needItemCode = -1;
    [SerializeField] private string successInteractionStr = "";
    [SerializeField] private string failInteractionStr = "";
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(Inventory.Instance.FindItemIndex(needItemCode) != -1){
            dialogueRunner.StartDialogue(dialogueName);
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
}
