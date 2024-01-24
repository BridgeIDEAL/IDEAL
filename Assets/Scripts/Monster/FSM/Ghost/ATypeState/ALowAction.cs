using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ALowAction : AType
{
    #region StateBehavior
    public override void IndifferenceEnter() { base.IndifferenceEnter(); }
    public override void IndifferenceExecute() { }
    public override void IndifferenceExit() { }
    public override void InteractionEnter() { base.InteractionEnter(); }
    public override void InteractionExecute() { }
    public override void InteractionExit() {  }
    public override void SpeechlessEnter() { base.SpeechlessEnter(); }
    public override void SpeechlessExecute() { }
    public override void SpeechlessExit() { }
    #endregion

    #region Override
    public override void AdditionalSetup() { anim = GetComponent<Animator>(); }
    public override void StartConversationInteraction() { ChangeState(ATypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(ATypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(ATypeEntityStates.Speechless); }
    public override void SetAnimation(ATypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case ATypeEntityStates.Indifference:
                anim.CrossFade("Idle", 0.2f);
                break;
            case ATypeEntityStates.Interaction:
                anim.CrossFade("Interaction", 0.2f);
                break;
            case ATypeEntityStates.Speechless:
                anim.CrossFade("Idle", 0.2f);
                break;
        }
    }
    #endregion
}
