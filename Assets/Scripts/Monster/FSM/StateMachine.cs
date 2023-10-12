using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class 
{
	private T ownEntity;  // 상태머신의 소유주
	private State<T> currentState;  // 현재 상태
	private State<T> globalState;   // 전역 상태

	public void Setup(T own, State<T> state)
    {
		ownEntity = own;
		currentState = state;
	}

	public void Execute()
    {
		if (globalState != null)
			globalState.Execute(ownEntity);
		if (currentState != null)
			currentState.Execute(ownEntity);
    }
	public void ChangeState(State<T> newState)
	{
		if (newState == null) return;
		if (currentState != null) currentState.Exit(ownEntity);
		currentState = newState;
		currentState.Enter(ownEntity);
	}

	public void SetGlobalState(State<T> newState)
	{
		globalState = newState;
	}
}
