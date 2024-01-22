using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AHighAction : AType
{
    #region Component
    [SerializeField] protected float turnSpeed = 5f;
    #endregion

    public override void StartConversationInteraction() { ChangeState(ATypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(ATypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(ATypeEntityStates.Speechless); }

    public override void LookPlayer()
    {
        Vector3 dir = playerObject.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * turnSpeed);
    }

    public override void LookOriginal()
    {
        Quaternion targetRotation = Quaternion.Euler(initRotation);
        transform.rotation = targetRotation;
        //Quaternion originalDir = Quaternion.Euler(initRotation);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, originalDir, 5 * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, , 5 * Time.deltaTime);
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
}
