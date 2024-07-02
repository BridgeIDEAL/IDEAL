using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Principal : MovableEntity, IPatrol
{
    // Patrol Val
    int currentPoint;
    int maxPoint;
    float stopDistance;

  

    [SerializeField, Tooltip("�÷��̾� Transform")] Transform playerTransform;
    [SerializeField, Tooltip("���� ������")] Vector3[] patrolPoints;
    [SerializeField, Tooltip("�ڽ��� �� ��ġ")] Vector3 studyRoomFrontPoint;
    [SerializeField, Tooltip("�ڽ��� �� ��ġ�� �ٶ󺸴� ����")] Vector3 studyRoomFrontRotation;

    public override void AdditionalInit()
    {
        currentPoint = 0;
        maxPoint = patrolPoints.Length - 1;
        stopDistance = agent.radius;
    }

    #region Implement Patrol Interface
    public void OnOffPatrol(bool _isOnPatrol = true)
    {
        if (_isOnPatrol) // ��Ʈ�� ����
        {
            StartCoroutine("PatrolCor");
            return;
        }
        else // ��Ʈ�� �ߴ�
        {
            agent.ResetPath();
            StopCoroutine("PatrolCor");
            return;
        }
    }

    public IEnumerator PatrolCor()
    {
        CheckMaintainCurrentRoute();
        agent.SetDestination(patrolPoints[currentPoint]);
        while (agent.remainingDistance > stopDistance)
        {
            yield return null;
        }
        SeekNextRoute();
        StartCoroutine("PatrolCor");
    }

    public void CheckMaintainCurrentRoute()
    {
        if (agent.remainingDistance < stopDistance)
            SeekNextRoute();
    }

    public void SeekNextRoute()
    {
        currentPoint += 1;
        if (currentPoint > maxPoint)
            currentPoint = 0;
    }
    #endregion

    #region Act Method
    public void ChasePlayer()
    {
        if (playerTransform == null)
                return;
        agent.SetDestination(playerTransform.position);
    }
    #endregion


    #region Implement Act Frame

    // Idle
    public override void IdleEnter()
    {
        base.IdleEnter();
        OnOffPatrol(true);
    }
    public override void IdleExecute() { }
    public override void IdleExit()
    {
        base.IdleExit();
        OnOffPatrol(false);
    }

    // Talk
    public override void TalkEnter()
    {
        base.TalkEnter();
    }
    public override void TalkExecute() { }
    public override void TalkExit()
    {
        base.TalkExit();
    }

    // Quiet
    public override void QuietEnter()
    {
        base.QuietEnter();
    }
    public override void QuietExecute() { }
    public override void QuietExit()
    {
        base.QuietExit();
    }

    // Penalty
    public override void PenaltyEnter()
    {
        base.PenaltyEnter();
    }
    public override void PenaltyExecute() { }
    public override void PenaltyExit()
    {
        base.PenaltyExit();
    }

    // Chase
    public override void ChaseEnter()
    {
        base.ChaseEnter();
    }
    public override void ChaseExecute() 
    {
    }
    public override void ChaseExit()
    {
        base.ChaseExit();
    }
    #endregion
}
