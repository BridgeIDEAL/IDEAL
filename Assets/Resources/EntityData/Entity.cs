using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Entity Dialogue Data
[System.Serializable]
public class Entity 
{
    public string speakerName;
    public int speakIndex;
    public bool isSpawn;
}

[System.Serializable]
public class EntityData
{
    public List<Entity> entities;
}

public class EntityEventData
{
    public bool isDoneEvent = false;
    public string eventName;

    public EntityEventData(bool _done, string _name)
    {
        isDoneEvent = _done;
        eventName = _name;
    }
}