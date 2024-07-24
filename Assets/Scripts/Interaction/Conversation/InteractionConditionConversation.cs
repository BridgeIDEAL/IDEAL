using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionConditionConversation : AbstractInteraction
{
    #region Class Data
    public ConversationManager conversationManager;
    BaseEntity baseEntity = null;
    Entity talkData = null;
    public Entity TalkData { get { InitTalkData(); return talkData; } set { talkData = value; } }
    #endregion

    #region Struct Data
    [Header("Check Dialogue Condition")]
    [SerializeField, Tooltip("아이템, 행동 등 선행조건이 있는지")] bool haveCondition = false;
    public int conditionIndex = -1;
    public bool isSameIndex= false;

    [Header("Check Dialogue State")]
    string dialogueName = "";
    public bool canTalk = true;
    public bool blockTalk = false;
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
        if (!canTalk) return;

        if (baseEntity == null)
            baseEntity = GetComponent<BaseEntity>();
        // Not Have Conversation Condition
        if (!haveCondition)
        {
            dialogueName = TalkData.speakerName + TalkData.speakIndex;
            DialogueManager.Instance.StartDialogue(dialogueName, baseEntity);
            return;
        }

        if (isSameIndex && !CheckCondition())
        {
            dialogueName = "Block1";
            DialogueManager.Instance.StartDialogue(dialogueName, baseEntity);
            return;
        }
        dialogueName = TalkData.speakerName + TalkData.speakIndex;
        DialogueManager.Instance.StartDialogue(dialogueName, baseEntity);
    }
    #endregion

    /****************************************************************************
                                        Jun Method : To Do ~~ Check Condition (Check Item)
    ****************************************************************************/
    #region Jun Method : Link & Manage Talk Index 

    public void InitTalkData()
    {
        if (talkData == null)
        {
            talkData = GetComponent<BaseEntity>().Entity_Data;
            if (talkData.speakIndex == -1)
            {
                canTalk = false;
                return;
            }
            CheckIndex();
        }
    }

    /// <summary>
    /// -1 : Can't Talk
    /// </summary>
    /// <param name="_idx"></param>
    public void ChangeIndex(int _idx)
    {
        TalkData.speakIndex = _idx;
        if (_idx == -1)
        {
            canTalk = false;
            return;
        }
        CheckIndex();
    }

    public void CheckIndex()
    {
        if (conditionIndex == -1)
            return;
        if (TalkData.speakIndex == conditionIndex)
            isSameIndex = true;
    }

    public bool CheckCondition()
    {
        // To Do ~~ Check Have Item
        // If Have Item => 
        //return false;
        return true;
    }
    #endregion
}
