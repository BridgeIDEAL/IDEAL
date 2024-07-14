using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Principal_Patrol : MovableEntity, IPatrol
{
    bool isCallChangeState = false;

    #region Patrol Val
    int currentPoint;
    int maxPoint;
    float stopDistance;

    [Header("Patrol")]
    [SerializeField, Tooltip("순찰 지점들")] Vector3[] patrolPoints;
    #endregion

    #region In StudyRoom Val
    bool isRotate = false;
    bool isInStudyRoom = false;
    public bool IsInStudyRoom { set { isInStudyRoom = value; if (value) agent.SetDestination(studyRoomFrontPoint); else isRotate = false; } }
    [Header("StudyRoom")]
    [SerializeField, Tooltip("자습실 앞 위치")] Vector3 studyRoomFrontPoint;
    [SerializeField, Tooltip("자습실 앞 위치를 바라보는 각도")] Vector3 studyRoomFrontRotation;
    #endregion

    /// <summary>
    /// Set Patrol Point
    /// </summary>
    public override void AdditionalInit()
    {
        currentPoint = 0;
        maxPoint = patrolPoints.Length - 1;
    } 

    #region Implement Patrol Interface
    public void OnOffPatrol(bool _isOnPatrol = true)
    {
        if (_isOnPatrol) // 패트롤 시작
        {
            StartCoroutine("PatrolCor");
            return;
        }
        else // 패트롤 중단
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

    #region In StudyRoom
    public override void EntityTriggerEvent(bool _isActive = true) { IsInStudyRoom = _isActive; }

    public void PlayerInStudyRoom()
    {
        if (!isRotate && agent.remainingDistance < 0.1f)
        {
            StartCoroutine(RotateCor());
        }
    }

    public IEnumerator RotateCor()
    {
        isRotate = true;
        float timer = 0f;
        Quaternion lookQuaternion = Quaternion.Euler(studyRoomFrontRotation);
        while (timer <= 1f)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, lookQuaternion, timer / 1f);
            yield return null;
        }
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

    #region Idle State
    public override void IdleEnter()
    {
        base.IdleEnter();
        OnOffPatrol(true);
    }

    public override void IdleExecute() 
    {
        if (detectPlayer.DetectExecute() && !isCallChangeState)
        {
            isCallChangeState = true;
            controller.SendMessage(data.speakerName, EntityStateType.Chase, EntityStateType.Quiet);
        }
    }

    public override void IdleExit()
    {
        base.IdleExit();
        isCallChangeState = false;
        OnOffPatrol(false);
    }
    #endregion

    #region Talk State : When you catch by Principal
    public override void TalkEnter()
    {
        base.TalkEnter();
    }
    public override void TalkExecute() { }
    public override void TalkExit()
    {
        base.TalkExit();
    }
    #endregion

    #region Quiet State
    public override void QuietEnter()
    {
        base.QuietEnter();
    }

    public override void QuietExecute() { }
    
    public override void QuietExit()
    {
        base.QuietExit();
    }
    #endregion

    #region Penalty State : Not Use
    public override void PenaltyEnter()
    {
        base.PenaltyEnter();
    }
    public override void PenaltyExecute() { }
    public override void PenaltyExit()
    {
        base.PenaltyExit();
    }
    #endregion

    #region Chase State
    public override void ChaseEnter()
    {
        base.ChaseEnter();
    }

    public override void ChaseExecute() 
    {
        if (isInStudyRoom)
            agent.SetDestination(playerTransform.position);
        else
            PlayerInStudyRoom();
    }

    public override void ChaseExit()
    {
        base.ChaseExit();
        isRotate = false;
        detectPlayer.IsDetectPlayer = false;
    }
    #endregion
}
