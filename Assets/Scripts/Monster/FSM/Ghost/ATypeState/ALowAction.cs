using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ALowAction : AType
{
    public override void StartConversationInteraction() { ChangeState(ATypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(ATypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(ATypeEntityStates.Speechless); }
    public override void SetAnimation(ATypeEntityStates entityAnim)
    {
        //switch (entityAnim)
        //{
        //    case ATypeEntityStates.Indifference:
        //        anim.CrossFade("Idle", 0.2f);
        //        break;
        //    case ATypeEntityStates.Interaction:
        //        anim.CrossFade("Interaction", 0.2f);
        //        break;
        //    case ATypeEntityStates.Speechless:
        //        anim.CrossFade("Idle", 0.2f);
        //        break;
        //}
    }
}
