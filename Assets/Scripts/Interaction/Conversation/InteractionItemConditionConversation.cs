using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionItemConditionConversation : InteractionConditionConversation
{
    [SerializeField] int checkItemID;
    [SerializeField] int haveItemTalkIndex;

    /****************************************************************************
                                       Chan Method
   ****************************************************************************/
    #region Chan Method : Interaction
    protected override void ActInteraction()
    {
        if (!canTalk) return;

        if (baseEntity == null)
            baseEntity = GetComponent<BaseEntity>();

        if (isSameIndex && !CheckCondition())
        {
            dialogueName = "Block1";
            Debug.Log(dialogueName);
            DialogueManager.Instance.StartDialogue(dialogueName, baseEntity);
            return;
        }
        else if (CheckCondition() && !isSameIndex)
        {
            dialogueName = TalkData.speakerName + haveItemTalkIndex;
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
    #region Jun Method : Condition
    
    public bool CheckCondition()
    {
        if (Inventory.Instance.FindItemIndex(checkItemID) == -1)
            return false;
        return true;
    }
    #endregion
}
