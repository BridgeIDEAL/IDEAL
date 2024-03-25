using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AType : BaseEntity
{
    #region Component
    protected Animator anim;
    protected State<AType>[] states;
    protected StateMachine<AType> stateMachine;
    public ATypeEntityStates CurrentType { protected set; get; }
    #endregion

    #region StateBehavior
    public virtual void IndifferenceEnter() { SetAnimation(CurrentType); }
    public virtual void IndifferenceExecute() { }
    public virtual void IndifferenceExit() { }
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

        // set component
        anim = GetComponent<Animator>();

        // set statemachine
        CurrentType = ATypeEntityStates.Indifference;
        states = new State<AType>[3];
        states[(int)ATypeEntityStates.Indifference] = new ATypeStates.Indifference();
        states[(int)ATypeEntityStates.Interaction] = new ATypeStates.Interaction();
        states[(int)ATypeEntityStates.Speechless] = new ATypeStates.Speechless();
        stateMachine = new StateMachine<AType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }
    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void StartConversationInteraction() { ChangeState(ATypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(ATypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(ATypeEntityStates.Speechless); }
    #endregion

    #region Method
    public void ChangeState(ATypeEntityStates newState)
    { 
        CurrentType = newState; 
        stateMachine.ChangeState(states[(int)newState]); 
    }
    #endregion

    #region Animation
    public virtual void SetAnimation(ATypeEntityStates entityAnim) { }
    private void OnAnimatorIK(int layerIndex)
    {
        if (HeadRotate)
        {
            anim.SetLookAtPosition(player.transform.position + Vector3.up);
            anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight);
        }
    }
    #endregion
}
