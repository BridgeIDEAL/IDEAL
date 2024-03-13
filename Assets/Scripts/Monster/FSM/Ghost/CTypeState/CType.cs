using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CType : BaseEntity
{
    #region Variable
    protected bool isWatch = false;
    protected bool isChasePlayer = false;
    [SerializeField] protected float sightDistance;
    #endregion

    #region Component
    protected Animator anim;
    protected State<CType>[] states;
    protected StateMachine<CType> stateMachine;
    public CTypeEntityStates CurrentType { private set; get; }
    #endregion

    #region StateBehavior
    public virtual void IndifferenceEnter() { SetAnimation(CurrentType); } 
    public virtual void IndifferenceExecute() { DetectPlayer(); } 
    public virtual void IndifferenceExit() { }
    public virtual void WatchEnter() { SetAnimation(CurrentType); StartWatchTimer(); } 
    public virtual void WatchExecute() {if (!CanDetectPlayer()) ChangeState(CTypeEntityStates.Indifference); }
    public virtual void WatchExit() { EndWatchTimer(); } 
    public virtual void InteractionEnter() { SetAnimation(CurrentType); }
    public virtual void InteractionExecute() { }
    public virtual void InteractionExit() { }
    public virtual void SpeechlessEnter() { SetAnimation(CurrentType); }
    public virtual void SpeechlessExecute() { }
    public virtual void SpeechlessExit() { }
    #endregion

    #region Override
    public override void Setup()
    {
        // set initVariable
        base.Setup();
        initLookDir = transform.forward;

        // set component
        anim = GetComponent<Animator>();

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
        Vector3 interV = player.transform.position - transform.position;
        if (interV.magnitude <= sightDistance) return true;
        else return false;

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

    #region Animation
    public virtual void SetAnimation(CTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case CTypeEntityStates.Indifference:
                isLookPlayer = false;
                break;
            case CTypeEntityStates.Interaction:
                isLookPlayer = true;
                break;
            case CTypeEntityStates.Watch:
                break;
            case CTypeEntityStates.Speechless:
                isLookPlayer = false;
                break;
        }
    }

    public void OnAnimatorIK(int layerIndex)
    {
        if (isChasePlayer)
            return;
        if (isLookPlayer)
        {
            anim.SetLookAtPosition(player.transform.position);
            anim.SetLookAtWeight(1, bodyWeight, headWeight);
        }
        else
        {
            anim.SetLookAtPosition(initLookDir);
            anim.SetLookAtWeight(1, bodyWeight, headWeight);
        }
    }
    #endregion
}
