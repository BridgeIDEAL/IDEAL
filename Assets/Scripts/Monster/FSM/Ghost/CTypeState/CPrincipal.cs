using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CPrincipal : CType
{
    #region Variable
    private int patrolPoint = 0;
    private int patrolPointCnt = 0;
    [SerializeField] private Vector3[] patrolPositions;
    #endregion

    #region Component
    protected NavMeshAgent nav;
    #endregion

    #region StateBehavior
    public override void AdditionalSetup() { patrolPointCnt = patrolPositions.Length; nav = GetComponent<NavMeshAgent>(); }
    public override void IndifferenceEnter() { SetAnimation(CurrentType); StartPatrol(); }
    public override void IndifferenceExecute() { if (InSight()) ChangeState(CTypeEntityStates.Watch); }
    public override void IndifferenceExit() { StopPatrol(); }
    public override void WatchEnter() { SetAnimation(CurrentType); }
    public override void WatchExecute() { }
    public override void WatchExit() { }
    public override void InteractionEnter() { SetAnimation(CurrentType); }
    public override void InteractionExecute() { }
    public override void InteractionExit() { }
    public override void SpeechlessEnter() { SetAnimation(CurrentType); StartPatrol(); }
    public override void SpeechlessExecute() { }
    public override void SpeechlessExit() { StopPatrol(); }
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
        }
    }

    #endregion

    #region CoroutineMethod
    public void StartPatrol() { StartCoroutine("Patrol"); }
    public void StopPatrol() { StopCoroutine("Patrol"); }
    #endregion

    #region Coroutine
    private IEnumerator Patrol()
    {
        //if(nav.destination!=null)
        //{
        //    if (Vector3.Distance(nav.destination, transform.position) > 0.6f)
        //        patrolPoint = (patrolPoint + 1) % patrolPointCnt;
        //}
        //else
        //{
        //    nav.SetDestination(patrolPositions[patrolPoint]);
        //    if (Vector3.Distance(nav.destination, transform.position) > 0.6f)
        //        patrolPoint = (patrolPoint + 1) % patrolPointCnt;
        //}
        patrolPoint = (patrolPoint + 1) % patrolPointCnt;
        nav.SetDestination(patrolPositions[patrolPoint]);
        while (Vector3.Distance(nav.destination, transform.position) > 0.6f)
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
}
