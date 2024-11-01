using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetSystem : MonoBehaviour
{
    //Vector3 spawnPosition = new Vector3(0, -0.2f, -0.2f);
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Transform rotateInteraction;
    [SerializeField] EventItemNames pickupEventName;
    EventData eventData = null;

    private void Start()
    {
        if (EntityDataManager.Instance.IsLastEvent)
        {
            this.gameObject.SetActive(false);
            return;
        }

        eventData = EventDataManager.Instance.GetEventData(Enums.GetString(pickupEventName));
        if (eventData == null)
        {
            rotateInteraction.gameObject.SetActive(true);
            EventData _eventData = new EventData(Enums.GetString(pickupEventName), false, true, spawnPosition + rotateInteraction.position);
            EventDataManager.Instance.AddEventData(_eventData.eventName, _eventData);
            EventDataManager.Instance.ItemController.DecideActiveItemState(pickupEventName, true, spawnPosition + rotateInteraction.position);
            eventData = _eventData;
        }
    }
}
