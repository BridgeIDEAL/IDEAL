using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PrincipalPatrol : MovableEntity, IPatrol
{
    [SerializeField, Header("Disappear Effect")] DissolveEffect dissolveEffect;
    [SerializeField] GameObject redLight;
    UnityAction dissolveAction = null;

    #region Chase/Detect Val
    [Header("Chase/Detect")]
    [SerializeField] VisibleDetectPlayer detectPlayer;
    [SerializeField] JumpScarePrincipal jumpscare;
    [SerializeField] ChaseCollisionDetect collisionDetect;
    #endregion

    #region Patrol Val
    [Header("Move")]
    [SerializeField, Tooltip("���� ������")] Vector3[] patrolPoints;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField, Range(0.1f, 5f)] float walkMotionSpeed;
    [SerializeField, Range(0.1f,5f)] float runMotionSpeed;

    int currentPoint = 0;
    int maxPoint;
    float stopPatrolDistance = 0.8f;
    #endregion
    
    public override void Init(Transform _playerTransfrom, Transform _playerHeightTransform)
    {
        base.Init(_playerTransfrom, _playerHeightTransform);
        maxPoint = patrolPoints.Length - 1;
        dissolveEffect.Init();
        dissolveAction += ReturnStartPoint;

        collisionDetect.Init(_playerTransfrom);

        #region Init Patrol Point Height
        float _height = 3.5f;
        int _poinCnt = patrolPoints.Length;
        switch (EventDataManager.Instance.Notice.CurrentTeleportPoint)
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
        if (agent.enabled != false)
            agent.SetDestination(patrolPoints[currentPoint]);
    }

    public void Patrol()
    {
        if (agent.enabled == false)
            return;
        if (agent.remainingDistance < stopPatrolDistance)
        {
            SeekNextRoute();
            return;
        }
    }

    public void SeekNextRoute()
    {
        if (agent.enabled == false)
            return;

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

    bool isInStudyRoom = false;
    
    public void PlayerInStudyRoom(Transform keepAnEyeTransform, bool isSelfStudyroom=false)
    {
        StopAllCoroutines();
        isInStudyRoom= true;
        if (EntityDataManager.Instance.Controller.IsChase)
        {
            StartCoroutine(MoveToKeepAnEyePosition(keepAnEyeTransform));
            if (isSelfStudyroom)
            {
                if(MonsterArchiveLogManager.Instance.GetMonsterArchiveLogs(8).isImageActive){
                    MonsterArchiveLogManager.Instance.UpdateArchiveLogData(0802, CountAttempts.Instance.GetAttemptCount());
                }
                RoomArchiveLogManager.Instance.UpdateArchiveLogData(0402, CountAttempts.Instance.GetAttemptCount());
            }
        }
    }

    public void PlayerOutStudyRoom()
    {
        StopAllCoroutines();
        isInStudyRoom= false;
        anim.SetBool("IsMove", true);
    }

    public IEnumerator MoveToKeepAnEyePosition(Transform keepAnEyeTransform)
    {
        agent.SetDestination(keepAnEyeTransform.position);
        while (agent.enabled!=false && agent.remainingDistance > 0.1f) 
        {
            yield return null;
        }
        agent.enabled = false;
        this.transform.position = keepAnEyeTransform.position;
        agent.enabled = true;

        float timer = 0f;
        anim.SetBool("IsMove", false);
        Quaternion keepAnEyeRotation = keepAnEyeTransform.rotation;

        while (timer <= 1f)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, keepAnEyeRotation, timer / 1f);
            yield return null;
        }
        transform.rotation = keepAnEyeRotation;
    }
    #endregion

    #region Solve Chase
    public void SolveChaseState()
    {
        agent.enabled = false;
        currentPoint = 0;
        dissolveEffect.Dissolve(dissolveAction);
    }

    public void ReturnStartPoint()
    {
        transform.position = patrolPoints[0];
        agent.enabled = true;
        dissolveEffect.RestoreDissolve();
        controller.SendMessage(EntityStateType.Idle);
    }
    #endregion

    #region Idle State
    public override void IdleEnter()
    {
        StartPatrol();
        agent.speed = walkSpeed;
        anim.SetFloat("MoveValue", walkMotionSpeed);
        anim.SetBool("IsMove", true);
    }

    public override void IdleExecute()
    {
        Patrol();
        if (detectPlayer.DetectExecute() && !isInStudyRoom)
        {
            controller.SendMessage(gameObject.name, EntityStateType.Chase, EntityStateType.Quiet);
        }
    }

    public override void IdleExit()  { EndPatrol(); }
    #endregion

    #region Quiet State
    public override void QuietEnter()
    {
        anim.SetBool("IsMove", false);
        agent.enabled = false;
        redLight.SetActive(false);
        dissolveEffect.Dissolve();
    }

    public override void QuietExecute() { }
    
    public override void QuietExit()
    {
        anim.SetBool("IsMove", true);
        agent.enabled = true;
        redLight.SetActive(true);
        dissolveEffect.RestoreDissolve();
    }
    #endregion

    #region Chase State
    
    public override void ChaseEnter()
    {
        Controller.PrincipalChase = true;
        anim.SetBool("IsMove", true);
        agent.speed = runSpeed;
        anim.SetFloat("MoveValue", runMotionSpeed);
        EntityDataManager.Instance.Controller.IsChase = true;

        IdealSceneManager.Instance.CurrentGameManager.scriptHub.cameraEffectManager.AddChasingEntities(this.transform);
        MonsterArchiveLogManager.Instance.UpdateArchiveImageData(8);
    }

    public override void ChaseExecute() 
    {
        if (!isInStudyRoom)
        {
            if(agent.enabled!=false)
                agent.SetDestination(playerTransform.position);
            
            if(collisionDetect.IsCollidePlayer())
            {
                anim.SetBool("IsMove", false);
                ChangeState(EntityStateType.Quiet);
                Controller.InActiveExceptOne(this.gameObject);
                IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.MoveLock = true;

                IdealSceneManager.Instance.CurrentGameManager.scriptHub.cameraEffectManager.AddChasingEntities(this.transform);

                jumpscare.ActiveJumpScare();
                if (SteamfeatureController.Instance.FeatureManager.Achievement03.isPrincipalDeath == false)
                {
                    SteamfeatureController.Instance.FeatureManager.Achievement03.isPrincipalDeath = true;
                    SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
                }
            }
        }
    }

    public override void ChaseExit()
    {
        Controller.PrincipalChase = false;
        detectPlayer.IsDetectPlayer = false;
        EntityDataManager.Instance.Controller.IsChase = false;

        IdealSceneManager.Instance.CurrentGameManager.scriptHub.cameraEffectManager.RemoveChasingEntities(this.transform);
    }
    #endregion

    #region Talk State : Not Use Now
    public override void TalkEnter() { }
    public override void TalkExecute() { }
    public override void TalkExit() { }
    #endregion

    #region Penalty State : Not Use Now
    public override void PenaltyEnter() { }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { }
    #endregion
}
