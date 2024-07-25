using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNoConditionConversation : InteractionConditionConversation
{
    /****************************************************************************
                                         Chan Method
     ****************************************************************************/
    #region Chan Method : Interaction
    protected override void ActInteraction()
    {
        if (!canTalk) return;

        if (baseEntity == null)
            baseEntity = GetComponent<BaseEntity>();

        dialogueName = TalkData.speakerName + TalkData.speakIndex;
        DialogueManager.Instance.StartDialogue(dialogueName, baseEntity);
    }
    #endregion
}
