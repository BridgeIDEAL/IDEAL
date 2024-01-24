using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AMiddleAction : AType
{
    #region Component
    public enum DetectDir { XR, ZL, XL, ZR, N }
    public DetectDir CalDir { set; get; } = DetectDir.N; // 계산 방향
    public enum RoateState { Straight, Left, Right }
    public RoateState GazeDir { set; get; } = RoateState.Straight; // 바라봐야 하는 방향
    #endregion

    #region StateBehavior
    public override void IndifferenceEnter() { base.IndifferenceEnter(); }
    public override void IndifferenceExecute() { }
    public override void IndifferenceExit() { }
    public override void InteractionEnter() { base.InteractionEnter(); }
    public override void InteractionExecute() { }
    public override void InteractionExit() { }
    public override void SpeechlessEnter() { base.SpeechlessEnter(); }
    public override void SpeechlessExecute() { }
    public override void SpeechlessExit() { }
    #endregion

    #region Override
    public override void AdditionalSetup() { anim = GetComponent<Animator>(); }
    public override void StartConversationInteraction() {
        CalRotateDir();
        ChangeState(ATypeEntityStates.Interaction);
    }

    public override void EndConversationInteraction() { 
        if(CurrentType == ATypeEntityStates.Interaction)
        {
            if (GazeDir == RoateState.Left)
                anim.CrossFade("BackLeft", 0.2f);
            else if (GazeDir == RoateState.Right)
                anim.CrossFade("BackRight", 0.2f);
            GazeDir = RoateState.Straight;
            return; 
        }
        else if(CurrentType == ATypeEntityStates.Speechless)
            ChangeState(ATypeEntityStates.Indifference);
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
                if(GazeDir == RoateState.Left)
                    anim.CrossFade("TurnLeft", 0.2f);
                else if (GazeDir == RoateState.Right)
                    anim.CrossFade("TurnRight", 0.2f);
                break;
            case ATypeEntityStates.Speechless:
                anim.CrossFade("Idle", 0.2f);
                break;
        }
    }
    #endregion

    #region Method
    public void SetCalDir(string _Dir)
    {
        switch (_Dir)
        {
            case "XR":
                CalDir = DetectDir.XR;
                break;
            case "XL":
                CalDir = DetectDir.XL;
                break;
            case "ZR":
                CalDir = DetectDir.ZR;
                break;
            case "ZL":
                CalDir = DetectDir.ZL;
                break;
            default:
                break;
        }
    }

    public void CalRotateDir()
    {
        switch (CalDir)
        {
            case DetectDir.XR: // 0도
                if (playerObject.transform.position.x - transform.position.x > 0)
                    GazeDir = RoateState.Right; // 플레이어가 왼쪽에 있음
                else
                    GazeDir = RoateState.Left; // 플레이어가 오른쪽에 있음
                break;
            case DetectDir.XL: // 180도
                if (playerObject.transform.position.x - transform.position.x > 0)
                    GazeDir = RoateState.Left; // 플레이어가 왼쪽에 있음
                else
                    GazeDir = RoateState.Right; // 플레이어가 오른쪽에 있음
                break;
            case DetectDir.ZR: // 270도
                if (playerObject.transform.position.z - transform.position.z > 0)
                    GazeDir = RoateState.Right; // 플레이어가 왼쪽에 있음
                else
                    GazeDir = RoateState.Left; // 플레이어가 오른쪽에 있음
                break;
            case DetectDir.ZL: // 90도
                if (playerObject.transform.position.z - transform.position.z > 0)
                    GazeDir = RoateState.Left; // 플레이어가 왼쪽에 있음
                else
                    GazeDir = RoateState.Right; // 플레이어가 오른쪽에 있음
                break;
            default:
                break;
        }
    } 
    #endregion
}
