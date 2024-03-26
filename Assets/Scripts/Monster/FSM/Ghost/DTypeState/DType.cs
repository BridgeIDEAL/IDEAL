using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DType : BaseEntity
{
    #region Variable
    public bool IsWatchPlayer { get; set; } = false;
    public bool IsChasePlayer { get; set; } = false;
    [SerializeField] protected Vector3 initPosition;
    [SerializeField] protected Vector3 initRotation;
    [SerializeField] protected float sightDistance;
    #endregion

    #region Component
    protected Animator anim;
    protected NavMeshAgent nav;
    protected State<DType>[] states;
    protected StateMachine<DType> stateMachine;
    public DTypeEntityStates CurrentType { private set; get; }
    #endregion

    #region Virtual
    public virtual void IndifferenceEnter() { SetAnimation(CurrentType); }
    public virtual void IndifferenceExecute() { }
    public virtual void IndifferenceExit() { }
    public virtual void WatchEnter() { SetAnimation(CurrentType); }
    public virtual void WatchExecute() { }
    public virtual void WatchExit() {  }
    public virtual void InteractionEnter() { SetAnimation(CurrentType); }
    public virtual void InteractionExecute() { }
    public virtual void InteractionExit() { }
    public virtual void AggressiveEnter() { SetAnimation(CurrentType); }
    public virtual void AggressiveExecute() { }
    public virtual void AggressiveExit() { }
    public virtual void ChaseEnter() { SetAnimation(CurrentType); }
    public virtual void ChaseExecute() { ChasePlayer();}
    public virtual void ChaseExit() { }
    public virtual void SpeechlessEnter() { SetAnimation(CurrentType); }
    public virtual void SpeechlessExecute() { }
    public virtual void SpeechlessExit() { }
    #endregion

    #region Override
    public override void Setup()
    {
        // set initVariable
        base.Setup();
        AdditionalSetup();

        // set component
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        // set statemachine
        CurrentType = DTypeEntityStates.Indifference;
        states = new State<DType>[6];
        states[(int)DTypeEntityStates.Indifference] = new DTypeStates.Indifference();
        states[(int)DTypeEntityStates.Interaction] = new DTypeStates.Interaction();
        states[(int)DTypeEntityStates.Watch] = new DTypeStates.Watch();
        states[(int)DTypeEntityStates.Aggressive] = new DTypeStates.Aggressive();
        states[(int)DTypeEntityStates.Chase] = new DTypeStates.Chase();
        states[(int)DTypeEntityStates.Speechless] = new DTypeStates.Speechless();
        stateMachine = new StateMachine<DType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }
    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void StartConversationInteraction() { ChangeState(DTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(DTypeEntityStates.Indifference); }
    public override void ChaseInteraction() { ChangeState(DTypeEntityStates.Aggressive); }
    public override void SpeechlessInteraction() { ChangeState(DTypeEntityStates.Speechless); }
    #endregion

    #region Method
    public void ChangeState(DTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    public void ChasePlayer()
    { 
        nav.SetDestination(player.transform.position);
        if (nav.hasPath)
        {
            Vector3 dir = (nav.steeringTarget - transform.position).normalized;
            Vector3 animDir = transform.InverseTransformDirection(dir);
            bool isFacingMoveDir = Vector3.Dot(dir, transform.forward) > 0.5f;
            anim.SetFloat("VX", isFacingMoveDir ? animDir.x : 0, 0.5f, Time.deltaTime);
            anim.SetFloat("VZ", isFacingMoveDir ? animDir.z : 0, 0.5f, Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 180 * Time.deltaTime);
            if (Vector3.Distance(transform.position,nav.destination)< nav.radius){
                Debug.Log("게임 오버!");
            }
        }
        else
        {
            anim.SetFloat("VX",  0, 0.25f, Time.deltaTime);
            anim.SetFloat("VZ", 0, 0.25f, Time.deltaTime);
        }
    }
    public bool InSight()
    {
        Vector3 interV = player.transform.position - transform.position;
        if (interV.magnitude > sightDistance)
            return false;
        RaycastHit rayCastHit;
        Vector3 direction = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, direction, out rayCastHit))
        {
            if (rayCastHit.collider.CompareTag("Player"))
                return true;
            else
                return false;
        }
        return false;
    }
    #endregion

    #region Coroutine
    public IEnumerator WatchTimerCor()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentType == DTypeEntityStates.Watch)
            ChangeState(DTypeEntityStates.Indifference);
        Debug.Log("경고 : 경계 상태 10초 초과!!");
        yield break;
    }
    public IEnumerator ReturnPositionCor()
    {
        nav.ResetPath();
        nav.isStopped = true;
        yield return new WaitForSeconds(0.4f);
        transform.position = initPosition;
        transform.rotation = Quaternion.Euler(initRotation.x, initRotation.y, initRotation.z);
        IsChasePlayer = false;
        nav.isStopped = false;
    }
    #endregion

    #region Animation
    public virtual void SetAnimation(DTypeEntityStates entityAnim)
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
}
