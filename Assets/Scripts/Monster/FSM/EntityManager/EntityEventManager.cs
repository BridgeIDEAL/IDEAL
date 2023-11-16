using System.Collections.Generic;
using UnityEngine;
using System;
public class EntityEventManager
{
    public Action RestAction; // �޽İ��� ���� ��
    public Action<int> StartAction; // ��ȣ�ۿ� ���� ��
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
                StartAction(entity.ID);
                break;
            case EventType.SuccessInteraction: // ��ȣ�ۿ� ���� ��
                SuccessAction(entity.ID);
                break;
            case EventType.FailInteraction: // ��ȣ�ۿ� ���� ��
                FailAction(entity.ID);
                break;
            case EventType.ChaseInteraction: // ��ȣ�ۿ� ���� ��
                ChaseAction(entity.ID);
                break;
        }
    }
}
