using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleTeacher_OnGuard : ImmovableEntity
{
    [SerializeField] float thresholdAngle= 60f;
    [SerializeField] OnGuard onGuard;
    [SerializeField] DetectPlayer detectPlayer;

    public override void AdditionalInit() 
    {
        detectPlayer = GetComponentInChildren<DetectPlayer>();
        onGuard = GetComponent<OnGuard>();
    }

    public void MaintainAngle()
    {
        Vector3 directionToTarget = playerTransform.position - transform.position;
        directionToTarget.y = 0; // 높이 무시

        // 현재 플레이어가 바라보는 방향과 목표물 사이의 각도 계산
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        // 각도가 thresholdAngle을 넘으면 물체쪽으로 회전
        if (angle > thresholdAngle)
        {
            // 물체를 바라보는 회전 계산
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            // 플레이어를 해당 방향으로 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    public override void IdleEnter() { SetAnimation(currentType, true); }
    public override void IdleExecute() { if (detectPlayer.DetectExecute()) { ChangeState(EntityStateType.Penalty); } }
    public override void IdleExit() { SetAnimation(currentType, false); }
    public override void TalkEnter() { SetAnimation(currentType, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { SetAnimation(currentType, false); }
    public override void QuietEnter() { SetAnimation(currentType, true); }
    public override void QuietExecute() { }
    public override void QuietExit() { SetAnimation(currentType, false); }
    public override void PenaltyEnter() { SetAnimation(currentType, true); onGuard.GazePlayer(playerTransform); }
    public override void PenaltyExecute() { MaintainAngle(); if (!detectPlayer.DetectExecute()) ChangeState(EntityStateType.Idle); }
    public override void PenaltyExit() { SetAnimation(currentType, false); onGuard.GazeFront(); }
}
