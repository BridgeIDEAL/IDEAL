using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEntity : BaseEntity
{
    [Header("변수 : 기획")]
    [SerializeField] protected ChaseEntityStates currentState = ChaseEntityStates.Idle;
    [SerializeField] protected float detectDistance; // 시야
    [SerializeField] protected float missDistance; // 감지하는 최소거리
    [SerializeField] protected Transform eyeTransform; // 눈 위치
    [SerializeField] protected Vector3[] patrolPoints; // 순찰 지점 
    
    [Header("컴포넌트 : 프로그래밍")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected NavMeshAgent nav;
    protected int currentPatrolPoint = 0;
    protected int maxPatrolPoint = 0;
    protected bool isChasePlayer = false;
    public bool IsChasePlayer
    {
        get
        {
            return isChasePlayer;
        }
        set
        {
            isChasePlayer = value;
        }
    }
    // 내장된 스크립트
    protected State<ChaseEntity>[] states = new State<ChaseEntity>[5];
    protected StateMachine<ChaseEntity> stateMachine = new StateMachine<ChaseEntity>();

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
    public virtual void ChaseEnter() { }
    public virtual void ChaseExecute() { }
    public virtual void ChaseExit() { }
    public virtual void ExtraEnter() { }
    public virtual void ExtraExecute() { }
    public virtual void ExtraExit() { }

    #endregion

    #region InitSetting
    public override void Setup()
    {
        base.Setup();
        maxPatrolPoint = patrolPoints.Length;
        states[(int)ChaseEntityStates.Idle] = new ChaseEntitySpace.IdleState();
        states[(int)ChaseEntityStates.Talk] = new ChaseEntitySpace.TalkState();
        states[(int)ChaseEntityStates.Quiet] = new ChaseEntitySpace.QuietState();
        states[(int)ChaseEntityStates.Penalty] = new ChaseEntitySpace.PenaltyState();
        states[(int)ChaseEntityStates.Chase] = new ChaseEntitySpace.ChaseState();
        stateMachine.Setup(this, states[(int)currentState]);
    }

    public override void UpdateExecute() { stateMachine.Execute(); }
    public override void StartConversation() { ChangeState(ChaseEntityStates.Talk); }
    public override void EndConversation() { ChangeState(ChaseEntityStates.Idle); }
    public override void BeCalmDown() { ChangeState(ChaseEntityStates.Idle); }
    public override void BeSilent() { ChangeState(ChaseEntityStates.Quiet); }
    public override void BeChasing() { ChangeState(ChaseEntityStates.Chase) ; }
    public void ChangeState(ChaseEntityStates _newState)
    {
        currentState = _newState;
        stateMachine.ChangeState(states[(int)currentState]);
    }
    #endregion

    #region Method

    public void MoveTo(Vector3 _destination, float _speed=0.5f)
    {
        nav.SetDestination(_destination);
        Vector3 direction = (eyeTransform.position - _destination).normalized;
        bool isFaceDirection = Vector3.Dot(direction, transform.forward) > 0.5f;
        anim.SetFloat("Speed", (isFaceDirection) ? _speed : 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), 180 * Time.deltaTime);
    }

    public void ChasePlayer()
    {
        if (player == null)
            return;
        MoveTo(player.transform.position);
    }
    #endregion

    #region Animation
    /// <summary>
    /// true이면 시작, false이면 종료
    /// </summary>
    /// <param name="_entityState"></param>
    /// <param name="_setBool"></param>
    public virtual void StateAnimation(ChaseEntityStates _entityState, bool _setBool) 
    {
        switch (_entityState)
        {
            case ChaseEntityStates.Idle:
                anim.SetBool("Idle", _setBool);
                break;
            case ChaseEntityStates.Talk:
                anim.SetBool("Talk", _setBool);
                break;
            case ChaseEntityStates.Quiet:
                anim.SetBool("Quiet", _setBool);
                break;
            case ChaseEntityStates.Penalty:
                anim.SetBool("Penalty", _setBool);
                break;
            case ChaseEntityStates.Chase:
                anim.SetBool("Chase", _setBool);
                break;
            default:
                break;
        }
    }
    #endregion
}
