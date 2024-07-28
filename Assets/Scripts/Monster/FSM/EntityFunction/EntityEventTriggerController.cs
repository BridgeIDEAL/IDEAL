using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEventTriggerController : MonoBehaviour
{
    public GameObject lastTriggerObject;
    [SerializeField, Tooltip("0 : Jump2FGirl, 1 : Jump3FHeadOfStudentTeacher")] JumpSpace[] jumpSpaces;

    public void Start()
    {
        EntityDataManager.Instance.EventTriggerController = this;

        if (EntityDataManager.Instance.IsLastEvent)
        {
            lastTriggerObject.SetActive(true);
        }
    }

    public JumpSpace GetJumpSpace(int _idx)
    {
        return jumpSpaces[_idx];
    }
}
