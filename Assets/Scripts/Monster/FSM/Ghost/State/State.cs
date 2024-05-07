using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> where T : BaseEntity
{
    public abstract void Enter(T entity); 
    public abstract void Execute(T entity); 
    public abstract void Exit(T entity); 
}
