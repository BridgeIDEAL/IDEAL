using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDataManager : MonoBehaviour
{
    public static EventDataManager Instance;
    Dictionary<string, EventData> eventGroup = new Dictionary<string, EventData>();

    public ItemSpawnController ItemController { get; set; } = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public EventData GetEventData(string _eventName)
    {
        if(eventGroup.ContainsKey(_eventName))
            return eventGroup[_eventName];
        return null;
    }

    public void AddEventData(string _eventName, EventData _data)
    {
        if (eventGroup.ContainsKey(_eventName))
            return;
    }

    public void ClearEventDatas() { eventGroup.Clear(); }
}
