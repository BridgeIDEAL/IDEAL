using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CType : BaseEntity
{
    State<CType>[] states;
    StateMachine<CType> stateMachine;
    NavMeshAgent nav;
    public GameObject headObject;
    public CTypeEntityStates CurrentType { private set; get; }
    public float Speed { set { nav.speed = value; } }
    public override void Setup()
    {
        base.Setup();
        CurrentType = CTypeEntityStates.Indifference;
        states = new State<CType>[4];
        //states[(int)EntityStates.Indifference] = new LowerLevelStates.Indifference();
        //states[(int)EntityStates.Watch] = new LowerLevelStates.Watch();
        //states[(int)EntityStates.Chase] = new LowerLevelStates.Chase();
        //states[(int)EntityStates.Patrol] = new LowerLevelStates.Patrol();
        //stateMachine = new StateMachine<LowerLv>();
        //stateMachine.Setup(this, states[(int)CurrentType]);
        nav = GetComponent<NavMeshAgent>();
    }

    public override void UpdateBehavior()
    {
        stateMachine.Execute();
    }
    public void ChangeState(CTypeEntityStates newState)
    {
        CurrentType = newState;
        //stateMachine.ChangeState(states[(int)newState]);
    }

    public void ChasePlayer()
    {
        Vector3 dir = playerObject.transform.position - transform.position;
        nav.SetDestination(playerObject.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), chaseSpeed * Time.deltaTime);
    }

    public void WatchPlayer()
    {
        headObject.transform.LookAt(playerObject.transform);
        /*
        Vector3 dir = playerObject.transform.position - transform.position;
        headObject.transform.rotation=Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), chaseSpeed * Time.deltaTime);*/
    }

    public void ExitWatchPlayer()
    {
        headObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.zero), chaseSpeed * Time.deltaTime);
    }
}
