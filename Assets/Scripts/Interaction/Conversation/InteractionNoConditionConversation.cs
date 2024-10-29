using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNoConditionConversation : InteractionConditionConversation
{
    [SerializeField] EntityDialogueType dialogueType = EntityDialogueType.CanOnlySayOnce;

    int defaultLayer = 0;
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

        Entity _entityData = GetComponent<BaseEntity>().Entity_Data;
        if (_entityData != null)
        {
            SetTalkType(_entityData);
        }
    }

    public void SetTalkType(Entity _entityData)
    {
        switch (dialogueType)
        {
            case EntityDialogueType.CanOnlySayOnce:
                _entityData.isSpawn = false;
                gameObject.layer = defaultLayer;
                break;
            case EntityDialogueType.CanSayMayTimes:
                break;
        }
    }
    #endregion
}
