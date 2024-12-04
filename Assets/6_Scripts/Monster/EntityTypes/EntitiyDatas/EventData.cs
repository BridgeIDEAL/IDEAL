using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData 
{
    public bool isOccurEvent = false; // use for item 
    public bool isDoneEvent = false; // check done event : all item/event
    public string eventName; // use for key
    public Vector3 position = Vector3.zero; // use for item
    
    public EventData(string _name)
    {
        eventName = _name;
        isOccurEvent = false;
        isDoneEvent = false;
    }

    public EventData(string _name, bool _isDoneEvent)
    {
        eventName = _name;
        isDoneEvent= _isDoneEvent;
    }

    public EventData(bool _isOccurEvent, string _eventName)
    {
        this.isOccurEvent = _isOccurEvent;
        this.eventName = _eventName;
    }

    public EventData(string _name, bool _isDoneEvent, bool _isOccurEvent)
    {
        eventName = _name;
        isDoneEvent = _isDoneEvent;
        isOccurEvent = _isOccurEvent;
    }
    public EventData(string _name, bool _isDoneEvent, bool _isOccurEvent, Vector3 _position)
    {
        eventName = _name;
        isDoneEvent = _isDoneEvent;
        isOccurEvent = _isOccurEvent;
        position = _position;
    }

    // Later Delete
    public void ResetData() { isDoneEvent = false; isOccurEvent = false;  }
}