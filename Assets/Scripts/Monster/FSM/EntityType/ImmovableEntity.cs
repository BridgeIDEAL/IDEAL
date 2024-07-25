using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmovableEntity : BaseEntity
{
    protected EntityStateType currentType;

    protected Animator anim;
    protected EntityState<ImmovableEntity>[] states;
    protected EntityStateMachine<ImmovableEntity> stateMachine;

    public override void Init(Transform _playerTransfrom)
    {
        playerTransform = _playerTransfrom;
        playerTransform = _playerTransfrom;
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
    }

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

        // Interaction Add
    }

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

    public virtual void IdleEnter() { SetAnimation(currentType,true); }
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
    #endregion
}
