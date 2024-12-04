using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateMachine<T> where T : BaseEntity 
{
	private T ownEntity;  
	private EntityState<T> currentState;  
	private EntityState<T> globalState;   

	public void Init(T _own, EntityState<T> _state)
    {
		ownEntity = _own;
		currentState = null;
		globalState = null;
		ChangeState(_state);
	}

	public void Execute()
    {
		if (globalState != null)
			globalState.Execute(ownEntity);
		if (currentState != null)
			currentState.Execute(ownEntity);	
    }
	public void ChangeState(EntityState<T> _changeState)
	{
		if (_changeState == null) return;
		if (currentState != null) currentState.Exit(ownEntity);
		currentState = _changeState;
		currentState.Enter(ownEntity);
	}

	public void SetGlobalState(EntityState<T> _globalState)
	{
		globalState = _globalState;
	}

}
