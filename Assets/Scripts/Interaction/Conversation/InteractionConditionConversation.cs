using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionConditionConversation : AbstractInteraction
{
    #region Class Data
    //public ConversationManager conversationManager;
    protected BaseEntity baseEntity = null;
    protected Entity talkData = null;
    public Entity TalkData { get { InitTalkData(); return talkData; } set { talkData = value; } }
    #endregion

    #region Struct Data
    [Header("Check Dialogue Condition")]
    [SerializeField, Tooltip("Don't Check Index = -1")] protected int conditionIndex = -1;
    protected bool isSameIndex= false;

    [Header("Check Dialogue State")]
    protected string dialogueName = "";
    public bool canTalk = true;
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

    protected override void ActInteraction() { }
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
    #endregion

    public virtual void ForceInteraction()
    {
        ActInteraction();
    }
}
