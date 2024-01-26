using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionNurse : AbstractInteraction
{
    public DialogueRunner dialogueRunner;
    public ConversationManager conversationManager;
    public string detectedStr = "";
    public string dialogueName = "";
    public string monsterName = "";
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        if(detectedStr == "") return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        //string dialogueName;
        if(Inventory.Instance.FindItemIndex(0) == -1){
            dialogueName = "NoPaper";
        }
        else if(HealthPointManager.Instance.NoDamage()){
            dialogueName = "NoDamage";
        }
        else{
            dialogueName = "04_1F_SchoolNurseStart";
        }
        conversationManager.SetTalkerName(monsterName);
        dialogueRunner.StartDialogue(dialogueName);
    }
}
