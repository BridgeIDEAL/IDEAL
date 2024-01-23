using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CPatrolAction : CType
{
    #region Component
    protected int curPoint = 0;
    protected int maxPoint = 0;
    protected float patrolSpeed = 2f;
    protected NavMeshAgent nav;
    [SerializeField] protected List<Vector3> patrolPoints;
    #endregion

    #region Override
    public override void AdditionalSetup()
    {
        this.gameObject.AddComponent<NavMeshAgent>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = patrolSpeed;
        nav.radius = 0.4f;
        patrolPoints = new List<Vector3>();
        patrolPoints.Add(new Vector3(13, 0, -5));
        patrolPoints.Add(new Vector3(13, 0, -83));
        maxPoint = patrolPoints.Count;
        nav.SetDestination(patrolPoints[curPoint]);
    }

    public override void IndifferenceExecute()
    {
        if (nav.destination == null) nav.destination = patrolPoints[curPoint];
        if(nav.remainingDistance < 0.1f && !nav.pathPending)
        {
            nav.destination = patrolPoints[curPoint];
            curPoint = (curPoint + 1) % maxPoint;
        }
    }

    public override void WatchEnter() { nav.ResetPath(); }
    public override void LookOriginal() { nav.isStopped = false; }
    public override void SetAnimation(CTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case CTypeEntityStates.Indifference:
                anim.CrossFade("Walk", 0.2f);
                break;
            case CTypeEntityStates.Watch:
                anim.CrossFade("Idle", 0.2f);
                break;
            case CTypeEntityStates.Interaction:
                anim.CrossFade("Idle", 0.2f);
                break;
            case CTypeEntityStates.Speechless:
                anim.CrossFade("Walk", 0.2f);
                break;
        }
    }
    #endregion
}
