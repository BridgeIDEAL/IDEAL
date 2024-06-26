using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Principal : ChaseEntity, IPatrol
{
    #region Variable
    [Header("이형체 이름, 대화")]
    [SerializeField] private string monsterName = "1F_PatorlPrincipal";
    [SerializeField] private string penaltyDialogue = "D_104_Principal_Start";

    [Header("순찰 변수")]
    [SerializeField] protected Vector3[] patrolPoints; 
    [SerializeField] protected float coolDownTimer = 30f; 
    protected bool canDetectPlayer = true; 
    protected int currentPatrolPoint = 0; 
    protected int maxPatrolPoint = 0; 
    protected float ratioChaseSpeed = 1.5f; // Anim Speed
    #endregion

    #region Override Setting
    public override void AdditionalSetup()
    {
        maxPatrolPoint = patrolPoints.Length;
    }
    #endregion

    #region BehaviourState
    public override void IdleEnter() { StateAnimation(EntityStateType.Idle, true); SeekNextRoute(); }
    public override void IdleExecute() { DetectPlayer(); Patrol(); }
    public override void IdleExit() { StateAnimation(EntityStateType.Idle, false); }
    public override void TalkEnter() { StopPatrol(); StateAnimation(EntityStateType.Talk, true); StartCoroutine(LookPlayerCor()); }
    public override void TalkExecute() { }
    public override void TalkExit() { StateAnimation(EntityStateType.Talk, false); }
    public override void QuietEnter() { StateAnimation(EntityStateType.Quiet, true); SeekNextRoute(); }
    public override void QuietExecute() { Patrol(); }
    public override void QuietExit() { StateAnimation(EntityStateType.Quiet, false); }
    public override void PenaltyEnter() { StateAnimation(EntityStateType.Penalty, true); StopPatrol(); }
    public override void PenaltyExecute() { }
    public override void PenaltyExit() { StateAnimation(EntityStateType.Penalty, false); }
    public override void ChaseEnter() { StateAnimation(EntityStateType.Chase, true); SetChase(true); }
    public override void ChaseExecute() { ChasePlayer(); }
    public override void ChaseExit() { StateAnimation(EntityStateType.Chase, false); SetChase(false); }
    #endregion

    #region Empty Method
    /// <summary>
    /// if not use this method => delete
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
        Vector3 direction = transform.position + eyeTransform - playerTransform.position;
        if (direction.magnitude > detectDistance)
            return;
        else
        {
            RaycastHit rayHit;
            if (Physics.Raycast(transform.position + eyeTransform, direction, out rayHit, detectDistance, playerLayer))
            {
                IdealSceneManager.Instance.CurrentGameManager.Entity_Manager.SendChaseMessage(this.name);
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && isChasePlayer)
        {
            StartCoroutine(CoolDownTimer());
            IdealSceneManager.Instance.CurrentGameManager.Entity_Manager.SendPenaltyMesage(this.name);
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
    
    public override void ChasePlayer()
    {
        if (gameEventManager.PlayerInPlace == PlaceTriggerType.StudyRoom_1F)
        {
            anim.SetFloat("WALKVAL", 0f);
            nav.ResetPath();
            // 문 앞까지 이동시켜야 함
        }
        else
        {
            anim.SetFloat("WALKVAL", 1f);
            base.ChasePlayer();
        }
    }
    #endregion

    #region Animation
    public override void StateAnimation(EntityStateType _entityState, bool _setBool)
    {
        //switch (_entityState)
        //{
        //    case EntityStateType.Idle:
        //        anim.SetBool("WALK", _setBool);
        //        break;
        //    case EntityStateType.Talk:
        //        anim.SetBool("IDLE", _setBool);
        //        break;
        //    case EntityStateType.Quiet:
        //        anim.SetBool("WALK", _setBool);
        //        break;
        //    case EntityStateType.Penalty:
        //        anim.SetBool("IDLE", _setBool);
        //        break;
        //    case EntityStateType.Chase:
        //        anim.SetBool("WALK", _setBool);
        //        break;
        //    default:
        //        break;
        //}
    }
    #endregion
}
