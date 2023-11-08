using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DType : BaseEntity
{
    State<DType>[] states;
    StateMachine<DType> stateMachine;
    NavMeshAgent nav;
    Animator anim;
    public DTypeEntityStates CurrentType { private set; get; }
    public override void Setup()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        base.Setup();
        CurrentType = DTypeEntityStates.Indifference;
        states = new State<DType>[6];
        states[(int)DTypeEntityStates.Indifference] = new DTypeStates.Indifference();
        states[(int)DTypeEntityStates.Interaction] = new DTypeStates.Interaction();
        states[(int)DTypeEntityStates.Watch] = new DTypeStates.Watch();
        states[(int)DTypeEntityStates.Aggressive] = new DTypeStates.Aggressive();
        states[(int)DTypeEntityStates.Chase] = new DTypeStates.Chase();
        stateMachine = new StateMachine<DType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
        nav.speed = chaseSpeed;
    }

    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void RestInteraction()
    {
        nav.ResetPath();
        transform.rotation = InitTransform.rotation;
        transform.position = InitTransform.position;
        ChangeState(DTypeEntityStates.Indifference);

    }
    public override void StartInteraction() { ChangeState(DTypeEntityStates.Interaction); }
    public override void FailInteraction() { ChangeState(DTypeEntityStates.Indifference); }
    public override void SuccessInteraction() { ChangeState(DTypeEntityStates.Indifference); }
    public override void ChaseInteraction() { ChangeState(DTypeEntityStates.Aggressive); }

    public void ChangeState(DTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    public void ChasePlayer()
    {
        Vector3 dir = playerObject.transform.position - transform.position;
        nav.SetDestination(playerObject.transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), chaseSpeed * Time.deltaTime);
    }
    public void WatchPlayer()
    {
        float dist = (playerObject.transform.position - transform.position).magnitude;
        Vector3 dir = playerObject.transform.position - transform.position;
        if (sightDistance >= dist)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), chaseSpeed * Time.deltaTime);
        else
            ChangeState(DTypeEntityStates.Indifference);
    }
    public void SetAnimation(DTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case DTypeEntityStates.Indifference:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case DTypeEntityStates.Watch:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case DTypeEntityStates.Interaction:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case DTypeEntityStates.Aggressive:
                anim.CrossFade("ATTACK", 0.2f);
                break;
            case DTypeEntityStates.Chase:
                anim.CrossFade("CHASE", 0.2f);
                break;
        }
    }
}
