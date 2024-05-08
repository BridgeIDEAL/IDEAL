using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGuard : ChaseEntity, IPatrol
{
    #region Patrol Variable
    [SerializeField] protected Vector3[] patrolPoints; // ���� ���� 
    protected int currentPatrolPoint = 0; // ���� �̵��ϴ� ��Ʈ�� ����
    protected int maxPatrolPoint = 0; // ��Ʈ�� ������ �ִ� ��
    protected float sightDetectDistance; // �þ�
    #endregion

    #region Guard Variable
    [Header("Use Teleport Variable")]
    [SerializeField] protected Vector3 guardRoomEntrance; // Teleport GuardRoom
    [SerializeField] protected Vector3 guardRoomToward;  // Look GuardRoom
    protected bool isDeathPenalty = false; // True : Collide Guard => Death
    protected bool isInRoom = false; // True : Player in GuardRoom
    #endregion

    #region Override Setting
    public override void AdditionalSetup()
    {
        sightDetectDistance = entityData.sightDetectDistance;
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
    public override void PenaltyEnter() { StopPatrol(); StateAnimation(currentState, true); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { StateAnimation(currentState, false); }
    public override void ChaseEnter() { }
    public override void ChaseExecute() { }
    public override void ChaseExit() { }
    #endregion

    #region Empty Method
    /// <summary>
    /// �߰��� ���� �ൿ�� ���ٸ� ���
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
    }

    #endregion

    #region Detect
    public void DetectPlayer()
    {
        Vector3 direction = eyeTransform.position - playerTransform.position;
        if (direction.magnitude > sightDetectDistance)
            return;
        if (isInRoom)
            TeleportGuardRoom();
    }
    public void TeleportGuardRoom()
    {
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastPenalty(this.name);
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
            Debug.Log("���� ����!");
        }
    }
    #endregion

    #region Animation
    public override void StateAnimation(ChaseEntityStates _entityState, bool _setBool)
    {
        switch (_entityState)
        {
            case ChaseEntityStates.Idle:
                anim.SetBool("WALK", _setBool);
                break;
            case ChaseEntityStates.Talk:
                anim.SetBool("IDLE", _setBool);
                break;
            case ChaseEntityStates.Quiet:
                anim.SetBool("WALK", _setBool);
                break;
            case ChaseEntityStates.Penalty:
                anim.SetBool("IDLE", _setBool);
                break;
            case ChaseEntityStates.Chase:
                anim.SetBool("WALK", _setBool);
                break;
            default:
                break;
        }
    }
    #endregion
}
