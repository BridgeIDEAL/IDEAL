using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : class
{
    public abstract void Enter(T entity); // 시작 시 한번만 호출
    public abstract void Execute(T entity); // 상태가 지속되면 계속 호출
    public abstract void Exit(T entity); // 다른 상태로 변할 때 한번만 호출
}
