using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoardClean : CleanSystem
{
    [SerializeField] GameObject graffities;
    [SerializeField] GameObject suddenDeathTrigger;
    [SerializeField] GameObject blackBoardCleanTrigger;

    void Start()
    {
        string _name = gameObject.name;
        if (EntityDataManager.Instance.HaveEventData(_name))
        {
            eventData = EntityDataManager.Instance.GetEventData(_name);
        }
        else
        {
            eventData = new EntityEventData(false, _name);
            EntityDataManager.Instance.AddData(eventData);
        }

        if (!eventData.isDoneEvent)
        {
            Setting();
        }
        else
        {
            Destroy(suddenDeathTrigger);
            Destroy(blackBoardCleanTrigger);
        }                
    }

    public void Setting()
    {
        int randNum = Random.Range(0, 2);
        graffities.SetActive(true);
        interactionClean = graffities.GetComponent<InteractionClean>();
        interactionClean.Clean = this;
    }

    public void SetCleanEvent(bool _isActive)
    {
        suddenDeathTrigger.SetActive(_isActive);
    }

    public override void DoneEvent()
    {
        eventData.isDoneEvent = true;
        SetCleanEvent(false);
    }
}
