using System.Collections.Generic;
using UnityEngine;
using System;
public class EntityEventManager
{
    #region Component
    private bool isChase;
    private bool canInteraction;
    public bool IsChase 
    {
        get { return isChase; }
        private set { isChase = value; } 
    } 
    public bool CanInteraction
    {
        get { return canInteraction; }
        private set { canInteraction = value; } 
    } 
    public Action<string> StartConversationAction; // make monsterstate speechless without talk monster (talk monster state = interaction)
    public Action EndConversationAction; // make all monsterstate indifference
    public Action<string> ChaseAction; // make talk monsterstate chase
    public Action<string> SpawnAction; // spawn monster
    #endregion

    #region Method
    public void Init()
    {
        IsChase = false;
        CanInteraction = true;
    }
   
    public void SendStateEventMessage(StateEventType _stateEventType, string _name = null) 
    { 
        switch (_stateEventType)
        {
            case StateEventType.StartConversation:
                if (_name == null)
                    return;
                CanInteraction = false;
                StartConversationAction(_name);
                break;
            case StateEventType.EndConversation:
                if (!IsChase)
                {
                    CanInteraction = true;
                    EndConversationAction();
                }   
                break;
            case StateEventType.Chase: 
                if (_name == null)
                    return;
                IsChase = true;
                CanInteraction = false;
                ChaseAction(_name);
                break;
            case StateEventType.Rest:
                IsChase = false;
                EndConversationAction();
                break;
            case StateEventType.Spawn:
                if (_name == null)
                    return;
                SpawnAction(_name);
                break;
        }
    }
    #endregion
}
