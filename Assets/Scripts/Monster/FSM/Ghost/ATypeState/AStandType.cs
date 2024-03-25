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

    #region Method
    public void WatchPlayer()
    {
        float angle = CalculateAngle();
        if (angle >= -90 && angle <= 90)
            LookPlayer();
        else if (angle < -90 && angle > -180)
            anim.SetBool("LEFT", true);
        else
            anim.SetBool("RIGHT", true);
    }
    public void WatchFront()
    {
        float angle = CalculateAngle();
        if (angle >= -90 && angle <= 90)
            return;
        else if (angle < -90 && angle > -180)
            anim.SetBool("LEFT", false);
        else
            anim.SetBool("RIGHT", false);
    }
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
