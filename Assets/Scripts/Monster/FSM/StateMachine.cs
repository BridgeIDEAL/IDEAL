using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class 
{
	private T ownEntity;  // ���¸ӽ��� ������
	private State<T> currentState;  // ���� ����
	private State<T> globalState;   // ���� ����

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
