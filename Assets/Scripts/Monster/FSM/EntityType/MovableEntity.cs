using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableEntity : BaseEntity
{
    protected EntityStateType currentType;
    
    // Need Value
    // ---------------------------
    // walk or patrol speed
    // chase speed
    // animation speed multiply
    // spawn 
    // dialogue name
    
    // if have aggressive montion?
    // (chase = true) -> aggressive motion -> chase motion(Loop) ->  (chase = false)

    protected Animator anim;
    protected NavMeshAgent agent;
    protected EntityState<MovableEntity>[] states;
    protected EntityStateMachine<MovableEntity> stateMachine;

    public override void Init()
    {
        currentType = EntityStateType.Idle;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        // States
        states = new EntityState<MovableEntity>[(int)EntityStateType.None];
        states[(int)EntityStateType.Idle] = new MovableEntityStates.IdleState();
        states[(int)EntityStateType.Talk] = new MovableEntityStates.TalkState();
        states[(int)EntityStateType.Quiet] = new MovableEntityStates.QuietState();
        states[(int)EntityStateType.Penalty] = new MovableEntityStates.PenaltyState();
        states[(int)EntityStateType.Chase] = new MovableEntityStates.ChaseState();
        // StateMachine
        stateMachine = new EntityStateMachine<MovableEntity>();
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

    public virtual void SetAnimation(EntityStateType _currentType, bool _isStart)
    {
        switch (_currentType)
        {
            case EntityStateType.Idle:
                anim.SetBool("Idle", _isStart);
                break;
            case EntityStateType.Talk:
                anim.SetBool("Talk", _isStart);
                break;
            case EntityStateType.Quiet:
                anim.SetBool("Quiet", _isStart);
                break;
            case EntityStateType.Penalty:
                anim.SetBool("Penalty", _isStart);
                break;
            case EntityStateType.Chase:
                anim.SetBool("Chase", _isStart);
                break;
            default:
                break;
        }
    }

    #region Act Frame
    public virtual void IdleEnter() { SetAnimation(currentType, true); }
    public virtual void IdleExecute() { }
    public virtual void IdleExit() { SetAnimation(currentType, false); }
    public virtual void TalkEnter() { SetAnimation(currentType, true); }
    public virtual void TalkExecute() { }
    public virtual void TalkExit() { SetAnimation(currentType, false); }
    public virtual void QuietEnter() { SetAnimation(currentType, true); }
    public virtual void QuietExecute() { }
    public virtual void QuietExit() { SetAnimation(currentType, false); }
    public virtual void PenaltyEnter() { SetAnimation(currentType, true); }
    public virtual void PenaltyExecute() { }
    public virtual void PenaltyExit() { SetAnimation(currentType, false); }
    public virtual void ChaseEnter() { SetAnimation(currentType, true); }
    public virtual void ChaseExecute() { }
    public virtual void ChaseExit() { SetAnimation(currentType, false); }
    #endregion
}
