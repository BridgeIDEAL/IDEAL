using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonChaseEntity : BaseEntity
{
    #region NonChaseEntity Common Variable
    [Header("NonChaseEntity Common Variable")]
    [SerializeField] protected Animator anim;
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
    public virtual void IdleEnter() { StateAnimation(NonChaseEntityStates.Idle, true); }
    public virtual void IdleExecute() { }
    public virtual void IdleExit() { StateAnimation(NonChaseEntityStates.Idle, false); }
    public virtual void TalkEnter() { StateAnimation(NonChaseEntityStates.Talk, true); }
    public virtual void TalkExecute() { }
    public virtual void TalkExit() { StateAnimation(NonChaseEntityStates.Talk, false); }
    public virtual void QuietEnter() { StateAnimation(NonChaseEntityStates.Quiet, true); }
    public virtual void QuietExecute() { }
    public virtual void QuietExit() { StateAnimation(NonChaseEntityStates.Quiet, false); }
    public virtual void PenaltyEnter() { StateAnimation(NonChaseEntityStates.Penalty, true); }
    public virtual void PenaltyExecute() { }
    public virtual void PenaltyExit() { StateAnimation(NonChaseEntityStates.Penalty, false); }
    #endregion

    #region Empty Method
    /// <summary>
    /// 추가로 넣어야 할 행동이 없다면 삭제
    /// </summary>
    public virtual void ExtraEnter() { }
    public virtual void ExtraExecute() { }
    public virtual void ExtraExit() { }
    #endregion

    #region Override Behaviour
    public override void UpdateExecute() { stateMachine.Execute(); }
    public override void StartConversation() { ChangeState(NonChaseEntityStates.Talk); StartCoroutine(LookPlayerCor()); }
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
                anim.SetBool("IDLE", _setBool);
                break;
            case NonChaseEntityStates.Talk:
                anim.SetBool("IDLE", _setBool);
                break;
            case NonChaseEntityStates.Quiet:
                anim.SetBool("IDLE", _setBool);
                break;
            case NonChaseEntityStates.Penalty:
                anim.SetBool("IDLE", _setBool);
                break;
            default:
                break;
        }
    }
    protected IEnumerator LookPlayerCor()
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, timer / 1f);
            yield return null;
        }
    }
    #endregion
}
