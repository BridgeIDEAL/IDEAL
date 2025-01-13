using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMEntityGroup : IMovableEntity
{
    [SerializeField] EntitySight[] sights;
    [SerializeField] ForceTalk forceTalk;
    public override void AdditionalInit() 
    {
        int cnt = sights.Length;
        for (int i = 0; i < cnt; i++)
        {
            sights[i].Init(playerHeightTransform);
        }
    }

    public override void AdditionalSetup()
    {
        InteractionConditionConversation _interaction = GetComponentInChildren<InteractionConditionConversation>();
        if (_interaction != null)
            forceTalk.Setup(_interaction, entity_Data);
    }

    public override void TalkEnter()
    {
        SetAnimation(currentType, true);
        int cnt = sights.Length;
        for (int i = 0; i < cnt; i++)
        {
            sights[i].SetRotate(true);
        }
    }
    public override void TalkExit() 
    {
        SetAnimation(currentType, false);
        int cnt = sights.Length;
        for (int i = 0; i < cnt; i++) 
        {
            sights[i].SetRotate(false);
        }
    }
}
