using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AHighAction : AType
{
    #region Component
    [SerializeField] protected Quaternion towardRotation;
    #endregion

    #region StateBehavior
    public override void IndifferenceEnter() { base.IndifferenceEnter();}
    public override void IndifferenceExecute() { LookOriginal(); }
    public override void IndifferenceExit() { isLookOriginal = true; }
    public override void InteractionEnter() { base.InteractionEnter(); }
    public override void InteractionExecute() { LookPlayer();}
    public override void InteractionExit() { isLookPlayer = true; }
    public override void SpeechlessEnter() { base.SpeechlessEnter(); }
    public override void SpeechlessExecute() { }
    public override void SpeechlessExit() { }
    #endregion

    #region Override
    public override void AdditionalSetup() { towardRotation = Quaternion.Euler(initRotation.x,initRotation.y,initRotation.z); anim = GetComponent<Animator>(); }
    public override void StartConversationInteraction() { ChangeState(ATypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(ATypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(ATypeEntityStates.Speechless); }

    public override void LookPlayer()
    {
        if (isLookPlayer)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerObject.transform.position - transform.position);
            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            float step = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
            if (angle < marginalAngle)
                isLookPlayer = false;
        }
    }

    public override void LookOriginal()
    {
        if (isLookOriginal)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, towardRotation, rotateSpeed*Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, towardRotation) < marginalAngle)
                isLookOriginal = false;
        }
    }
    public override void SetAnimation(ATypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case ATypeEntityStates.Indifference:
                anim.CrossFade("Idle", 0.2f);
                break;
            case ATypeEntityStates.Interaction:
                anim.CrossFade("Idle", 0.2f);
                break;
            case ATypeEntityStates.Speechless:
                anim.CrossFade("Idle", 0.2f);
                break;
        }
    }
    #endregion
}
