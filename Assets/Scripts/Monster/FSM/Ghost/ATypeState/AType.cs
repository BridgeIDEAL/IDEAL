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
    #endregion

    public ATypeEntityStates CurrentType { private set; get; }
    
    public override void Setup(MonsterData.MonsterStat stat)
    {   
        // add component
        anim = GetComponent<Animator>();
        // set information
        gameObject.name = stat.monsterName; 
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = stat.initTransform;
        transform.eulerAngles = stat.initRotation;
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
    public void ChangeState(ATypeEntityStates newState){ CurrentType = newState; stateMachine.ChangeState(states[(int)newState]); }
    public virtual void SetAnimation(ATypeEntityStates entityAnim){ }
}
