using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractionConversation : AbstractInteraction
{
    public DialogueRunner dialogueRunner;
    public string detectedStr = "";
    public string dialogueName = "";
    public string monsterName = "";
    public override float RequiredTime { get => 1.0f;}
    [SerializeField] private bool lookPlayerwithConversation = true;

    protected override string GetDetectedString(){
        if(detectedStr == "") return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(dialogueName != ""){
            if(lookPlayerwithConversation) LookPlayer();
            dialogueRunner.StartDialogue(dialogueName);
        }
    }

    private void LookPlayer(){
        //Transform playerTransform = GameObject.Find("PlayerCapsule").transform;
        //this.transform.LookAt(playerTransform);
    }
}
