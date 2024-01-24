using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CAction : CType
{
    #region Component
    [SerializeField] protected Quaternion towardRotation;
    #endregion

    #region StateBehavior
    public override void IndifferenceExecute() { LookOriginal(); base.IndifferenceExecute(); }
    public override void IndifferenceExit() { isLookOriginal = true; }
    public override void SpeechlessExecute() { LookOriginal(); }
    public override void SpeechlessExit() { isLookOriginal = true; }
    #endregion

    #region Override
    public override void AdditionalSetup() { towardRotation = Quaternion.Euler(initRotation.x, initRotation.y, initRotation.z); }
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
            transform.rotation = Quaternion.Lerp(transform.rotation, towardRotation, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, towardRotation) < marginalAngle)
                isLookOriginal = false;
        }
    }

    public override void SetAnimation(CTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case CTypeEntityStates.Indifference:
                anim.CrossFade("Idle", 0.2f);
                break;
            case CTypeEntityStates.Watch:
                anim.CrossFade("Idle", 0.2f);
                break;
            case CTypeEntityStates.Interaction:
                anim.CrossFade("Idle", 0.2f);
                break;
            case CTypeEntityStates.Speechless:
                anim.CrossFade("Idle", 0.2f);
                break;
        }
    }
    #endregion
}
