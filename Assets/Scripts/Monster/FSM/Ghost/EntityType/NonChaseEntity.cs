using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonChaseEntity : BaseEntity
{
    #region NonChaseEntity Common Variable
    [Header("NonChaseEntity Common Variable")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected Transform eyeTransform; // Detect Transform
    [SerializeField] protected ScriptableNonChaseEntity entityData;
    // Player Variable
    protected LayerMask playerLayer = 3;
    protected Transform playerTransform;
    // State Variable
    protected NonChaseEntityStates currentState = NonChaseEntityStates.Idle;
    protected State<NonChaseEntity>[] states = new State<NonChaseEntity>[4];
    protected StateMachine<NonChaseEntity> stateMachine = new StateMachine<NonChaseEntity>();
    #endregion

    #region Init Setting
    public override void Setup(Transform _playerTransform)
    {
        base.Setup(_playerTransform);
        if (playerTransform != null)
            return;
        playerTransform = _playerTransform;
        if (anim == null)
            anim = GetComponent<Animator>();
        states[(int)NonChaseEntityStates.Idle] = new NonChaseEntitySpace.IdleState();
        states[(int)NonChaseEntityStates.Talk] = new NonChaseEntitySpace.TalkState();
        states[(int)NonChaseEntityStates.Quiet] = new NonChaseEntitySpace.QuietState();
        states[(int)NonChaseEntityStates.Penalty] = new NonChaseEntitySpace.PenaltyState();
        stateMachine.Setup(this, states[(int)currentState]);
    }
    #endregion

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

    #region Override Behaviour
    public override void UpdateExecute() { stateMachine.Execute(); }
    public override void StartConversation() { ChangeState(NonChaseEntityStates.Talk); }
    public override void EndConversation() { ChangeState(NonChaseEntityStates.Idle); }
    public override void BeCalmDown() { ChangeState(NonChaseEntityStates.Idle); }
    public override void BeSilent() { ChangeState(NonChaseEntityStates.Quiet); }
    public override void BePenalty() { ChangeState(NonChaseEntityStates.Penalty); }
    #endregion

    #region Method
    public void ChangeState(NonChaseEntityStates _newState)
    {
        currentState = _newState;
        stateMachine.ChangeState(states[(int)currentState]);
    }
    #endregion

    #region Animation
    /// <summary>
    /// true이면 시작, false이면 종료
    /// </summary>
    /// <param name="_entityState"></param>
    /// <param name="_setBool"></param>
    public virtual void StateAnimation(NonChaseEntityStates _entityState, bool _setBool)
    {
        switch (_entityState)
        {
            case NonChaseEntityStates.Idle:
                anim.SetBool("Idle", _setBool);
                break;
            case NonChaseEntityStates.Talk:
                anim.SetBool("Talk", _setBool);
                break;
            case NonChaseEntityStates.Quiet:
                anim.SetBool("Quiet", _setBool);
                break;
            case NonChaseEntityStates.Penalty:
                anim.SetBool("Penalty", _setBool);
                break;
            default:
                break;
        }
    }
    #endregion
}
