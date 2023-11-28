using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionConversationTest01 : AbstractInteraction
{
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] string dialogueName = "Start";
    public override float RequiredTime { get => 2.0f;}

    protected override string GetDetectedString(){
        return $"<sprite=0>  Start Talk {dialogueName}!";
    }

    protected override void ActInteraction(){
        dialogueRunner.StartDialogue(dialogueName);
    }
}
