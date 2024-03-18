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

    public bool CheckBehind()
    {
        Vector3 mySelfDir = transform.forward;
        Vector3 playerDir = player.transform.position - transform.position;
        return (Vector3.Dot(mySelfDir, playerDir) > 0f) ? true : false;
    }
    #endregion

    #region Animation
    public virtual void SetAnimation(ATypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case ATypeEntityStates.Indifference:
                ActiveLookCor(false);
                break;
            case ATypeEntityStates.Interaction:
                ActiveLookCor();
                break;
            case ATypeEntityStates.Speechless:
                ActiveLookCor(false);
                break;
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (activeLook)
        {
            anim.SetLookAtPosition(player.transform.position + Vector3.up);
            anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight);
        }
    }
    #endregion

    
    //public IEnumerator BodyRotate()
    //{
    //    float cur = 0f;
    //    float per = 0f;
    //    float speed = 3f;
    //    while (per < 1f)
    //    {
    //        cur += Time.deltaTime;
    //        per = cur / speed;
    //        Quaternion tr = Quaternion.LookRotation(player.transform.position - transform.position);
    //        transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z),
    //            tr, per);
    //        yield return null;
    //    }
    //}
}
