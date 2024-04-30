using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityEventManager
{
    private bool isChaseDown = false;
    public bool IsChaseDown
    {
        get
        {
            return isChaseDown;
        }
        set
        {
            isChaseDown = value;
        }
    }
    public Func<string, BaseEntity> SearchEntity;
    public Action<string> SpawnEntity;
    public Action<string> DespawnEntity;

    public Action BroadCastCalmDown;
    public Action<string> BroadCastStartConversation;
    public Action<string> BroadCastEndConversation;
    public Action<string> BroadCastChase;
}
