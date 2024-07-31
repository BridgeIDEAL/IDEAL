using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : MovableEntity, IPatrol
{
    #region Patrol Val
    [Header("Patrol")]
    [SerializeField, Tooltip("���� ������")] Vector3[] patrolPoints;
    [SerializeField, Range(0.5f, 1f)] float checkPatrolDistance;
    int currentPoint;
    int maxPoint;

    [Header("Dialogue")]
    [SerializeField] DetectPlayer detectPlayer;
    bool onceTalk = true;

    #endregion

    public override void Init(Transform _playerTransfrom)
    {
        base.Init(_playerTransfrom);
        currentPoint = 1;
        maxPoint = patrolPoints.Length;
    }

    public override void Setup()
    {
        base.Setup();
        if (Entity_Data.speakIndex == -1)
            onceTalk = false;
    }

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
        if (currentPoint >= maxPoint)
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
            Talk();
        }
    }

    public void Talk()
    {
        string talkID = Entity_Data.speakerName + Entity_Data.speakIndex;
        ProgressManager.Instance.UpdateCheckList(102, 1);
        DialogueManager.Instance.StartDialogue(talkID, this);
        StartCoroutine(MoveAndRotateTowardsPlayer());
    }

    IEnumerator MoveAndRotateTowardsPlayer()
    {
        Vector3 startPosition = transform.position;  
        Vector3 targetPosition = playerTransform.position;
        Quaternion startRotation = transform.rotation; 
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - startPosition); 
        
        float timer  = 0; 
        while (timer < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timer / 1f);
            timer += Time.deltaTime; 
            yield return null;  
        }

        transform.rotation = targetRotation;
    }

    public override void ChangeState(EntityStateType _changeType)
    {
        currentType = _changeType;
        stateMachine.ChangeState(states[(int)currentType]);
    }

    // To Do ~~~ Look Player
    #endregion

    int testCnt = 0;
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
            default:
                break;
        }
    }
    #endregion

    #region Idle
    public override void IdleEnter() { SetAnimation(EntityStateType.Idle, true); StartPatrol(); }
    public override void IdleExecute() { Patrol(); DetectPlayer(); }
    public override void IdleExit() { SetAnimation(EntityStateType.Idle, false); EndPatrol(); }
    #endregion

    #region Talk
    public override void TalkEnter() {  SetAnimation(EntityStateType.Talk, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { SetAnimation(EntityStateType.Talk, false); }
    #endregion

    #region Quiet
    public override void QuietEnter() { SetAnimation(EntityStateType.Quiet, true); }
    public override void QuietExecute() { }
    public override void QuietExit() { SetAnimation(EntityStateType.Quiet, false); }
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
