using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableEntityGroup : ImmovableEntity
{
    int lookPlayerCnt = 0;
    Quaternion[] groupRotateValues;
    [SerializeField] Transform lookTarget;
    [SerializeField] StandLookPlayer[] lookPlayers;
    [SerializeField] ForceTalk forceTalk;
    public override void AdditionalInit() 
    {
        lookPlayerCnt = lookPlayers.Length;
        groupRotateValues = new Quaternion[lookPlayerCnt];

        for (int i = 0; i < lookPlayerCnt; i++)
        {
            groupRotateValues[i] = lookPlayers[i].transform.rotation;
        }

        for (int i=0; i< lookPlayerCnt; i++)
        {
            lookPlayers[i].GazeTarget(lookTarget);
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
        for (int i = 0; i < lookPlayerCnt; i++)
        {
            lookPlayers[i].GazePlayer(controller.lookTransform);
        }
    }
    public override void TalkExit() 
    {
        SetAnimation(currentType, false);
        for (int i = 0; i < lookPlayerCnt; i++)
        {
            lookPlayers[i].GazeDefaultTarget(groupRotateValues[i], lookTarget);
        }
    }
}
