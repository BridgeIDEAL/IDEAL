using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonChaseEntity : BaseEntity
{
    [Header("변수 : 기획")]
    #region Inspector
    [SerializeField] private NonChaseEntityStates currentState = NonChaseEntityStates.Idle;
    #endregion

    [Header("컴포넌트 : 프로그래밍")]
    #region Component
    [SerializeField] private Animator anim;
    #endregion

    protected State<NonChaseEntity>[] states = new State<NonChaseEntity>[4];
    protected StateMachine<NonChaseEntity> stateMachine = new StateMachine<NonChaseEntity>();

    #region BehaviourState
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
    public virtual void ExtraEnter() { }
    public virtual void ExtraExecute() { }
    public virtual void ExtraExit() { }

    #endregion

    public override void Setup()
    {
        base.Setup();
        states[(int)NonChaseEntityStates.Idle] = new NonChaseEntitySpace.IdleState();
        states[(int)NonChaseEntityStates.Talk] = new NonChaseEntitySpace.TalkState();
        states[(int)NonChaseEntityStates.Quiet] = new NonChaseEntitySpace.QuietState();
        states[(int)NonChaseEntityStates.Penalty] = new NonChaseEntitySpace.PenaltyState();
        stateMachine.Setup(this, states[(int)currentState]);
    }

    public override void UpdateExecute() { stateMachine.Execute(); }
    public override void StartConversation() { ChangeState(NonChaseEntityStates.Talk); }
    public override void EndConversation() { ChangeState(NonChaseEntityStates.Idle); }
    public override void BeCalmDown() { ChangeState(NonChaseEntityStates.Idle); }
    public override void BeSilent() { ChangeState(NonChaseEntityStates.Quiet); }

    #region Method
    public void ChangeState(NonChaseEntityStates _newState)
    {
        currentState = _newState;
        stateMachine.ChangeState(states[(int)currentState]);
    }

    #endregion

    #region Animation
    public virtual void SetAnimation(ChaseEntityStates _entityState)
    {
        switch (_entityState)
        {
            case ChaseEntityStates.Idle:
                break;
            case ChaseEntityStates.Talk:
                break;
            case ChaseEntityStates.Quiet:
                break;
            case ChaseEntityStates.Penalty:
                break;
            default:
                break;
        }
    }
    #endregion
}
