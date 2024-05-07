using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Principal : ChaseEntity, IPatrol
{
    #region Patrol Variable
    [Header("Patrol Variable")]
    [SerializeField] protected Vector3[] patrolPoints; // 순찰 지점 
    [SerializeField] protected float coolDownTimer = 30f; // 재추격까지의 타이머
    protected float sightDetectDistance; // 시야
    protected bool canDetectPlayer = true;
    protected int currentPatrolPoint = 0; // 현재 이동하는 패트롤 지점
    protected int maxPatrolPoint = 0; // 패트롤 지점의 최대 수
    #endregion

    #region Principal Variable
    protected float ratioChaseSpeed; // 추격 속도 비율
    protected bool isInRoom = false;
    #endregion

    #region Override Setting
    public override void AdditionalSetup()
    {
        sightDetectDistance = entityData.sightDetectDistance;
        ratioChaseSpeed = entityData.ratioChaseSpeed;
        maxPatrolPoint = patrolPoints.Length;
    }
    public override void IsInRoom(bool _isInRoom) { isInRoom = _isInRoom; }
    #endregion

    #region BehaviourState
    public override void IdleEnter() { StateAnimation(currentState, true); SeekNextRoute(); }
    public override void IdleExecute() { DetectPlayer(); Patrol(); }
    public override void IdleExit() { StateAnimation(currentState, false); }
    public override void TalkEnter() { StopPatrol(); StateAnimation(currentState, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { StateAnimation(currentState, false); }
    public override void QuietEnter() { StateAnimation(currentState, true); SeekNextRoute(); }
    public override void QuietExecute() { Patrol(); }
    public override void QuietExit() { StateAnimation(currentState, false); }
    public override void PenaltyEnter() { StateAnimation(currentState, true); StopPatrol(); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { StateAnimation(currentState, false); }
    public override void ChaseEnter() { StateAnimation(currentState, true); IsChasePlayer = true; }
    public override void ChaseExecute() { ChasePlayer(); }
    public override void ChaseExit() { StateAnimation(currentState, false); IsChasePlayer = false; }
    #endregion

    #region Empty Method
    /// <summary>
    /// 추가로 넣을 행동이 없다면 폐기
    /// </summary>
    public override void ExtraEnter() { /*StateAnimation(currentState, true); */}
    public override void ExtraExecute() { }
    public override void ExtraExit() { /*StateAnimation(currentState, false);*/ }
    #endregion

    #region Interface Patrol
    public void Patrol()
    {
        if (nav.steeringTarget == null)
            return;
        if (nav.remainingDistance < 0.5f)
            SeekNextRoute();
    }

    public void SeekNextRoute()
    {
        currentPatrolPoint = (currentPatrolPoint + 1) % maxPatrolPoint;
        nav.SetDestination(patrolPoints[currentPatrolPoint]);
        anim.SetFloat("Speed", 0.5f);
    }

    public void StopPatrol()
    {
        nav.ResetPath();
    }

    #endregion

    #region Detect & Chase
    public void DetectPlayer()
    {
        if (!canDetectPlayer)
            return;
        Vector3 direction = eyeTransform.position - playerTransform.position;
        if (direction.magnitude > sightDetectDistance)
            return;
        else
        {
            RaycastHit rayHit;
            if (Physics.Raycast(eyeTransform.position, direction, out rayHit, sightDetectDistance, playerLayer))
            {
                ChangeState(ChaseEntityStates.Chase);
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && isChasePlayer)
        {
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastPenalty(this.name);
            IsChasePlayer = false;
            ChangeState(ChaseEntityStates.Idle);
            StartCoroutine(CoolDownTimer());
            Debug.Log("벌점주는중!");
        }
    }
    public IEnumerator CoolDownTimer()
    {
        canDetectPlayer = false;
        yield return new WaitForSeconds(coolDownTimer);
        canDetectPlayer = true;
    }
    public override void ChasePlayer()
    {
        if(isInRoom)
        {
            anim.SetFloat("Speed", 0f);
            nav.ResetPath();
        }
        else
        {
            anim.SetFloat("Speed", 0.5f);
            base.ChasePlayer();
        }
    }
    #endregion

    #region Animation
    public override void StateAnimation(ChaseEntityStates _entityState, bool _setBool)
    {
        switch (_entityState)
        {
            case ChaseEntityStates.Idle:
                anim.SetBool("Move", _setBool);
                break;
            case ChaseEntityStates.Talk:
                anim.SetBool("Stand", _setBool);
                break;
            case ChaseEntityStates.Quiet:
                anim.SetBool("Stand", _setBool);
                break;
            case ChaseEntityStates.Penalty:
                anim.SetBool("Stand", _setBool);
                break;
            case ChaseEntityStates.Chase:
                anim.SetBool("Move", _setBool);
                break;
            default:
                break;
        }
    }
    #endregion
}
