using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetKeyBundleItem : InteractionGetKeyBundle
{
    [SerializeField] Jump3FHeadOfStudentTeacher jump3FTeacher;
    protected override void ActInteraction()
    {
        base.ActInteraction();
        if (jump3FTeacher == null)
            jump3FTeacher = EntityDataManager.Instance.EventTriggerController.GetJumpSpace(1).gameObject.GetComponent<Jump3FHeadOfStudentTeacher>();
        jump3FTeacher.CanActive = true;
    }
}
