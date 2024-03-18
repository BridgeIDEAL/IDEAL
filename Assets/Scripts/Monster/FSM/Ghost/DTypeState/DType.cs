using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DType : BaseEntity
{
    #region Variable
    protected bool isWatch = false;
    protected bool isChasePlayer = false;
    [SerializeField] protected Vector3 InitVec { get; set; }
    [SerializeField] protected float chaseSpeed;
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
    public virtual void WatchEnter() { SetAnimation(CurrentType); StartWatchTimer(); }
    public virtual void WatchExecute() { if (!CanDetectPlayer()) ChangeState(DTypeEntityStates.Indifference); }
    public virtual void WatchExit() { EndWatchTimer(); }
    public virtual void InteractionEnter() { SetAnimation(CurrentType); }
    public virtual void InteractionExecute() { }
    public virtual void InteractionExit() { }
    public virtual void AggressiveEnter() { SetAnimation(CurrentType); }
    public virtual void AggressiveExecute() { }
    public virtual void AggressiveExit() { }
    public virtual void ChaseEnter() { SetAnimation(CurrentType); }
    public virtual void ChaseExecute() { ChasePlayer(); Debug.Log("추격중!"); }
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

    public void DetectPlayer()
    {
        if (isWatch)
            return;
        if (CanDetectPlayer()) ChangeState(DTypeEntityStates.Watch);
    }

    public bool CanDetectPlayer()
    {
        Vector3 interV = player.transform.position - transform.position;
        if (interV.magnitude > sightDistance) return false;
        else return true;
    }

    public void StartWatchTimer() { StartCoroutine("WatchTimer"); }
    public void EndWatchTimer() { StopCoroutine("WatchTimer"); }
    public IEnumerator WatchTimer()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentType == DTypeEntityStates.Watch && !isWatch)
            isWatch = true;
        yield break;
    }
    public void SetReposition() { StartCoroutine("ResetPosition"); }
    public IEnumerator ResetPosition()
    {
        nav.ResetPath();
        nav.enabled = false;
        yield return new WaitForSeconds(0.4f);
        transform.position = InitVec;
        nav.enabled = true; 
    }
    #endregion

    #region Animation
    public virtual void SetAnimation(DTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case DTypeEntityStates.Indifference:
                anim.SetBool("WALK", false);
                break;
            case DTypeEntityStates.Watch:
                anim.SetBool("WALK", false);
                break;
            case DTypeEntityStates.Interaction:
                anim.SetBool("WALK", false);
                break;
            case DTypeEntityStates.Aggressive:
                anim.SetBool("WALK", true);
                break;
            case DTypeEntityStates.Chase:
                anim.SetBool("WALK", true);
                break;
            case DTypeEntityStates.Speechless:
                anim.SetBool("WALK", false);
                break;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            ChangeState(DTypeEntityStates.Chase);
    }
    #endregion
}
