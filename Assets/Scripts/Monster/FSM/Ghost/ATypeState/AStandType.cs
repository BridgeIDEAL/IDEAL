using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStandType : AType
{
    #region StateBehavior
    public override void IndifferenceEnter() { SetAnimation(CurrentType); }
    public override void IndifferenceExecute() { }
    public override void IndifferenceExit() { }
    public override void InteractionEnter() { SetAnimation(CurrentType); }
    public override void InteractionExecute() { }   
    public override void InteractionExit() { WatchFront(); }
    public override void SpeechlessEnter() { SetAnimation(CurrentType); }
    public override void SpeechlessExecute() { }
    public override void SpeechlessExit() { }
    #endregion

    #region Animation
    public override void SetAnimation(ATypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case ATypeEntityStates.Indifference:
                break;
            case ATypeEntityStates.Interaction:
                WatchPlayer();
                break;
            case ATypeEntityStates.Speechless:
                break;
        }
    }
    #endregion
}
