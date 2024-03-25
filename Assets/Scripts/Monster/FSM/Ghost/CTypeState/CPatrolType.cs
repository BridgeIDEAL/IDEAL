using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CPatrolType : CType
{
    bool rotate = false;
    #region Variable
    private int patrolPoint = 0;
    private int patrolPointCnt = 0;
    [SerializeField] private Vector3[] patrolPositions;
    #endregion

    #region Component
    protected NavMeshAgent nav;
    #endregion

    #region StateBehavior
    public override void AdditionalSetUp() { patrolPointCnt = patrolPositions.Length; nav = GetComponent<NavMeshAgent>(); }
    public override void IndifferenceEnter() { SetAnimation(CurrentType); StartPatrol(); }
    public override void IndifferenceExecute() { }
    public override void IndifferenceExit() { }
    public override void WatchEnter() { SetAnimation(CurrentType); StartWatchTimer(); }
    public override void WatchExecute() { if (!CanDetectPlayer()) ChangeState(CTypeEntityStates.Indifference); }
    public override void WatchExit() { EndWatchTimer(); }
    public override void InteractionEnter() { SetAnimation(CurrentType); }
    public override void InteractionExecute() { }
    public override void InteractionExit() { }
    public override void SpeechlessEnter() { SetAnimation(CurrentType); }
    public override void SpeechlessExecute() { }
    public override void SpeechlessExit() { }
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

    #region Coroutine
    public void StartPatrol()
    {
        //    rotate = false;
        //    anim.SetBool("TURN", false);
        //    anim.SetBool("WALK", true);
            StartCoroutine("Patrol"); 
        //
    }
    public void StopPatrol() { StopCoroutine("Patrol"); }
    private IEnumerator Patrol()
    {
        patrolPoint = (patrolPoint + 1) % patrolPointCnt;
        nav.SetDestination(patrolPositions[patrolPoint]);
        //Debug.Log(patrolPositions[patrolPoint]);
        while (Vector3.Distance(nav.destination, transform.position) > 0.6f)
        {
            if (isWatch) yield break;
            //Debug.Log(Vector3.Distance(nav.destination, transform.position));

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
        //anim.SetBool("WALK", false);
        //anim.SetBool("TURN", true);
        //rotate = true;
        //StartCoroutine("Rotate");
    }

    IEnumerator Rotate()
    {
        while (rotate)
        {
            Vector3 rotation = anim.transform.eulerAngles;
            transform.forward = Quaternion.Euler(0, rotation.y, 0) * Vector3.forward;
            yield return null;
        }
    }  
    #endregion

    #region Method
    
    #endregion
}
