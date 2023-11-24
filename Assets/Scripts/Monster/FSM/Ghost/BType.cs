using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    State<BType>[] states;
    StateMachine<BType> stateMachine;
    NavMeshAgent nav;
    public BTypeEntityStates CurrentType { private set; get; }
    public override void Setup(MonsterData.MonsterStat stat)
    {
        //base.Setup();
        nav.speed = stat.speed;
        speed = stat.speed;
        InitTransform.position = stat.initTransform;
        InitTransform.eulerAngles = stat.initRotation;
        gameObject.name = stat.name;
        CurrentType = BTypeEntityStates.Indifference;
        states = new State<BType>[4];
        states[(int)BTypeEntityStates.Indifference] = new BTypeStates.Indifference();
        states[(int)BTypeEntityStates.Interaction] = new BTypeStates.Interaction();
        states[(int)BTypeEntityStates.Aggressive] = new BTypeStates.Aggressive();
        states[(int)BTypeEntityStates.Chase] = new BTypeStates.Chase();
        stateMachine = new StateMachine<BType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
    }

    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void RestInteraction()
    {
        transform.position = InitTransform.position;
        nav.SetDestination(InitTransform.position);
        ChangeState(BTypeEntityStates.Indifference);
    }
    public override void StartInteraction() { ChangeState(BTypeEntityStates.Interaction); }
    public override void SuccessInteraction() { ChangeState(BTypeEntityStates.Interaction); } 
    public override void FailInteraction() { ChangeState(BTypeEntityStates.Indifference); }
    public override void ChaseInteraction() { ChangeState(BTypeEntityStates.Aggressive); }
   
    public void ChangeState(BTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    
    public void ChasePlayer()
    {
        Vector3 dir = playerObject.transform.position - transform.position;
        nav.SetDestination(playerObject.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), speed * Time.deltaTime);
    }
}
