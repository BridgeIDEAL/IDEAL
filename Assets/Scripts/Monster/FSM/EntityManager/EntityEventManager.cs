using System.Collections.Generic;
using UnityEngine;
using System;
public class EntityEventManager
{
    public bool CanInteraction { get; set; } = true;
    public Action RestAction; 
    public Action<int> ConversationAction; 
    public Action<int> SuccessAction;
    public Action<int> FailAction; 
    public Action<int> ChaseAction;

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
                ConversationAction(entity.ID);
                break;
            case EventType.SuccessInteraction: 
                if (entity == null)
                    return;
                SuccessAction(entity.ID);
                break;
            case EventType.FailInteraction: 
                if (entity == null)
                    return;
                FailAction(entity.ID);
                break;
            case EventType.ChaseInteraction: 
                if (entity == null)
                    return;
                ChaseAction(entity.ID);
                break;
        }
    }
}
