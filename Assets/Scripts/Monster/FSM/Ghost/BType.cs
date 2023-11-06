using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    Transform respawnPosition;
    State<BType>[] states;
    StateMachine<BType> stateMachine;
    NavMeshAgent nav;
    public BTypeEntityStates CurrentType { private set; get; }
    public float Speed { set { nav.speed = value; } }
    public override void Setup()
    {
        base.Setup();
        respawnPosition = gameObject.transform;
        CurrentType = BTypeEntityStates.Indifference;
        states = new State<BType>[4];
        states[(int)BTypeEntityStates.Indifference] = new BTypeStates.Indifference();
        states[(int)BTypeEntityStates.Interaction] = new BTypeStates.Interaction();
        states[(int)BTypeEntityStates.Aggressive] = new BTypeStates.Aggressive();
        states[(int)BTypeEntityStates.Chase] = new BTypeStates.Chase();
        stateMachine = new StateMachine<BType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
        nav = GetComponent<NavMeshAgent>();
    }

    public override void UpdateBehavior()
    {
        stateMachine.Execute();
    }
    public void ChangeState(BTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    public bool SendMessage(bool interaction)
    {
        return stateMachine.SendMessage(interaction);
    }

    public void ChasePlayer()
    {
        Vector3 dir = playerObject.transform.position - transform.position;
        nav.SetDestination(playerObject.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), chaseSpeed * Time.deltaTime);
    }

    public void RepositionEntity()
    {
        CurrentType = BTypeEntityStates.Indifference;
        //transform.position = respawnPosition.position;
        transform.rotation = respawnPosition.rotation;
        nav.SetDestination(respawnPosition.position);
        stateMachine.ChangeState(states[(int)CurrentType]);
    }
}
