using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CType : BaseEntity
{
    #region Variable
    public bool IsWatchPlayer { get; set; } 
    public bool CanWatchPlayer { get; protected set; } = true;
    [SerializeField] protected Transform detectTransform;
    [SerializeField] protected float watchTime;
    [SerializeField] protected float coolDownTime;
    [SerializeField] protected float sightDistance;
    #endregion

    #region Component
    public Animator anim;
    protected State<CType>[] states;
    protected StateMachine<CType> stateMachine;
    public CTypeEntityStates CurrentType { private set; get; }
    #endregion

    #region StateBehavior
    public virtual void IndifferenceEnter() { } 
    public virtual void IndifferenceExecute() { } 
    public virtual void IndifferenceExit() { }
    public virtual void WatchEnter() { } 
    public virtual void WatchExecute() { }
    public virtual void WatchExit() { } 
    public virtual void InteractionEnter() { }
    public virtual void InteractionExecute() { }
    public virtual void InteractionExit() { }
    public virtual void SpeechlessEnter() { }
    public virtual void SpeechlessExecute() { }
    public virtual void SpeechlessExit() { }
    public virtual void PenaltyEnter() { }
    public virtual void PenaltyExecute() { }
    public virtual void PenaltyExit() { }
    #endregion

    #region Initialize
    public override void Setup()
    {
        // set initVariable
        base.Setup();
        AdditionalSetup();
        // set component
        if(anim==null)
            anim = GetComponent<Animator>();
        if (detectTransform == null)
            detectTransform = this.transform;
        // set statemachine
        CurrentType = CTypeEntityStates.Indifference;
        states = new State<CType>[5];
        states[(int)CTypeEntityStates.Indifference] = new CTypeStates.Indifference();
        states[(int)CTypeEntityStates.Watch] = new CTypeStates.Watch();
        states[(int)CTypeEntityStates.Interaction] = new CTypeStates.Interaction();
        states[(int)CTypeEntityStates.Speechless] = new CTypeStates.Speechless();
        states[(int)CTypeEntityStates.Penalty] = new CTypeStates.Penalty();
        stateMachine = new StateMachine<CType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }
    #endregion

    #region Override
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
        Vector3 dirV = player.transform.position - transform.position;
        if (dirV.magnitude > sightDistance) 
            return false;
        RaycastHit rayCastHit;
        if(Physics.Raycast(transform.position+Vector3.up, dirV, out rayCastHit))
        {
            if (rayCastHit.collider.CompareTag("Player"))
                return true;
            else
                return false;
        }
        return false;
    }
    public bool IsNearPlayer(){
        if(Vector3.Distance(player.transform.position,transform.position)<sightDistance)
            return true;
        else
            return false;
    }
    #endregion

    #region CoroutineMethod
    public void StartWatchTimer() { StartCoroutine("WatchTimerCor"); }
    public void EndWatchTimer() { StopCoroutine("WatchTimerCor"); }
    public void StartCoolDownTimer() { StartCoroutine("CoolDownTimerCor"); }
    public void EndCoolDownTimer() { StopCoroutine("CoolDownTimerCor"); }
    #endregion

    #region Coroutine
    public IEnumerator WatchTimerCor()
    {
        yield return new WaitForSeconds(watchTime);
        if (CurrentType == CTypeEntityStates.Watch)
        {
            CanWatchPlayer = false;
            ChangeState(CTypeEntityStates.Indifference);
        }
    }
    public IEnumerator CoolDownTimerCor()
    {
        yield return new WaitForSeconds(coolDownTime);
        CanWatchPlayer = true;
    }
    #endregion

    #region Animation
    public virtual void SetAnimation(CTypeEntityStates entityAnim) { }
    #endregion
}
