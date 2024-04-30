using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal : ChaseEntity, IPatrol
{
    #region BehaviourState
    public override void IdleEnter() { StateAnimation(currentState, true); }
    public override void IdleExecute() { StartPatrol(); DetectPlayer(); }
    public override void IdleExit() { StateAnimation(currentState, false); StopPatrol(); }
    public override void TalkEnter() { StateAnimation(currentState, true); }
    public override void TalkExecute() { }
    public override void TalkExit() { StateAnimation(currentState, false); }
    public override void QuietEnter() { StateAnimation(currentState, true); }
    public override void QuietExecute() { StartPatrol(); }
    public override void QuietExit() { StateAnimation(currentState, false); StopPatrol(); }
    public override void PenaltyEnter() { StateAnimation(currentState, true); }
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

    #region Method
    public void StartPatrol()
    {
        if (nav.destination == null)
        {
            currentPatrolPoint = (currentPatrolPoint + 1) % maxPatrolPoint;
            MoveTo(patrolPoints[currentPatrolPoint]);
            return;
        }
        if (nav.remainingDistance > 0.5f)
            return;
        currentPatrolPoint = (currentPatrolPoint + 1) % maxPatrolPoint;
        MoveTo(patrolPoints[currentPatrolPoint]);
    }

    public void StopPatrol()
    {
        nav.ResetPath();
    }

    public void DetectPlayer()
    {
        Vector3 direction = transform.position - player.transform.position;
        if (direction.magnitude > detectDistance)
            return;
        else
        {
            RaycastHit rayHit;
            if (Physics.Raycast(Vector3.up, direction, out rayHit, missDistance, player.gameObject.layer))
            {
                if (rayHit.collider.CompareTag("Player"))
                {
                    ChangeState(ChaseEntityStates.Chase);   
                }
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") && IsChasePlayer)
        {
            ChangeState(ChaseEntityStates.Talk);
            Debug.Log("벌점주는중!");
        }
    }
    #endregion
}
