using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionCheckItemBeforeTalk : AbstractInteraction
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private ConversationManager conversationManager;
    [SerializeField] private string detectedStr = "";
    [SerializeField] private string dialogueName = "";
    [SerializeField] private int[] needItemCodes = {-1};
    [SerializeField] private string successInteractionStr = "";
    [SerializeField] private string failInteractionStr = "";
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(CheckNeedItem()){
            conversationManager.SetTalkerName("");
            dialogueRunner.StartDialogue(dialogueName);
            if(successInteractionStr != ""){
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(successInteractionStr);
            }
        }
        else{
            if(failInteractionStr != ""){
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(failInteractionStr);
            }
        }
    }

    private bool CheckNeedItem(){
        foreach(var needItemCode in needItemCodes){
            if(Inventory.Instance.FindItemIndex(needItemCode) == -1){
                return false;
            }
        }
        return true;
    }
}
