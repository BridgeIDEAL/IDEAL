using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionConversationTest01 : AbstractInteraction
{
    [SerializeField] DialogueRunner dialogueRunner;
    public override float RequiredTime { get => 2.0f;}

    protected override string GetDetectedString(){
        return $"Press E, Talk with Student!";
    }

    protected override void ActInteraction(){
        dialogueRunner.StartDialogue("Start");
    }
}
