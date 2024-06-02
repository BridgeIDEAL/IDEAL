using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEntity : BaseEntity
{
    #region ChaseEntity Common Variable
    [Header("플레이어를 감지하는 거리")]
    [SerializeField] protected float detectDistance;
    // Component
    protected Animator anim = null;
    protected NavMeshAgent nav = null;
    protected GameEventManager gameEventManager = null;
    // Relate Player
    protected bool isChasePlayer = false;
    protected Transform playerTransform;
    protected LayerMask playerLayer = 3;
    protected Vector3 eyeTransform = new Vector3(0f,1.5f,0f);
    // State
    protected EntityStateType currentState = EntityStateType.Idle;
    protected State<ChaseEntity>[] states = new State<ChaseEntity>[6];
    protected StateMachine<ChaseEntity> stateMachine = new StateMachine<ChaseEntity>();
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
    public virtual void ChaseEnter() { }
    public virtual void ChaseExecute() { }
    public virtual void ChaseExit() { }
    #endregion

    #region InitSetting
    public override void Setup()
    {
        base.Setup();
        if(playerTransform==null)
            playerTransform = IdealSceneManager.Instance.CurrentGameManager.Entity_Manager.PlayerTransform;
        if (anim == null)
            anim = GetComponent<Animator>();
        if (nav == null)
            nav = GetComponent<NavMeshAgent>();
        if(gameEventManager==null)
            gameEventManager = IdealSceneManager.Instance.CurrentGameManager.GameEvent_Manager;
        states[(int)EntityStateType.Idle] = new ChaseEntitySpace.IdleState();
        states[(int)EntityStateType.Talk] = new ChaseEntitySpace.TalkState();
        states[(int)EntityStateType.Quiet] = new ChaseEntitySpace.QuietState();
        states[(int)EntityStateType.Penalty] = new ChaseEntitySpace.PenaltyState();
        states[(int)EntityStateType.Extra] = new ChaseEntitySpace.ExtraState();
        states[(int)EntityStateType.Chase] = new ChaseEntitySpace.ChaseState();
        stateMachine.Setup(this, states[(int)currentState]);
    }
    public override void UpdateState() { stateMachine.Execute(); }
    public override void IdleState() { ChangeState(EntityStateType.Idle); }
    public override void TalkState() { ChangeState(EntityStateType.Talk); }
    public override void QuietState() { ChangeState(EntityStateType.Quiet); }
    public override void PenaltyState() { ChangeState(EntityStateType.Penalty); }
    public override void ExtraState() { ChangeState(EntityStateType.Extra); }
    public override void ChaseState() { ChangeState(EntityStateType.Chase); }
    public void ChangeState(EntityStateType _newState)
    {
        currentState = _newState;
        stateMachine.ChangeState(states[(int)currentState]);
    }
    #endregion

    #region Chase Method
    public virtual void ChasePlayer()
    {
        if (playerTransform == null)
            return;
        nav.SetDestination(playerTransform.position);
    }
    public virtual void SetChase(bool _isChasePlayer)
    {
        isChasePlayer = _isChasePlayer;
        IdealSceneManager.Instance.CurrentGameManager.Entity_Manager.IsChasePlayer= _isChasePlayer;
    }
    #endregion

    #region Animation
    /// <summary>
    /// Enter : True / Exit : False
    /// </summary>
    /// <param name="_entityState"></param>
    /// <param name="_setBool"></param>
    public virtual void StateAnimation(EntityStateType _entityState, bool _setBool)
    {
        switch (_entityState)
        {
            case EntityStateType.Idle:
                anim.SetBool("Idle", _setBool);
                break;
            case EntityStateType.Talk:
                anim.SetBool("Talk", _setBool);
                break;
            case EntityStateType.Quiet:
                anim.SetBool("Quiet", _setBool);
                break;
            case EntityStateType.Penalty:
                anim.SetBool("Penalty", _setBool);
                break;
            case EntityStateType.Chase:
                anim.SetBool("Chase", _setBool);
                break;
            case EntityStateType.Extra:
                anim.SetBool("Aggressive", true);
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, timer/1f);
            yield return null;
        }
    }
    #endregion
}
