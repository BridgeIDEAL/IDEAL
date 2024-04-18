using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;

public class CGuard : CType
{
    #region Variable
    private int patrolPoint = 0;
    private int patrolPointCnt = 0;
    [SerializeField] private Vector3[] patrolPositions;
    [SerializeField] private Vector3 guardRoomEntrancePosition;
    [SerializeField] private Vector3 guardRoomEntranceRotation;
    public bool IsInGuardRoom {get; private set;} = true;
    #endregion

    #region Component
    protected NavMeshAgent nav;
    #endregion

    #region StateBehavior
    public override void AdditionalSetup() { patrolPointCnt = patrolPositions.Length; nav = GetComponent<NavMeshAgent>(); }
    public override void IndifferenceEnter() { SetAnimation(CurrentType); StartPatrol(); }
    public override void IndifferenceExecute() { /*if (InSight()) ChangeState(CTypeEntityStates.Watch*/ FindPlayerInGuardRoom();}
    public override void IndifferenceExit() { StopPatrol(); }
    public override void WatchEnter() { SetAnimation(CurrentType); WatchPlayer(); }
    public override void WatchExecute() { }
    public override void WatchExit() { LookFront() ; }
    public override void InteractionEnter() { SetAnimation(CurrentType); }
    public override void InteractionExecute() { }
    public override void InteractionExit() { }
    public override void SpeechlessEnter() { SetAnimation(CurrentType); StartPatrol(); }
    public override void SpeechlessExecute() { }
    public override void SpeechlessExit() { StopPatrol(); }
    public override void PenaltyEnter() { SetAnimation(CurrentType); MoveGuardRoomAtOnce();}
    public override void PenaltyExecute() { ImmediateKill();}
    public override void PenaltyExit() { StopPatrol(); }
    #endregion

    

    #region CoroutineMethod
    public void StartPatrol() { StartCoroutine("Patrol"); }
    public void StopPatrol() { StopCoroutine("Patrol"); }
    #endregion

    #region Coroutine
    private IEnumerator Patrol()
    {
        patrolPoint = (patrolPoint + 1) % patrolPointCnt;
        nav.SetDestination(patrolPositions[patrolPoint]);
        while (Vector3.Distance(nav.destination, transform.position) > nav.stoppingDistance)
        {
            if (IsWatchPlayer) yield break;
            if (nav.hasPath)
            {
                Vector3 dir = (nav.steeringTarget - transform.position).normalized;
                Vector3 animDir = transform.InverseTransformDirection(dir);
                bool isFacingMoveDir = Vector3.Dot(dir, transform.forward) > 0.5f;
                anim.SetFloat("VX", (isFacingMoveDir) ? animDir.x : 0, 0.5f, Time.deltaTime);
                anim.SetFloat("VZ", (isFacingMoveDir) ? animDir.z : 0, 0.5f, Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 180 * Time.deltaTime);
            }
            else
            {
                anim.SetFloat("VX", 0, 0.25f, Time.deltaTime);
                anim.SetFloat("VZ", 0, 0.25f, Time.deltaTime);
            }

            yield return null;
        }
        anim.SetFloat("VX", 0, 0.25f, Time.deltaTime);
        anim.SetFloat("VZ", 0, 0.25f, Time.deltaTime);
        StartCoroutine("Patrol");
    }
    #endregion

    #region Method
    /// <summary>
    /// 플레이어가 수위 방으로 들어간 것을 확인하는 함수
    /// </summary>
    public void FindPlayerInGuardRoom(){
        if(IsInGuardRoom){
            if(IsNearPlayer())
                ChangeState(CTypeEntityStates.Penalty);
            else
                return;
        }
    } 

    public void MoveGuardRoomAtOnce(){
        nav.ResetPath();
        nav.enabled =false;
        transform.position = guardRoomEntrancePosition;
        Quaternion targetRoattion = Quaternion.Euler(guardRoomEntranceRotation.x,guardRoomEntranceRotation.y,guardRoomEntranceRotation.z);
        transform.rotation = targetRoattion;
        nav.enabled = true;
    }

    /// <summary>
    /// 플레이어가 경비를 보게 되면 즉사시킴
    /// </summary>
    public void ImmediateKill(){
        Vector3 dir = transform.position - player.transform.position;
        float angle = Vector3.Angle(player.transform.forward, dir);
        
    }
    #endregion

    #region Animation
    public override void SetAnimation(CTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case CTypeEntityStates.Indifference:
                anim.SetBool("WALK", true);
                break;
            case CTypeEntityStates.Watch:
                anim.SetBool("WALK", false);
                break;
            case CTypeEntityStates.Interaction:
                anim.SetBool("WALK", false);
                break;
            case CTypeEntityStates.Speechless:
                anim.SetBool("WALK", true);
                break;
            case CTypeEntityStates.Penalty:
                anim.SetBool("WALK", false);
                break;
        }
    }

    #endregion
}
