using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEntity : BaseEntity
{
    #region ChaseEntity Common Variable
    [Header("ChaseEntity Common Variable")]
    [SerializeField] protected Animator anim;
    [SerializeField] protected NavMeshAgent nav;
    [SerializeField] protected Transform eyeTransform; // Detect Transform => Y : 1.5~2f 
    [SerializeField] protected float detectDistance;
    // Player Variable
    protected LayerMask playerLayer = 3;
    protected Transform playerTransform;
    protected bool isChasePlayer = false;
    // State Variable
    [SerializeField] protected ChaseEntityStates currentState = ChaseEntityStates.Idle;
    protected State<ChaseEntity>[] states = new State<ChaseEntity>[5];
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
    public virtual void ChaseEnter() { }
    public virtual void ChaseExecute() { }
    public virtual void ChaseExit() { }
    public virtual void ExtraEnter() { }
    public virtual void ExtraExecute() { }
    public virtual void ExtraExit() { }

    #endregion

    #region InitSetting
    public override void Setup(Transform _playerTransform)
    {
        base.Setup(_playerTransform);
        if (playerTransform != null)
            return;
        playerTransform = _playerTransform;
        if (anim == null)
            anim = GetComponent<Animator>();
        if (nav == null)
            nav = GetComponent<NavMeshAgent>();
        states[(int)ChaseEntityStates.Idle] = new ChaseEntitySpace.IdleState();
        states[(int)ChaseEntityStates.Talk] = new ChaseEntitySpace.TalkState();
        states[(int)ChaseEntityStates.Quiet] = new ChaseEntitySpace.QuietState();
        states[(int)ChaseEntityStates.Penalty] = new ChaseEntitySpace.PenaltyState();
        states[(int)ChaseEntityStates.Chase] = new ChaseEntitySpace.ChaseState();
        stateMachine.Setup(this, states[(int)currentState]);
    }
    public override void UpdateExecute() { stateMachine.Execute(); }
    public override void StartConversation() { ChangeState(ChaseEntityStates.Talk); StartCoroutine(LookPlayerCor()); }
    public override void EndConversation() { ChangeState(ChaseEntityStates.Idle); }
    public override void BeCalmDown() { ChangeState(ChaseEntityStates.Idle); }
    public override void BeSilent() { ChangeState(ChaseEntityStates.Quiet); }
    /// <summary>
    /// (Have) Aggressive Animation => Act Animation => Chase 
    /// (Do not Have) Aggressive Animation => Immediately Chase
    /// </summary>
    public override void BeChasing() { ChangeState(ChaseEntityStates.Chase); } // 나중에 변경해야 함 => virtual로
    public override void BePenalty() { ChangeState(ChaseEntityStates.Penalty); }
    public void ChangeState(ChaseEntityStates _newState)
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
    #endregion

    #region Animation
    /// <summary>
    /// Enter : True / Exit : False
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
            case ChaseEntityStates.Extra:
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
