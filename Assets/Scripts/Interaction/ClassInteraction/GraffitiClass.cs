using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraffitiClass : MonoBehaviour
{
    [SerializeField] EventNames eventName;
    [SerializeField] GameObject deathTrigger;
    EventData eventData = null;

    private void Start()
    {
        if (EventDataManager.Instance.RingAfterSchoolBell)
        {
            this.gameObject.SetActive(false);
            return;
        }

        eventData = EventDataManager.Instance.GetEventData(Enums.GetString(eventName));
        if (eventData== null)
        {
            EventData _eventData = new EventData(Enums.GetString(eventName), false, false);
            EventDataManager.Instance.AddEventData(_eventData.eventName, _eventData);
            eventData= _eventData;
        }
        else
        {
            if(eventData.isDoneEvent)
            {
                this.gameObject.SetActive(false);
                return;
            }
        }
        deathTrigger.SetActive(false);
    }

    public void EraseGraffiti()
    {
        eventData.isDoneEvent = true;
        this.gameObject.SetActive(false);
    }

    public void ActiveGraffitiEvent() { deathTrigger.SetActive(true); }
}
