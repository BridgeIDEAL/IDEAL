using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CType : BaseEntity
{
    #region Component
    protected bool isWatch = false;
    protected Animator anim;
    protected State<CType>[] states;
    protected StateMachine<CType> stateMachine;
    public CTypeEntityStates CurrentType { private set; get; }
    #endregion

    #region Virtual
    public virtual void IndifferenceEnter() { SetAnimation(CurrentType); } 
    public virtual void IndifferenceExecute() { DetectPlayer(); } 
    public virtual void IndifferenceExit() { }
    public virtual void WatchEnter() { SetAnimation(CurrentType); StartWatchTimer(); } 
    public virtual void WatchExecute() { WatchPlayer(); if (!CanDetectPlayer()) ChangeState(CTypeEntityStates.Indifference); }
    public virtual void WatchExit() { EndWatchTimer();  } 
    public virtual void InteractionEnter() { SetAnimation(CurrentType); }
    public virtual void InteractionExecute() { LookPlayer(); }
    public virtual void InteractionExit() { isLookPlayer = true; }
    public virtual void SpeechlessEnter() { SetAnimation(CurrentType); }
    public virtual void SpeechlessExecute() { }
    public virtual void SpeechlessExit() { }
    public virtual void SetAnimation(CTypeEntityStates entityAnim) { }
    #endregion

    #region Override
    public override void Setup(MonsterData.MonsterStat stat)
    {
        // set information
        gameObject.name = stat.monsterName;
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = initPosition;
        transform.eulerAngles = initRotation;        
        // add component
        AdditionalSetup();
        anim = GetComponentInChildren<Animator>();
        // set statemachine
        CurrentType = CTypeEntityStates.Indifference;
        states = new State<CType>[4];
        states[(int)CTypeEntityStates.Indifference] = new CTypeStates.Indifference();
        states[(int)CTypeEntityStates.Watch] = new CTypeStates.Watch();
        states[(int)CTypeEntityStates.Interaction] = new CTypeStates.Interaction();
        states[(int)CTypeEntityStates.Speechless] = new CTypeStates.Speechless();
        stateMachine = new StateMachine<CType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }

    public override void UpdateBehavior(){stateMachine.Execute();}
    public override void StartConversationInteraction() { ChangeState(CTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(CTypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(CTypeEntityStates.Speechless); }

    #endregion

    #region Method
    public void ChangeState(CTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void DetectPlayer()
    {
        if (isWatch)
            return;
        if (CanDetectPlayer()) { ChangeState(CTypeEntityStates.Watch); return; }
    }

    public bool CanDetectPlayer()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude <= sightDistance)
            return true;
        else
            return false;
    }

    public void WatchPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(playerObject.transform.position - transform.position);
        float angle = Quaternion.Angle(transform.rotation, targetRotation);
        float step = rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
    }

    public void StartWatchTimer() { StartCoroutine("WatchTimer"); }
    public void EndWatchTimer() { StopCoroutine("WatchTimer"); }
    public IEnumerator WatchTimer()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentType == CTypeEntityStates.Watch && !isWatch)
        {
            isWatch = true;
            ChangeState(CTypeEntityStates.Indifference);
        }
            
        yield break;   
    }
    #endregion
}
