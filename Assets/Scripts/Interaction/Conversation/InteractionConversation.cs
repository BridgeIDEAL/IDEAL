using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionConversation : AbstractInteraction
{
    public DialogueRunner dialogueRunner;
    public string detectedStr = "";
    public string dialogueName = "";
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        if(detectedStr == "") return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(dialogueName != ""){
            dialogueRunner.StartDialogue(dialogueName);
        }
    }
}
