using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEventTriggerController : MonoBehaviour
{
    [SerializeField] LastFence lastFence;
    public GameObject lastTriggerObject;
    [SerializeField, Tooltip("0 : Jump2FGirl, 1 : Jump3FHeadOfStudentTeacher")] JumpSpace[] jumpSpaces;

    public void Start()
    {
        EntityDataManager.Instance.EventTriggerController = this;

        if (EntityDataManager.Instance.IsLastEvent)
        {
            lastTriggerObject.SetActive(true);
        }
        else
        {
            lastTriggerObject.SetActive(false);
        }
    }

    /// <summary>
    /// 0:2F Girl, 1:3F Male Teachre
    /// </summary>
    /// <param name="_idx"></param>
    /// <returns></returns>
    public JumpSpace GetJumpSpace(int _idx)
    {
        return jumpSpaces[_idx];
    }

    public void TriggerLastEvent()
    {
        GameObject go = GameObject.Find("SchoolFrontDoors");
        go.GetComponent<LastDoorOpen>().OpenFrontDoors();
        EntityDataManager.Instance.IsLastEvent = true;
        EntityDataManager.Instance.Controller.InActiveInteractionEntities();
        lastTriggerObject.SetActive(true);

        if (lastFence != null)
            lastFence.Active();
        // To Do ~~ Audio
    }
}
