using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleTeacherOnGuard : ImmovableEntity
{
    [SerializeField] float thresholdAngle= 60f;
    OnGuard onGuard;
    DetectPlayer detectPlayer;

    bool isRotate = false;

    public override void AdditionalInit() 
    {
        detectPlayer = GetComponentInChildren<DetectPlayer>();
        onGuard = GetComponent<OnGuard>();
    }

    public void MaintainAngle()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        directionToPlayer.y = 0; 
        Vector3 monsterForward = transform.forward;
        monsterForward.y = 0; 

        float angle = Vector3.Angle(monsterForward, directionToPlayer);
        if (angle > thresholdAngle && !isRotate)
        {
            isRotate = true;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            StartCoroutine(RotateCor(targetRotation));
        }
    }

    public IEnumerator RotateCor(Quaternion _target)
    {
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, _target, timer/1f);
            yield return null;
        }
        isRotate = false;
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
