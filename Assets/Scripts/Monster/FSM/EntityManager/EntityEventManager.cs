using System.Collections.Generic;
using UnityEngine;
using System;
public class EntityEventManager
{
    public bool CanInteraction { get; set; } = true;
    public Action<string> StartConversationAction; // make monsterstate speechless without talk monster (talk monster state = interaction)
    public Action EndConversationAction; // make all monsterstate indifference
    public Action<string> ChaseAction; // make talk monsterstate chase
    public Action<string> SpawnAction; // spawn monster

    public void Init()
    {
        
    }
   
    public void SendStateEventMessage(StateEventType _StateEventType, string _Name = null) 
    { 
        switch (_StateEventType)
        {
            case StateEventType.StartInteraction:
                if (_Name == null)
                    return;
                StartConversationAction(_Name);
                break;
            case StateEventType.EndInteraction: 
                EndConversationAction();
                break;
            case StateEventType.ChaseInteraction: 
                if (_Name == null)
                    return;
                ChaseAction(_Name);
                break;
        }
    }

    public void SendSpawnEventMessage(string _Name) { SpawnAction(_Name); }
}
