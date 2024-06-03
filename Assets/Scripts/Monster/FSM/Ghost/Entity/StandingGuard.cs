using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingGuard : NonChaseEntity
{
    #region BehaviourState
    public override void IdleEnter() { StateAnimation(currentState, true);}
    public override void IdleExecute() { }
    public override void IdleExit() { StateAnimation(currentState, false); }
    public override void TalkEnter() { StateAnimation(currentState, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { StateAnimation(currentState, false); }
    public override void QuietEnter() { StateAnimation(currentState, true); }
    public override void QuietExecute() { }
    public override void QuietExit() { StateAnimation(currentState, false); }
    public override void PenaltyEnter() { StateAnimation(currentState, true); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { StateAnimation(currentState, false); }
    public override void ExtraEnter() { StateAnimation(currentState, true); }
    public override void ExtraExecute() { }
    public override void ExtraExit() { StateAnimation(currentState, false); }
    #endregion
}
