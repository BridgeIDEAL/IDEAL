using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : MovableEntity, IPatrol
{
    #region Patrol Val
    [Header("Patrol")]
    [SerializeField, Tooltip("¼øÂû ÁöÁ¡µé")] Vector3[] patrolPoints;
    [SerializeField] int currentPoint;
    [SerializeField] int maxPoint;
    [SerializeField, Range(0.5f, 1f)] float checkPatrolDistance;

    [Header("Dialogue")]
    [SerializeField] bool onceTalk = true;
    [SerializeField] DetectPlayer detectPlayer;

    #endregion

    #region Patrol Interface
    public void StartPatrol()
    {
        agent.SetDestination(patrolPoints[currentPoint]);
    }

    public void Patrol()
    {
        if (agent.remainingDistance < checkPatrolDistance)
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

    #region Dialogue Method
    public void DetectPlayer()
    {
        if (!detectPlayer.DetectExecute())
            return;
        if (onceTalk)
        {
            onceTalk = false;
            controller.SendMessage(gameObject.name, EntityStateType.Talk, EntityStateType.Quiet);
        }
    }

    public void Talk()
    {
        string talkID = Entity_Data.speakerName + Entity_Data.speakIndex;
        DialogueManager.Instance.StartDialogue(talkID, this);
    }

    // To Do ~~~ Look Player
    #endregion

    #region Animation
    public override void SetAnimation(EntityStateType _currentType, bool _isStart)
    {
        switch (_currentType)
        {
            case EntityStateType.Idle:
                anim.SetBool("Walk", _isStart);
                break;
            case EntityStateType.Talk:
                anim.SetBool("Idle", _isStart);
                break;
            case EntityStateType.Quiet:
                anim.SetBool("Idle", _isStart);
                break;
            case EntityStateType.Penalty:
                anim.SetBool("Idle", _isStart);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Idle
    public override void IdleEnter() { SetAnimation(currentType, true); StartPatrol(); }
    public override void IdleExecute() { Patrol(); DetectPlayer(); }
    public override void IdleExit() { SetAnimation(currentType, false); EndPatrol(); }
    #endregion

    #region Talk
    public override void TalkEnter() { SetAnimation(currentType, true); Talk(); }
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
