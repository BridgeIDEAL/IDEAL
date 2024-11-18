using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMovableEntity : BaseEntity
{
    protected Animator anim;
    protected EntityState<IMovableEntity>[] states;
    protected EntityStateMachine<IMovableEntity> stateMachine;

    public override void Init(Transform _playerTransfrom, Transform _playerHeightTransform)
    {
        playerTransform = _playerTransfrom;
        playerHeightTransform = _playerHeightTransform; ;

        currentType = EntityStateType.Idle;
        anim = GetComponent<Animator>();
        // States
        states = new EntityState<IMovableEntity>[(int)EntityStateType.None-1];
        states[(int)EntityStateType.Idle] = new ImmovableEntityStates.IdleState();
        states[(int)EntityStateType.Talk] = new ImmovableEntityStates.TalkState();
        states[(int)EntityStateType.Quiet] = new ImmovableEntityStates.QuietState();
        states[(int)EntityStateType.Penalty] = new ImmovableEntityStates.PenaltyState();
        // StateMachine
        stateMachine = new EntityStateMachine<IMovableEntity>();
        stateMachine.Init(this, states[(int)currentType]);

        AdditionalInit();
    }
    public virtual void AdditionalInit() { }

    public override void Setup()
    {
        entity_Data = EntityDataManager.Instance.GetEntityData(gameObject.name);
        if (entity_Data == null)
        {
            Debug.LogError("해당 이형체의 정보를 찾을 수 없습니다!");
            return;
        }
        
        if (entity_Data.isSpawn)
            controller.ActiveEntity(entity_Data.speakerName);
        else
            SetActiveState(false);

        if (Entity_Data.speakIndex == -1)
            this.gameObject.layer = defaultLayer;

        AdditionalSetup();
    }

    public virtual void AdditionalSetup()  { }
    public override void Execute()
    {
        stateMachine.Execute();
    }

    public override void ReceiveMessage(EntityStateType _messageType) { ChangeState(_messageType); }
    public void ChangeState(EntityStateType _changeType)
    {
        currentType = _changeType;
        stateMachine.ChangeState(states[(int)currentType]);
    }

    #region Animation
    public virtual void SetAnimation(EntityStateType _currentType, bool _isStart) { }

    public override void AnimationTriggerCallByDialogue(string _triggerName)
    {
        base.AnimationTriggerCallByDialogue(_triggerName);
        anim.SetTrigger(_triggerName);
    }
    #endregion

    #region Act Frame
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


//switch (_currentType)
//{
//    case EntityStateType.Idle:
//        anim.SetBool("Idle", _isStart);
//        break;
//    default:
//        anim.SetBool("Idle", _isStart);
//        break;
//}