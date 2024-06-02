using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGuard : ChaseEntity, IPatrol
{
    #region Patrol Variable
    [SerializeField] protected Vector3[] patrolPoints; // 순찰 지점 
    protected int currentPatrolPoint = 0; // 현재 이동하는 패트롤 지점
    protected int maxPatrolPoint = 0; // 패트롤 지점의 최대 수
    #endregion

    #region Guard Variable
    [Header("Use Teleport Variable")]
    [SerializeField] protected Vector3 guardRoomEntrance; // Teleport GuardRoom
    [SerializeField] protected Vector3 guardRoomToward;  // Look GuardRoom
    protected bool isDeathPenalty = false; // True : Collide Guard => Death
    protected bool isInRoom = false; // True : Player in GuardRoom
    protected bool isNearPlayer = false; // 주위에 플레이어가 있으면 말 걸기 쉽게 잠시 멈춤
    #endregion

    #region Override Setting
    public override void AdditionalSetup()
    {
        maxPatrolPoint = patrolPoints.Length;
    }
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
    public override void PenaltyEnter() { StopPatrol(); StateAnimation(currentState, true); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { StateAnimation(currentState, false); }
    public override void ChaseEnter() { }
    public override void ChaseExecute() { }
    public override void ChaseExit() { }
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
        if (isNearPlayer)
            return;
        if (nav.steeringTarget == null)
            SeekNextRoute();
        if (!nav.enabled)
            return;
        if (nav.remainingDistance < 0.5f)
            SeekNextRoute();
    }

    public void SeekNextRoute()
    {
        currentPatrolPoint = (currentPatrolPoint + 1) % maxPatrolPoint;
        nav.SetDestination(patrolPoints[currentPatrolPoint]);
        anim.SetFloat("WALKVAL", 0.5f);
    }

    public void StopPatrol()
    {
        nav.ResetPath();
        anim.SetFloat("WALKVAL", 0f);
    }

    #endregion

    #region Detect
    public void DetectPlayer()
    {
        Vector3 direction = transform.position + eyeTransform - playerTransform.position;
        if (direction.magnitude > detectDistance)
        {
            isNearPlayer = false;
            return;
        }
        isNearPlayer = true;
        if (isInRoom)
            TeleportGuardRoom();
        else
            StopPatrol();
    }
    public void TeleportGuardRoom()
    {
        IdealSceneManager.Instance.CurrentGameManager.Entity_Manager.SendPenaltyMesage(this.name);
        anim.SetFloat("WALKVAL", 0f);
        nav.enabled = false;
        transform.position = guardRoomEntrance;
        transform.rotation = Quaternion.Euler(guardRoomToward);
        isDeathPenalty = true;
    }

    public void OnCollisionEnter(Collision collision)
    {   
        if (collision.collider.CompareTag("Player") && isDeathPenalty)
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver("경비에게 끌려간 뒤, 피투성이가 되어 발견");
        }
    }
    #endregion

    #region Animation
    public override void StateAnimation(EntityStateType _entityState, bool _setBool)
    {
        switch (_entityState)
        {
            case EntityStateType.Idle:
                anim.SetBool("WALK", _setBool);
                break;
            case EntityStateType.Talk:
                anim.SetBool("IDLE", _setBool);
                break;
            case EntityStateType.Quiet:
                anim.SetBool("WALK", _setBool);
                break;
            case EntityStateType.Penalty:
                anim.SetBool("IDLE", _setBool);
                break;
            case EntityStateType.Chase:
                anim.SetBool("WALK", _setBool);
                break;
            default:
                break;
        }
    }
    #endregion
}
