using System.Collections.Generic;
using UnityEngine;
using System;
public class EntityEventManager
{
    public bool CanInteraction { get; set; } = true;
    public Action RestAction; // �޽İ��� ���� ��
    public Action<int> ConversationAction; // ��ȣ�ۿ� ���� ��
    public Action<int> SuccessAction; // ��ȣ�ۿ� ���� ��
    public Action<int> FailAction; // ��ȣ�ۿ� ���� ��
    public Action<int> ChaseAction; // �߰� ���� ��

    public void Init()
    {
        
    }
   
    public void SendMessage(EventType eventType, GameObject interactionObject = null) // � �����̵� �̺�Ʈ �޽����� ������ �ش� �̺�Ʈ�� ����
    { 
        BaseEntity entity = interactionObject.GetComponent<BaseEntity>();
        switch (eventType)
        {
            case EventType.RestInteraction: // �޽İ��� ����, �� ��ȯ ��, ��ȣ�ۿ� ���� ��
                RestAction.Invoke();
                break;
            case EventType.StartInteraction:
                if (entity == null)
                    return;
                ConversationAction(entity.ID);
                break;
            case EventType.SuccessInteraction: // ��ȣ�ۿ� ���� ��
                if (entity == null)
                    return;
                SuccessAction(entity.ID);
                break;
            case EventType.FailInteraction: // ��ȣ�ۿ� ���� ��
                if (entity == null)
                    return;
                FailAction(entity.ID);
                break;
            case EventType.ChaseInteraction: // ��ȣ�ۿ� ���� ��
                if (entity == null)
                    return;
                ChaseAction(entity.ID);
                break;
        }
    }
}
