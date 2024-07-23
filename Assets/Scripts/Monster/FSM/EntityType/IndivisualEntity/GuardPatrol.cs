using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : MovableEntity, IPatrol
{

    #region Patrol Val
    [SerializeField] int currentPoint;
    [SerializeField] int maxPoint;
    [SerializeField, Range(0.5f, 1f)] float stopDistance;
    bool isChasePlayer = false;

    [Header("Patrol")]
    [SerializeField, Tooltip("¼øÂû ÁöÁ¡µé")] Vector3[] patrolPoints;
    #endregion

    #region Patrol Interface
    public void StartPatrol()
    {
        agent.SetDestination(patrolPoints[currentPoint]);
    }

    public void Patrol()
    {
        if (agent.remainingDistance < stopDistance)
        {
            SeekNextRoute();
            return;
        }
    }

    public void SeekNextRoute()
    {
        currentPoint += 1;
        if (currentPoint > maxPoint)
            currentPoint = 0;
        agent.SetDestination(patrolPoints[currentPoint]);
    }

    public void EndPatrol()
    {
        agent.ResetPath();
    }
    #endregion

    #region Animation
    public override void SetAnimation(EntityStateType _currentType, bool _isStart)
    {
        switch (_currentType)
        {
            //case EntityStateType.Idle:
            //    anim.SetBool("Idle", _isStart);
            //    break;
            //case EntityStateType.Talk:
            //    anim.SetBool("Talk", _isStart);
            //    break;
            //case EntityStateType.Quiet:
            //    anim.SetBool("Quiet", _isStart);
            //    break;
            //case EntityStateType.Penalty:
            //    anim.SetBool("Penalty", _isStart);
            //    break;
            //case EntityStateType.Chase:
            //    anim.SetBool("Chase", _isStart);
            //    break;
            //default:
            //    break;
        }
    }
    #endregion

    #region Idle
    public override void IdleEnter() { SetAnimation(currentType, true); }
    public override void IdleExecute() { }
    public override void IdleExit() { SetAnimation(currentType, false); }
    #endregion

    #region Talk
    public override void TalkEnter() { SetAnimation(currentType, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { SetAnimation(currentType, false); }
    #endregion

    #region Quiet
    public override void QuietEnter() { SetAnimation(currentType, true); }
    public override void QuietExecute() { }
    public override void QuietExit() { SetAnimation(currentType, false); }
    #endregion

    #region Penalty : Not Use
    public override void PenaltyEnter() { SetAnimation(currentType, true); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { SetAnimation(currentType, false); }
    #endregion

    #region Chase : Not Use
    public override void ChaseEnter() { SetAnimation(currentType, true); }
    public override void ChaseExecute() { }
    public override void ChaseExit() { SetAnimation(currentType, false); }
    #endregion
}
