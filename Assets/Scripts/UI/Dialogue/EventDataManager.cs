using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDataManager : MonoBehaviour
{
    public static EventDataManager Instance;

    public bool RingAfterSchoolBell { get; set; } = false;
    Dictionary<string, EventData> eventGroup = new Dictionary<string, EventData>();


    #region Relate Controller
    public ItemSpawnController ItemController { get; set; } = null;
    public EventTriggerController TriggerController { get; set; } = null;
    #endregion

    #region Use For Principal Teleport
    private EntityNoticeManager notice = new EntityNoticeManager();
    public EntityNoticeManager Notice { get { return notice; } }
    #endregion

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
        eventGroup.Add(_eventName,_data);
        //Debug.Log(_eventName + "데이터 추가 완료~~");
    }

    public void ClearEventDatas() { eventGroup.Clear(); }
}
