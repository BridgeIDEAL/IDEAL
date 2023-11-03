using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : class
{
    public abstract void Enter(T entity); // ���� �� �ѹ��� ȣ��
    public abstract void Execute(T entity); // ���°� ���ӵǸ� ��� ȣ��
    public abstract void Exit(T entity); // �ٸ� ���·� ���� �� �ѹ��� ȣ��
}
