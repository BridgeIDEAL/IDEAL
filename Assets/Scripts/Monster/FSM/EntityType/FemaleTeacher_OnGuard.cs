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
        directionToTarget.y = 0; // ���� ����

        // ���� �÷��̾ �ٶ󺸴� ����� ��ǥ�� ������ ���� ���
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        // ������ thresholdAngle�� ������ ��ü������ ȸ��
        if (angle > thresholdAngle)
        {
            // ��ü�� �ٶ󺸴� ȸ�� ���
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            // �÷��̾ �ش� �������� ȸ��
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
