using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
