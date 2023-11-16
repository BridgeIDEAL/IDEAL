using System.Collections.Generic;
using UnityEngine;
using System;
public class EntityEventManager
{
    public Action RestAction; // 휴식공간 진입 시
    public Action<int> StartAction; // 상호작용 시작 시
    public Action<int> SuccessAction; // 상호작용 성공 시
    public Action<int> FailAction; // 상호작용 실패 시
    public Action<int> ChaseAction; // 추격 진행 시

    public void Init()
    {

    }
   
    public void SendMessage(EventType eventType, GameObject interactionObject = null) // 어떤 상태이든 이벤트 메시지를 받으면 해당 이벤트를 수행
    { 
        BaseEntity entity = interactionObject.GetComponent<BaseEntity>();
        switch (eventType)
        {
            case EventType.RestInteraction: // 휴식공간 진입, 층 변환 시, 상호작용 성공 시
                RestAction.Invoke();
                break;
            case EventType.StartInteraction:
                StartAction(entity.ID);
                break;
            case EventType.SuccessInteraction: // 상호작용 성공 시
                SuccessAction(entity.ID);
                break;
            case EventType.FailInteraction: // 상호작용 실패 시
                FailAction(entity.ID);
                break;
            case EventType.ChaseInteraction: // 상호작용 실패 시
                ChaseAction(entity.ID);
                break;
        }
    }
}
