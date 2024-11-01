using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetKeyBundleItem : InteractionGetKeyBundle
{
    protected override void ActInteraction()
    {
        base.ActInteraction();
        EventDataManager.Instance.TriggerController.EnableTrigger(ChaseEventType.Jump3F_StudentOfHeadTeacher);
    }
}
