using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public enum DialogueSeriesCharacter{
    None = 0,
    Bystander,
    Guilty,
    Victim,
}

public class InteractionConversation : AbstractInteraction
{
    public DialogueRunner dialogueRunner;
    public ConversationManager conversationManager;
    public string detectedStr = "";
    public DialogueSeriesCharacter dialougeSeries = DialogueSeriesCharacter.None;
    public bool cantalk = true;
    public string dialogueName = "";
    public string monsterName = "";
    public override float RequiredTime { get => 1.0f;}
    [SerializeField] private bool lookPlayerwithConversation = true;

    protected override string GetDetectedString(){
        if(detectedStr == "") return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction(){
        if(!cantalk) return;
        if(dialogueName != ""){
            dialogueName = FindRightDialougeName();
            if(lookPlayerwithConversation) LookPlayer();
            conversationManager.SetTalkerName(monsterName);
            dialogueRunner.StartDialogue(dialogueName);
            cantalk = false;
        }
    }

    private void LookPlayer(){
        //Transform playerTransform = GameObject.Find("PlayerCapsule").transform;
        //this.transform.LookAt(playerTransform);
    }

    private string FindRightDialougeName(){
        switch(dialougeSeries){
            case DialogueSeriesCharacter.None:
                return dialogueName;
                break;
            case DialogueSeriesCharacter.Bystander:
                switch(ConversationPointManager.Instance.GetConvPoint(DialogueSeriesCharacter.Bystander)){
                    case 1:
                        return "D_108_Bystander01_Start";
                        break;
                    case 2:
                        return "D_109_Bystander02_Start";
                        break;
                    case 3:
                        return "D_110_Bystander03_Start";
                        break;
                    case 4:
                        return "D_111_Bystander04_Start";
                        break;
                    case 5:
                        return "D_112_Bystander05_Start";
                        break;
                }
                ConversationPointManager.Instance.AddConvPoint(DialogueSeriesCharacter.Bystander);
                break;
            case DialogueSeriesCharacter.Guilty:
                switch(ConversationPointManager.Instance.GetConvPoint(DialogueSeriesCharacter.Guilty)){
                    case 1:
                        return "D_113_Guilty01_Start";
                        break;
                    case 2:
                        return "D_114_Guilty02_Start";
                        break;
                    case 3:
                        return "D_115_Guilty03_Start";
                        break;
                    case 4:
                        return "D_116_Guilty04_Start";
                        break;
                }
                ConversationPointManager.Instance.AddConvPoint(DialogueSeriesCharacter.Guilty);
                break;
            case DialogueSeriesCharacter.Victim:
                switch(ConversationPointManager.Instance.GetConvPoint(DialogueSeriesCharacter.Victim)){
                    case 1:
                        return "D_117_Victim01_Start";
                        break;
                    case 2:
                        return "D_118_Victim02_Start";
                        break;
                    case 3:
                        return "D_119_Victim03_Start";
                        break;
                    case 4:
                        return "D_120_Victim04_Start";
                        break;
                }
                ConversationPointManager.Instance.AddConvPoint(DialogueSeriesCharacter.Victim);
                break;
        }
        return dialogueName;
    }
}
