using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Student : BaseEntity
{
    State<Student>[] states;
    StateMachine<Student> stateMachine;
    NavMeshAgent nav;
    public float chaseSpeed;
    public float patrolSpeed;
    public EntityStates CurrentType { private set; get; }
    public float Speed { set { nav.speed = value; }}
    public override void Setup()
    {
        base.Setup();
        CurrentType = EntityStates.Indifference;
        states = new State<Student>[4];
        states[(int)EntityStates.Indifference] = new StudentState.Indifference();
        states[(int)EntityStates.Watch] = new StudentState.Watch();
        states[(int)EntityStates.Chase] = new StudentState.Chase();
        states[(int)EntityStates.Patrol] = new StudentState.Patrol();
        stateMachine = new StateMachine<Student>();
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
}
