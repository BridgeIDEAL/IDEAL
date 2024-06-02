using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonChaseEntity : BaseEntity
{
    #region NonChaseEntity Common Variable
    // Component
    protected Animator anim;
    // Relate Player 
    protected LayerMask playerLayer = 3;
    protected Transform playerTransform;
    // State
    protected EntityStateType currentState = EntityStateType.Idle;
    protected State<NonChaseEntity>[] states = new State<NonChaseEntity>[4];
    protected StateMachine<NonChaseEntity> stateMachine = new StateMachine<NonChaseEntity>();
    #endregion

    #region Init Setting
    public override void Setup()
    {
        base.Setup();
        if (playerTransform != null)
            playerTransform = IdealSceneManager.Instance.CurrentGameManager.Entity_Manager.PlayerTransform;
        if (anim == null)
            anim = GetComponent<Animator>();
        states[(int)EntityStateType.Idle] = new NonChaseEntitySpace.IdleState();
        states[(int)EntityStateType.Talk] = new NonChaseEntitySpace.TalkState();
        states[(int)EntityStateType.Quiet] = new NonChaseEntitySpace.QuietState();
        states[(int)EntityStateType.Penalty] = new NonChaseEntitySpace.PenaltyState();
        //states[(int)EntityStateType.Extra] = new NonChaseEntitySpace.ExtraState();
        stateMachine.Setup(this, states[(int)currentState]);
    }
    #endregion

    #region BehaviourState
    public virtual void IdleEnter() { StateAnimation(EntityStateType.Idle, true); }
    public virtual void IdleExecute() { }
    public virtual void IdleExit() { StateAnimation(EntityStateType.Idle, false); }
    public virtual void TalkEnter() { StateAnimation(EntityStateType.Talk, true); StartCoroutine(LookPlayerCor()); }
    public virtual void TalkExecute() { }
    public virtual void TalkExit() { StateAnimation(EntityStateType.Talk, false); }
    public virtual void QuietEnter() { StateAnimation(EntityStateType.Quiet, true); }
    public virtual void QuietExecute() { }
    public virtual void QuietExit() { StateAnimation(EntityStateType.Quiet, false); }
    public virtual void PenaltyEnter() { StateAnimation(EntityStateType.Penalty, true); }
    public virtual void PenaltyExecute() { }
    public virtual void PenaltyExit() { StateAnimation(EntityStateType.Penalty, false); }
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
    public override void  UpdateState() { stateMachine.Execute(); }
    public override void  IdleState() { ChangeState(EntityStateType.Idle); }
    public override void  TalkState() { ChangeState(EntityStateType.Talk);  }
    public override void  QuietState() { ChangeState(EntityStateType.Quiet); }
    public override void PenaltyState() { ChangeState(EntityStateType.Penalty); }
    #endregion

    #region Method
    public void ChangeState(EntityStateType _newState)
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
    public virtual void StateAnimation(EntityStateType _entityState, bool _setBool)
    {
        switch (_entityState)
        {
            case EntityStateType.Idle:
                anim.SetBool("IDLE", _setBool);
                break;
            case EntityStateType.Talk:
                anim.SetBool("IDLE", _setBool);
                break;
            case EntityStateType.Quiet:
                anim.SetBool("IDLE", _setBool);
                break;
            case EntityStateType.Penalty:
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
