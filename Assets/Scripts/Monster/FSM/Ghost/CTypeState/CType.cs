using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CType : BaseEntity
{
    #region Variable
    public bool IsWatchPlayer { get; set; }
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
    public virtual void IndifferenceExecute() { if (InSight()) ChangeState(CTypeEntityStates.Watch); } 
    public virtual void IndifferenceExit() { }
    public virtual void WatchEnter() { SetAnimation(CurrentType); StartCoroutine("WatchTimerCor"); IsHeadRotate = true; } 
    public virtual void WatchExecute() {if (!InSight()) ChangeState(CTypeEntityStates.Indifference); }
    public virtual void WatchExit() { StopCoroutine("WatchTimerCor"); IsHeadRotate = false; } 
    public virtual void InteractionEnter() { SetAnimation(CurrentType); }
    public virtual void InteractionExecute() { }
    public virtual void InteractionExit() { WatchFront(); }
    public virtual void SpeechlessEnter() { SetAnimation(CurrentType); }
    public virtual void SpeechlessExecute() { }
    public virtual void SpeechlessExit() { }
    #endregion

    #region Override
    public override void Setup()
    {
        // set initVariable
        base.Setup();
        AdditionalSetup();

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
    public bool InSight()
    {
        Vector3 interV = player.transform.position - transform.position;
        if (interV.magnitude > sightDistance) 
            return false;
        RaycastHit rayCastHit;
        Vector3 direction = player.transform.position - transform.position;
        if(Physics.Raycast(transform.position, direction, out rayCastHit))
        {
            if (rayCastHit.collider.CompareTag("Player"))
                return true;
            else
                return false;
        }
        return false;
    }
    #endregion

    #region Coroutine
    public IEnumerator WatchTimerCor()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentType == CTypeEntityStates.Watch)
            ChangeState(CTypeEntityStates.Indifference);
        Debug.Log("경고 : 경계 상태 10초 초과!!");
        yield break;   
    }
    #endregion

    #region Animation
    public virtual void SetAnimation(CTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case CTypeEntityStates.Indifference:
                break;
            case CTypeEntityStates.Interaction:
                WatchPlayer();
                break;
            case CTypeEntityStates.Watch:
                break;
            case CTypeEntityStates.Speechless:
                break;
        }
    }
    #endregion
}
