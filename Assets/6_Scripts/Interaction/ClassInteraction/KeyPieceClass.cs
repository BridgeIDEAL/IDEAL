using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPieceClass : MonoBehaviour
{
    [SerializeField] EventItemNames pickupEventName;
    [SerializeField] Transform[] spawnPositions;
    EventData eventData = null;

    private void Start()
    {
        if (EventDataManager.Instance.RingAfterSchoolBell)
        {
            this.gameObject.SetActive(false);
            return;
        }

        eventData = EventDataManager.Instance.GetEventData(Enums.GetString(pickupEventName));
        if (eventData == null)
        {
            int randomIndex = Random.Range(0,spawnPositions.Length);
            EventData _eventData = new EventData(Enums.GetString(pickupEventName), false, true, spawnPositions[randomIndex].position);
            EventDataManager.Instance.AddEventData(_eventData.eventName, _eventData);
            EventDataManager.Instance.ItemController.DecideActiveItemState(pickupEventName,true,spawnPositions[randomIndex].position);
            eventData = _eventData;
        }
    }
}
