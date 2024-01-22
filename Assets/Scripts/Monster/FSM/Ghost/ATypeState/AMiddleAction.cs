using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AMiddleAction : AType
{
    #region Component
    public enum TurnState { Straight, Left, Right }
    public TurnState GazeDir { protected set; get; } = TurnState.Straight;
    #endregion

    #region Override
    public override void StartConversationInteraction() {
        if (transform.position.x - playerObject.transform.position.x > 0)
            GazeDir = TurnState.Left; // 플레이어가 왼쪽에 있음
        else
            GazeDir = TurnState.Right; // 플레이어가 오른쪽에 있음
        ChangeState(ATypeEntityStates.Interaction);
    }
    public override void EndConversationInteraction() { 
        if(CurrentType == ATypeEntityStates.Interaction)
        {
            if(GazeDir== TurnState.Left)
                anim.CrossFade("BackLeft", 0.2f);
            else if(GazeDir==TurnState.Right)
                anim.CrossFade("BackRight", 0.2f);
            GazeDir = TurnState.Straight;
        }
        else
        {
            ChangeState(ATypeEntityStates.Indifference);
        }
    }
    public override void SpeechlessInteraction() { ChangeState(ATypeEntityStates.Speechless); }
    public override void SetAnimation(ATypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case ATypeEntityStates.Indifference:
                anim.CrossFade("Idle", 0.2f);
                break;
            case ATypeEntityStates.Interaction:
                if(GazeDir == TurnState.Left)
                    anim.CrossFade("TurnLeft", 0.2f);
                else if (GazeDir == TurnState.Right)
                    anim.CrossFade("TurnRight", 0.2f);
                break;
            case ATypeEntityStates.Speechless:
                anim.CrossFade("Idle", 0.2f);
                break;
        }
    }
    #endregion
}
