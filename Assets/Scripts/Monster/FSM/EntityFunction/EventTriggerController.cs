using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerController : MonoBehaviour
{
    [SerializeField] ChaseEventTrigger[] chaseEventTriggerGroup;
    [SerializeField] LastObjects[] lastObjects;
    public void Start()
    {
        EventDataManager.Instance.TriggerController = this;

        if (EventDataManager.Instance.RingAfterSchoolBell)
        {
            // To Do ~~ Sound
            //IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.LastRunStart();
            RenderSettings.skybox = lastBoxMat;
        }

        int chaseTriggerCnt = chaseEventTriggerGroup.Length;
        for(int i = 0; i < chaseTriggerCnt; i++)
        {
            chaseEventTriggerGroup[i].Init();
        }
    }

    /// <summary>
    /// 0:2F Girl, 1:3F Male Teachre
    /// </summary>
    /// <param name="_idx"></param>
    /// <returns></returns>
    
    [SerializeField] Material lastBoxMat;

    public void TriggerLastEvent()
    {
        ProgressManager.Instance.UpdateCheckList(401, 1);

        EventDataManager.Instance.RingAfterSchoolBell = true;
        EntityDataManager.Instance.Controller.InActiveInteractionEntities();

        int lastObjectsCnt = lastObjects.Length;
        for (int i = 0; i < lastObjectsCnt; i++)
        {
            lastObjects[i].gameObject.SetActive(true);
            lastObjects[i].EnableObject();
        }

        EnableTrigger(ChaseEventType.Last1F_APrincipal);
        EnableTrigger(ChaseEventType.Last1F_BPrincipal);
        EnableTrigger(ChaseEventType.Last1F_Guard);
        EnableTrigger(ChaseEventType.Last3F_StudentOfHeadTeacher);
        EnableTrigger(ChaseEventType.Last3F_GirlStudent);

        RenderSettings.skybox = lastBoxMat;
        // To Do ~~ Audio
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.LastRunStart();
    }

    /// <summary>
    /// Call when you get event : Ring Bell or Jump Scare
    /// </summary>
    /// <param name="type"></param>
    public void EnableTrigger(ChaseEventType type)
    {
        EventData data = EventDataManager.Instance.GetEventData(Enums.GetString(type));
        if (data == null) return;
        data.isOccurEvent = true;
        chaseEventTriggerGroup[(int)type].gameObject.SetActive(true);
    }

    /// <summary>
    /// Call When you enter trigger
    /// </summary>
    /// <param name="type"></param>
    public void DisableTrigger(ChaseEventType type)
    {
        EventData data = EventDataManager.Instance.GetEventData(Enums.GetString(type));
        if (data == null) return;
        data.isDoneEvent = true;
        chaseEventTriggerGroup[(int)type].gameObject.SetActive(false);
    }
}
