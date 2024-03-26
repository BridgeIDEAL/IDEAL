using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    #region Variable
    public bool IsChasePlayer { get; set; } = false;
    [SerializeField] protected Vector3 initPosition;
    [SerializeField] protected Vector3 initRotation;
    #endregion

    #region Component
    protected Animator anim;
    protected NavMeshAgent nav;
    protected State<BType>[] states;
    protected StateMachine<BType> stateMachine;
    public BTypeEntityStates CurrentType { private set; get; }
   
    #endregion

    #region StateBehavior
    public virtual void IndifferenceEnter() { SetAnimation(CurrentType); }
    public virtual void IndifferenceExecute() { }
    public virtual void IndifferenceExit() { }
    public virtual void InteractionEnter() { SetAnimation(CurrentType); }
    public virtual void InteractionExecute() { }
    public virtual void InteractionExit() { LookFront(); }
    public virtual void AggressiveEnter() { SetAnimation(CurrentType); }
    public virtual void AggressiveExecute() { }
    public virtual void AggressiveExit() { }
    public virtual void ChaseEnter() { SetAnimation(CurrentType); }
    public virtual void ChaseExecute() { }
    public virtual void ChaseExit() { }
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
        nav = GetComponent<NavMeshAgent>();

        // set statemachine
        CurrentType = BTypeEntityStates.Indifference;
        states = new State<BType>[5];
        states[(int)BTypeEntityStates.Indifference] = new BTypeStates.Indifference();
        states[(int)BTypeEntityStates.Interaction] = new BTypeStates.Interaction();
        states[(int)BTypeEntityStates.Aggressive] = new BTypeStates.Aggressive();
        states[(int)BTypeEntityStates.Chase] = new BTypeStates.Chase();
        states[(int)BTypeEntityStates.Speechless] = new BTypeStates.Speechless();
        stateMachine = new StateMachine<BType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }
    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void StartConversationInteraction() { ChangeState(BTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(BTypeEntityStates.Indifference); }
    public override void ChaseInteraction() { ChangeState(BTypeEntityStates.Aggressive); }
    public override void SpeechlessInteraction() { ChangeState(BTypeEntityStates.Speechless); }
    #endregion

    #region Method
    public void ChangeState(BTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    public void ChasePlayer() { nav.SetDestination(player.transform.position); }
    #endregion

    #region Coroutine
    public IEnumerator ReturnPositionCor()
    {
        nav.ResetPath();
        nav.isStopped = true;
        yield return new WaitForSeconds(0.4f);
        transform.position = initPosition;
        transform.rotation = Quaternion.Euler(initRotation.x, initRotation.y, initRotation.z);
        IsChasePlayer = false;
        nav.isStopped = false;
    }
    #endregion

    #region Animation
    public virtual void SetAnimation(BTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case BTypeEntityStates.Indifference:
                break;
            case BTypeEntityStates.Interaction:
                WatchPlayer();
                break;
            case BTypeEntityStates.Aggressive:
                break;
            case BTypeEntityStates.Chase:
                break;
            case BTypeEntityStates.Speechless:
                break;
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (IsHeadRotate)
        {
            anim.SetLookAtPosition(player.transform.position + Vector3.up);
            anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight);
        }
    }
    #endregion
}
