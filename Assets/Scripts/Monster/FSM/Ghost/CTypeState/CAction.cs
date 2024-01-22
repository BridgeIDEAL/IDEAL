using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CAction : CType
{
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
}
