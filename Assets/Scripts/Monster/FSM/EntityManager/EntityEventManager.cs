using System.Collections.Generic;
using UnityEngine;

public class EntityEventManager
{
    public void SendMessage(EventType eventType, GameObject interactionObject = null) // � �����̵� �̺�Ʈ �޽����� ������ �ش� �̺�Ʈ�� ����
    { 
        BaseEntity entity = interactionObject.GetComponent<BaseEntity>();
        switch (eventType)
        {
            case EventType.RestInteraction: // �޽İ��� ����, �� ��ȯ ��, ��ȣ�ۿ� ���� ��
                FSMManager.instance.RestAction.Invoke();
                break;
            case EventType.StartInteraction:
                FSMManager.instance.StartAction(entity.ID);
                break;
            case EventType.SuccessInteraction: // ��ȣ�ۿ� ���� ��
                FSMManager.instance.SuccessAction(entity.ID);
                break;
            case EventType.FailInteraction: // ��ȣ�ۿ� ���� ��
                FSMManager.instance.FailAction(entity.ID);
                break;
            case EventType.ChaseInteraction: // ��ȣ�ۿ� ���� ��
                FSMManager.instance.ChaseAction(entity.ID);
                break;
        }
    }
}
