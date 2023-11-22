using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AType : BaseEntity
{
    NavMeshAgent nav;
    State<AType>[] states;
    StateMachine<AType> stateMachine;
    public ATypeEntityStates CurrentType { private set; get; }
    public override void Setup(MonsterData.MonsterStat stat)
    {
        //base.Setup();
        nav.speed = stat.speed;
        speed = stat.speed;
        InitTransform.position = stat.initTransform;
        InitTransform.eulerAngles = stat.initRotation;
        gameObject.name = stat.name;
        CurrentType = ATypeEntityStates.Indifference;
        states = new State<AType>[2];
        states[(int)ATypeEntityStates.Indifference] = new ATypeStates.Indifference();
        states[(int)ATypeEntityStates.Interaction] = new ATypeStates.Interaction();
        stateMachine = new StateMachine<AType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
        nav = GetComponent<NavMeshAgent>();
    }

    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void RestInteraction() { ChangeState(ATypeEntityStates.Indifference); }
    public override void StartInteraction() { ChangeState(ATypeEntityStates.Interaction); }
    public override void FailInteraction() { ChangeState(ATypeEntityStates.Indifference); }
    public override void SuccessInteraction(){ ChangeState(ATypeEntityStates.Indifference); }
    
    public void ChangeState(ATypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
}
