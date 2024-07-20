using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Principal_Patrol : MovableEntity, IPatrol
{
    bool isCallChangeState = false;
    bool isCoolDown = true;
    [SerializeField] float chaseCoolDownTimer;

    #region Patrol Val
    [SerializeField] int currentPoint;
    [SerializeField] int maxPoint;
    [SerializeField, Range(0.5f, 1f)] float stopDistance;
    bool isChasePlayer = false;

    [Header("Patrol")]
    [SerializeField, Tooltip("순찰 지점들")] Vector3[] patrolPoints;
    #endregion

    #region In StudyRoom Val
    bool isRotate = false;
    [SerializeField] bool isInStudyRoom = false;
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
        StartPatrol();
    }

    public override void IdleExecute()
    {
        if (isCoolDown && detectPlayer.DetectExecute() && !isCallChangeState)
        {
            isCoolDown = false;
            isCallChangeState = true;
            controller.SendMessage(entity_Data.speakerName, EntityStateType.Chase, EntityStateType.Quiet);
        }
        Patrol();
    }

    public override void IdleExit()
    {
        base.IdleExit();
        EndPatrol();
        isCallChangeState = false;
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
        StartCoroutine(ChaseCoolDownCor());
    }

    public IEnumerator ChaseCoolDownCor()
    {
        yield return new WaitForSeconds(chaseCoolDownTimer);
        isCoolDown = true;
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
        isChasePlayer = true;
        base.ChaseEnter();
    }

    public override void ChaseExecute() 
    {
        if (!isInStudyRoom)
            agent.SetDestination(playerTransform.position);
        else
            PlayerInStudyRoom();
    }

    public override void ChaseExit()
    {
        base.ChaseExit();
        isRotate = false;
        isChasePlayer = false;
        detectPlayer.IsDetectPlayer = false;
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(isChasePlayer && collision.collider.CompareTag("Player"))
        {
            DialogueManager.Instance.StartDialogue(entity_Data.speakerName);
            controller.SendMessage(entity_Data.speakerName, EntityStateType.Talk, EntityStateType.Quiet);
        }
    }
}
