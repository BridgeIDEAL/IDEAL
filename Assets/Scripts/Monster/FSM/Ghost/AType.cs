using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AType : BaseEntity
{
    State<AType>[] states;
    StateMachine<AType> stateMachine;
    Animator anim;
    public ATypeEntityStates CurrentType { private set; get; }
    public override void Setup(MonsterData.MonsterStat stat)
    {   
        // add component
        anim = GetComponent<Animator>();
        // set information
        playerObject = GameObject.FindGameObjectWithTag("Player");
        transform.position = stat.initTransform;
        transform.eulerAngles = stat.initRotation;
        gameObject.name = stat.name;
        // set statemachine
        CurrentType = ATypeEntityStates.Indifference;
        states = new State<AType>[2];
        states[(int)ATypeEntityStates.Indifference] = new ATypeStates.Indifference();
        states[(int)ATypeEntityStates.Interaction] = new ATypeStates.Interaction();
        stateMachine = new StateMachine<AType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
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
    public void SetAnimation(ATypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case ATypeEntityStates.Indifference:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case ATypeEntityStates.Interaction:
                anim.CrossFade("IDLE", 0.2f);
                break;
        }
    }
}
