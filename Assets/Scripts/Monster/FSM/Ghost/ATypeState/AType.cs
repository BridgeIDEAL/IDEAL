using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AType : BaseEntity
{
    #region Component
    protected Animator anim;
    protected State<AType>[] states;
    protected StateMachine<AType> stateMachine;
    public ATypeEntityStates CurrentType { protected set; get; }
    #endregion

    #region StateBehavior
    public virtual void IndifferenceEnter() { SetAnimation(CurrentType); }
    public virtual void IndifferenceExecute() { }
    public virtual void IndifferenceExit() { }
    public virtual void InteractionEnter() { SetAnimation(CurrentType); }
    public virtual void InteractionExecute() { }
    public virtual void InteractionExit() { }
    public virtual void SpeechlessEnter() { SetAnimation(CurrentType); }
    public virtual void SpeechlessExecute() { }
    public virtual void SpeechlessExit() { }
    #endregion

    #region Virtual
    public virtual void SetAnimation(ATypeEntityStates entityAnim) { }
    #endregion

    #region Override
    public override void Setup(MonsterData.MonsterStat stat)
    {
        // set information
        gameObject.name = stat.monsterName; 
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = stat.initTransform;
        transform.eulerAngles = stat.initRotation;
        // add component
        AdditionalSetup();
        // set statemachine
        CurrentType = ATypeEntityStates.Indifference;
        states = new State<AType>[3];
        states[(int)ATypeEntityStates.Indifference] = new ATypeStates.Indifference();
        states[(int)ATypeEntityStates.Interaction] = new ATypeStates.Interaction();
        states[(int)ATypeEntityStates.Speechless] = new ATypeStates.Speechless();
        stateMachine = new StateMachine<AType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }
    public override void UpdateBehavior() { stateMachine.Execute(); }
    #endregion

    #region Method
    public void ChangeState(ATypeEntityStates newState){ CurrentType = newState; stateMachine.ChangeState(states[(int)newState]); }
    #endregion
}
