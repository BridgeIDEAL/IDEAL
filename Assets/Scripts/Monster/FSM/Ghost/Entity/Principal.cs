using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Principal : ChaseEntity, IPatrol
{
    #region Patrol Variable
    [Header("Patrol Variable")]
    [SerializeField] protected Vector3[] patrolPoints; // ���� ���� 
    [SerializeField] protected float coolDownTimer = 30f; // ���߰ݱ����� Ÿ�̸�

    protected bool canDetectPlayer = true; // Cool Time 
    [SerializeField] protected int currentPatrolPoint = 0; // ���� �̵��ϴ� ��Ʈ�� ����
    [SerializeField] protected int maxPatrolPoint = 0; // ��Ʈ�� ������ �ִ� ��
    #endregion

    #region Principal Variable
    protected float ratioChaseSpeed; // �߰� �ӵ� ����
    protected bool isInRoom = false;

    private string monsterName = "1F_PatorlPrincipal";
    private string penaltyDialogue = "D_104_Principal_Start";
    #endregion

    #region Override Setting
    public override void AdditionalSetup()
    {
        maxPatrolPoint = patrolPoints.Length;
    }
    public override void IsInRoom(bool _isInRoom) { isInRoom = _isInRoom; }
    #endregion

    #region BehaviourState
    public override void IdleEnter() { StateAnimation(ChaseEntityStates.Idle, true); SeekNextRoute(); }
    public override void IdleExecute() { DetectPlayer(); Patrol(); }
    public override void IdleExit() { StateAnimation(ChaseEntityStates.Idle, false); }
    public override void TalkEnter() { StopPatrol(); StateAnimation(ChaseEntityStates.Talk, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { StateAnimation(ChaseEntityStates.Talk, false); }
    public override void QuietEnter() { StateAnimation(ChaseEntityStates.Quiet, true); SeekNextRoute(); }
    public override void QuietExecute() { Patrol(); }
    public override void QuietExit() { StateAnimation(ChaseEntityStates.Quiet, false); }
    public override void PenaltyEnter() { StateAnimation(ChaseEntityStates.Penalty, true); StopPatrol(); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { StateAnimation(ChaseEntityStates.Penalty, false); }
    public override void ChaseEnter() { StateAnimation(ChaseEntityStates.Chase, true); SetChase(true); }
    public override void ChaseExecute() { ChasePlayer(); }
    public override void ChaseExit() { StateAnimation(ChaseEntityStates.Chase, false); SetChase(false); }
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

    public void StopPatrol() { nav.ResetPath(); }

    #endregion

    #region Detect & Chase
    public void DetectPlayer()
    {
        if (!canDetectPlayer)
            return;
        Vector3 direction = eyeTransform.position - playerTransform.position;
        if (direction.magnitude > detectDistance)
            return;
        else
        {
            RaycastHit rayHit;
            if (Physics.Raycast(eyeTransform.position, direction, out rayHit, detectDistance, playerLayer))
            {
                IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastChase(this.name);
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && isChasePlayer)
        {
            StartCoroutine(CoolDownTimer());
            IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastPenalty(this.name);
            // YarnScript 발동
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.conversationManager.SetTalkerName(monsterName);
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.dialogueRunner.StartDialogue(penaltyDialogue);
            // 플레이어가 Principal 바라보도록
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.CameraEnforceLookAt(this.transform);
        }
    }

    public IEnumerator CoolDownTimer()
    {
        canDetectPlayer = false;
        yield return new WaitForSeconds(coolDownTimer);
        canDetectPlayer = true;
    }
    public void SetChase(bool _isChasePlayer)
    {
        isChasePlayer = _isChasePlayer;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.IsChasePlayer = isChasePlayer;
    }
    public override void ChasePlayer()
    {
        if (isInRoom)
        {
            anim.SetFloat("WALKVAL", 0f);
            nav.ResetPath();
        }
        else
        {
            anim.SetFloat("WALKVAL", 1f);
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
