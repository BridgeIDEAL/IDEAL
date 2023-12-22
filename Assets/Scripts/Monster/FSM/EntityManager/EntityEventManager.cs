using System.Collections.Generic;
using UnityEngine;
using System;
public class EntityEventManager
{
    public bool CanInteraction { get; set; } = true;
    public Action RestAction; 
    public Action<string> StartConversationAction; 
    public Action<string> EndConversationAction;
    public Action<string> ChaseAction;

    public void Init()
    {
        
    }
   
    public void SendMessage(EventType eventType, GameObject interactionObject = null) 
    { 
        BaseEntity entity = interactionObject.GetComponent<BaseEntity>();
        switch (eventType)
        {
            case EventType.RestInteraction:
                RestAction.Invoke();
                break;
            case EventType.StartInteraction:
                if (entity == null)
                    return;
                StartConversationAction(entity.monsterName);
                break;
            case EventType.EndInteraction: 
                if (entity == null)
                    return;
                EndConversationAction(entity.monsterName);
                break;
            case EventType.ChaseInteraction: 
                if (entity == null)
                    return;
                ChaseAction(entity.monsterName);
                break;
        }
    }
}
