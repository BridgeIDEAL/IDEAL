using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionStudentroomBoard : AbstractInteraction
{
    #region Class Data
    protected Entity talkData = null;
    public Entity TalkData { get { InitTalkData(); return talkData; } set { talkData = value; } }
    #endregion

    #region Struct Data
    [Header("Check Dialogue State")]
    protected string dialogueName = "";

    public string detectedStr = "";
    public override float RequiredTime { get => 1.0f; }
    #endregion

    /****************************************************************************
                                       Chan Method
   ****************************************************************************/
    #region Chan Method : Detect & Interaction
    protected override string GetDetectedString()
    {
        if (detectedStr == "") return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {    
        dialogueName = TalkData.speakerName + TalkData.speakIndex;
        DialogueManager.Instance.StartDialogue(dialogueName);
    }
    #endregion

    /****************************************************************************
           Jun Method : To Do ~~ Check Condition (Check Item)
    ****************************************************************************/

    public void InitTalkData()
    {
        if (talkData == null)
        {
            talkData= EntityDataManager.Instance.GetEntityData(gameObject.name);
        }
    }
}
