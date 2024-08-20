using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrincipalPatrol : MovableEntity, IPatrol
{
    [SerializeField] float chaseCoolDownTimer;
    protected DetectPlayer detectPlayer;
    #region Patrol Val
    [SerializeField] int currentPoint;
    [SerializeField] int maxPoint;
    float stopDistance = 0.8f;

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
    
    public override void Init(Transform _playerTransfrom)
    {
        base.Init(_playerTransfrom);

        detectPlayer = GetComponentInChildren<DetectPlayer>();
        currentPoint = 0;
        maxPoint = patrolPoints.Length - 1;

        #region Set Patrol Point Height
        float _height = 3.5f;
        int _poinCnt = patrolPoints.Length;
        switch (EntityDataManager.Instance.Notice.CurrentTeleportPoint)
        {
            case TeleportPoint.BuildingB_1F:
                _height *= 0;
                break;
            case TeleportPoint.BuildingB_2F:
                _height *= 1;
                break;
            case TeleportPoint.BuildingB_3F:
                _height *= 2;
                break;
            default:
                return;
        }

        agent.enabled = false;
        for(int i=0; i<_poinCnt; i++)
        {
            patrolPoints[i].y = _height;
        }
        transform.position = patrolPoints[0];
        agent.enabled = true;
        #endregion
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

    public void PlayerInStudyRoom()
    {
        onceInStudyroom = true;
        if (!isRotate && agent.remainingDistance < 0.1f)
        {
            StartCoroutine(RotateCor());
        }
    }

    public IEnumerator RotateCor()
    {
        isRotate = true;
        float timer = 0f;
        anim.SetBool("Run",false);
        anim.SetBool("Idle",true);
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

    public void SolveChaseState()
    {
        controller.SendMessage(EntityStateType.Idle);
    }

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
            case EntityStateType.Chase:
                anim.SetBool("Run", _isStart);
                break;
            default:
                break;
        }
    }

    #endregion

    #region Idle State
    public override void IdleEnter()
    {
        base.IdleEnter();
        StartPatrol();
        agent.speed = walkSpeed;
        anim.SetFloat("WalkValue", walkMotionSpeed);
    }

    public override void IdleExecute()
    {
        Patrol();
        if (detectPlayer.DetectExecute() && !isInStudyRoom)
        {
            controller.SendMessage(gameObject.name, EntityStateType.Chase, EntityStateType.Quiet);
        }
    }

    public override void IdleExit()
    {
        base.IdleExit();
        EndPatrol();
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

    bool onceInStudyroom = false;

    #region Chase State
    public override void ChaseEnter()
    {
        base.ChaseEnter();
        agent.speed = runSpeed;
        anim.SetFloat("RunValue", runMotionSpeed);
        EntityDataManager.Instance.Controller.IsChase = true;
    }

    public override void ChaseExecute() 
    {
        if (!isInStudyRoom)
        {
            if (onceInStudyroom)
            {
                anim.SetBool("Idle", false);
                anim.SetBool("Run", true);
                onceInStudyroom = false;
            }
            agent.SetDestination(playerTransform.position);
        }
        else
            PlayerInStudyRoom();
    }

    public override void ChaseExit()
    {
        base.ChaseExit();
        isRotate = false;
        detectPlayer.IsDetectPlayer = false;
        EntityDataManager.Instance.Controller.IsChase = false;
        onceInStudyroom = false;
        anim.SetBool("Idle", false);
    }
    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(deathIndex);
        }
    }
    [SerializeField] float walkSpeed;
    [SerializeField] float walkMotionSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float runMotionSpeed;
    [SerializeField] int deathIndex;
}
