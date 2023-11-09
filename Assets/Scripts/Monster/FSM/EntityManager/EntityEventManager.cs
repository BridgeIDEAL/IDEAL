using System.Collections.Generic;
using UnityEngine;

public class EntityEventManager
{
    public void SendMessage(EventType eventType, GameObject interactionObject = null) // 어떤 상태이든 이벤트 메시지를 받으면 해당 이벤트를 수행
    { 
        BaseEntity entity = interactionObject.GetComponent<BaseEntity>();
        switch (eventType)
        {
            case EventType.RestInteraction: // 휴식공간 진입, 층 변환 시, 상호작용 성공 시
                FSMManager.instance.RestAction.Invoke();
                break;
            case EventType.StartInteraction:
                FSMManager.instance.StartAction(entity.ID);
                break;
            case EventType.SuccessInteraction: // 상호작용 성공 시
                FSMManager.instance.SuccessAction(entity.ID);
                break;
            case EventType.FailInteraction: // 상호작용 실패 시
                FSMManager.instance.FailAction(entity.ID);
                break;
            case EventType.ChaseInteraction: // 상호작용 실패 시
                FSMManager.instance.ChaseAction(entity.ID);
                break;
        }
    }
}
