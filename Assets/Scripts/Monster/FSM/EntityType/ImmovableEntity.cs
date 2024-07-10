using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableEntity : BaseEntity
{
    protected EntityStateType currentType;

    protected Animator anim;
    protected EntityState<ImmovableEntity>[] states;
    protected EntityStateMachine<ImmovableEntity> stateMachine;

    public override void Init()
    {
        currentType = EntityStateType.Idle;
        anim = GetComponent<Animator>();
        // States
        states = new EntityState<ImmovableEntity>[(int)EntityStateType.None-1];
        states[(int)EntityStateType.Idle] = new ImmovableEntityStates.IdleState();
        states[(int)EntityStateType.Talk] = new ImmovableEntityStates.TalkState();
        states[(int)EntityStateType.Quiet] = new ImmovableEntityStates.QuietState();
        states[(int)EntityStateType.Penalty] = new ImmovableEntityStates.PenaltyState();
        // StateMachine
        stateMachine = new EntityStateMachine<ImmovableEntity>();
        stateMachine.Init(this, states[(int)currentType]);
        AdditionalInit();
    }

    public virtual void AdditionalInit() { }

    public override void ReceiveMessage(EntityStateType _messageType)
    {
        ChangeState(_messageType);
    }

    public void ChangeState(EntityStateType _changeType)
    {
        currentType = _changeType;
        stateMachine.ChangeState(states[(int)currentType]);
    }

    public override void Execute()
    {
        stateMachine.Execute();
    }

    #region Act Frame
    public virtual void IdleEnter() { }
    public virtual void IdleExecute() { }
    public virtual void IdleExit() { }
    public virtual void TalkEnter() { }
    public virtual void TalkExecute() { }
    public virtual void TalkExit() { }
    public virtual void QuietEnter() { }
    public virtual void QuietExecute() { }
    public virtual void QuietExit() { }
    public virtual void PenaltyEnter() { }
    public virtual void PenaltyExecute() { }
    public virtual void PenaltyExit() { }

    public override void Setup()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
