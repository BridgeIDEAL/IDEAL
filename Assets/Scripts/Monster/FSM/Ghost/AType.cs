using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AType : BaseEntity
{
    State<AType>[] states;
    StateMachine<AType> stateMachine;
    NavMeshAgent nav;

    public ATypeEntityStates CurrentType { private set; get; }
    public float Speed { set { nav.speed = value; } }
    public override void Setup()
    {
        base.Setup();
        CurrentType = ATypeEntityStates.Indifference;
        states = new State<AType>[2];
        states[(int)ATypeEntityStates.Indifference] = new ATypeStates.Indifference();
        states[(int)ATypeEntityStates.Interaction] = new ATypeStates.Interaction();
        stateMachine = new StateMachine<AType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
        nav = GetComponent<NavMeshAgent>();
    }

    public override void UpdateBehavior()
    {
        stateMachine.Execute();
    }
    public void ChangeState(ATypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    public bool SendMessage(bool interaction)
    {
        return stateMachine.SendMessage(interaction);
    }

    protected override void OnDrawGizmos() { }
}
