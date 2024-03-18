using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPatrolType : DType
{
    #region Component
    private int patrolPoint = 0;
    [SerializeField] private Vector3[] patrolPositions;
    #endregion

    #region Virtual
    public override void IndifferenceEnter() { SetAnimation(CurrentType); }
    public override void IndifferenceExecute() { }
    public override void IndifferenceExit() { }
    public override void WatchEnter() { SetAnimation(CurrentType); StartWatchTimer(); }
    public override void WatchExecute() { if (!CanDetectPlayer()) ChangeState(DTypeEntityStates.Indifference); }
    public override void WatchExit() { EndWatchTimer(); }
    public override void InteractionEnter() { SetAnimation(CurrentType); }
    public override void InteractionExecute() { }
    public override void InteractionExit() { }
    public override void AggressiveEnter() { SetAnimation(CurrentType); }
    public override void AggressiveExecute() { }
    public override void AggressiveExit() { }
    public override void ChaseEnter() { SetAnimation(CurrentType); }
    public override void ChaseExecute() { }
    public override void ChaseExit() { }
    public override void SpeechlessEnter() { SetAnimation(CurrentType); }
    public override void SpeechlessExecute() { }
    public override void SpeechlessExit() { }
    #endregion

    #region Animation
    public override void SetAnimation(DTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case DTypeEntityStates.Indifference:
                break;
            case DTypeEntityStates.Watch:
                break;
            case DTypeEntityStates.Interaction:
                break;
            case DTypeEntityStates.Aggressive:
                break;
            case DTypeEntityStates.Chase:
                break;
            case DTypeEntityStates.Speechless:
                break;
        }
    }

    #endregion

    #region Method
    public void Patrol()
    {

    }

    #endregion
}
