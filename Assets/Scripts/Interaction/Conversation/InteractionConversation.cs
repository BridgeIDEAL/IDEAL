using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionConversation : AbstractInteraction
{
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] private string detectedStr = "";
    [SerializeField] private string dialogueName = "";
    public override float RequiredTime { get => 1.0f;}

    protected override string GetDetectedString(){
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        dialogueRunner.StartDialogue(dialogueName);
    }
}
