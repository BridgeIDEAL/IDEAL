using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovableEntity : BaseEntity
{
    [SerializeField] protected EntityStateType currentType;
    protected Animator anim;
    protected NavMeshAgent agent;
    protected EntityState<MovableEntity>[] states;
    protected EntityStateMachine<MovableEntity> stateMachine;


    #region Unity Life Cycle
   
    // Awake
    public override void Init(Transform _playerTransfrom)
    {
        playerTransform = _playerTransfrom;
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
    }

    // Start
    public override void Setup()
    {
        entity_Data = EntityDataManager.Instance.GetEntityData(gameObject.name);
        if (entity_Data == null)
        {
            Debug.LogError("해당 이형체의 정보를 찾을 수 없습니다!");
            return;
        }
        controller.ActiveEntity(gameObject.name);
        if (entity_Data.isSpawn)
            controller.ActiveEntity(entity_Data.speakerName);
        else
            SetActiveState(false);
    }

    // Update
    public override void Execute()
    {
        stateMachine.Execute();
    }

    #endregion

    #region State Method (Receive & Change)
    public override void ReceiveMessage(EntityStateType _messageType)
    {
        ChangeState(_messageType);
    }

    public virtual void ChangeState(EntityStateType _changeType)
    {
        stateMachine.ChangeState(states[(int)_changeType]);
        currentType = _changeType;
    }
    #endregion

    #region Act Frame

    // Animation
    public virtual void SetAnimation(EntityStateType _currentType, bool _isStart)
    {
        switch (_currentType)
        {
            //case EntityStateType.Idle:
            //    anim.SetBool("Idle", _isStart);
            //    break;
            //case EntityStateType.Talk:
            //    anim.SetBool("Talk", _isStart);
            //    break;
            //case EntityStateType.Quiet:
            //    anim.SetBool("Quiet", _isStart);
            //    break;
            //case EntityStateType.Penalty:
            //    anim.SetBool("Penalty", _isStart);
            //    break;
            //case EntityStateType.Chase:
            //    anim.SetBool("Chase", _isStart);
            //    break;
            //default:
            //    break;
        }
    }

    // Act
    public virtual void IdleEnter() { SetAnimation(EntityStateType.Idle, true); }
    public virtual void IdleExecute() { }
    public virtual void IdleExit() { SetAnimation(EntityStateType.Idle, false); }
    public virtual void TalkEnter() { SetAnimation(EntityStateType.Talk, true); }
    public virtual void TalkExecute() { }
    public virtual void TalkExit() { SetAnimation(EntityStateType.Talk, false); }
    public virtual void QuietEnter() { SetAnimation(EntityStateType.Quiet, true); }
    public virtual void QuietExecute() { }
    public virtual void QuietExit() { SetAnimation(EntityStateType.Quiet, false); }
    public virtual void PenaltyEnter() { SetAnimation(EntityStateType.Penalty, true); }
    public virtual void PenaltyExecute() { }
    public virtual void PenaltyExit() { SetAnimation(EntityStateType.Penalty, false); }
    public virtual void ChaseEnter() { SetAnimation(EntityStateType.Chase, true); }
    public virtual void ChaseExecute() { }
    public virtual void ChaseExit() { SetAnimation(EntityStateType.Chase, false); }
    #endregion
}
