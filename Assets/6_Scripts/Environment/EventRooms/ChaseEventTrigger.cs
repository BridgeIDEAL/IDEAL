using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEventTrigger : MonoBehaviour
{
    [SerializeField] ChaseEventType type;
    EventData eventData = null;
    public void Init()
    {
        eventData = EventDataManager.Instance.GetEventData(Enums.GetString(type));
        if (eventData == null)
        {
            EventData _data = new EventData(Enums.GetString(type), false, false);
            EventDataManager.Instance.AddEventData(_data.eventName,_data);
            this.gameObject.SetActive(false);
            eventData = _data;
        }
        else
        {
            if (eventData.isDoneEvent)
            {
                this.gameObject.SetActive(false);
                return;
            }

            if (eventData.isOccurEvent)
                this.gameObject.SetActive(true);
            else
                this.gameObject.SetActive(false);
        }
    }
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventDataManager.Instance.TriggerController.DisableTrigger(type);
            EntityDataManager.Instance.Controller.ActiveChaseEntity(type);
        }
    }
}
