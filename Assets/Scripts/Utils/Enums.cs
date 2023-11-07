using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ATypeEntityStates // �߰�, ��� x
{
    Indifference,
    Interaction
}

public enum BTypeEntityStates // �߰� o, ��� x
{
    Indifference,
    Interaction,
    Aggressive,
    Chase
}

public enum CTypeEntityStates // �߰� x, ��� o
{
    Indifference,
    Interaction,
    Watch
}

public enum DTypeEntityStates // �߰� o, ��� o
{
    Indifference,
    Interaction,
    Watch,
    Aggressive,
    Chase
}

public enum EventType
{
    RestInteraction, // �޽� ������ ���� �� or ���� ������ ��
    StartInteraction, // ��ȣ�ۿ� ������ ��
    SuccessInteraction, // ��� ��ü���� �������� ��
    FailInteraction, // �����ص� �Ѿƿ��� �ʴ� ��ü
    ChaseInteraction // �����ϸ� �Ѿƿ��� ��ü
}