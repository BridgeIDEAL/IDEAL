using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    State<BType>[] states;
    StateMachine<BType> stateMachine;
    NavMeshAgent nav;
    public GameObject headObject;
    public EntityStates CurrentType { private set; get; }
    public float Speed { set { nav.speed = value; } }
    public override void Setup()
    {
        base.Setup();
        CurrentType = EntityStates.Indifference;
        states = new State<BType>[4];
        states[(int)EntityStates.Indifference] = new BTypeStates.Indifference();
        states[(int)EntityStates.Watch] = new BTypeStates.Watch();
        states[(int)EntityStates.Chase] = new BTypeStates.Chase();
        states[(int)EntityStates.Patrol] = new BTypeStates.Patrol();
        stateMachine = new StateMachine<BType>();
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
    public void RepositionEntity()
    {
        CurrentType = EntityStates.Indifference;
        //transform.position = FSMController.instance.spawnPosition[ID].position;
        nav.SetDestination(FSMManager.instance.spawnPosition[ID].position);
        stateMachine.ChangeState(states[(int)CurrentType]);
    }
}
