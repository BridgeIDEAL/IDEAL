using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AType : BaseEntity
{
    State<AType>[] states;
    StateMachine<AType> stateMachine;
    NavMeshAgent nav;

    public EntityStates CurrentType { private set; get; }
    public float Speed { set { nav.speed = value; } }
    public override void Setup()
    {
        base.Setup();
        CurrentType = EntityStates.Indifference;
        states = new State<AType>[4];
        states[(int)EntityStates.Indifference] = new ATypeStates.Indifference();
        states[(int)EntityStates.Watch] = new ATypeStates.Watch();
        states[(int)EntityStates.Chase] = new ATypeStates.Chase();
        states[(int)EntityStates.Patrol] = new ATypeStates.Patrol();
        stateMachine = new StateMachine<AType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
        nav = GetComponent<NavMeshAgent>();
    }

    public override void UpdateBehavior()
    {
        stateMachine.Execute();
    }
    public void ChangeState(EntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void ChasePlayer()
    {
        Vector3 dir = playerObject.transform.position - transform.position;
        nav.SetDestination(playerObject.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), chaseSpeed * Time.deltaTime);
    }

    public void RepositionEntity()
    {
        CurrentType = EntityStates.Indifference;
        stateMachine.ChangeState(states[(int)CurrentType]);
    }
}
