using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DType : BaseEntity
{
    #region Component
    State<DType>[] states;
    StateMachine<DType> stateMachine;
    NavMeshAgent nav;
    Animator anim;
    bool onceWatch = false;
    #endregion

    public DTypeEntityStates CurrentType { private set; get; }

    public override void Setup(MonsterData.MonsterStat stat)
    {   // add component
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        // set information
        gameObject.name = stat.monsterName;
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        nav.speed = stat.monsterSpeed;
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
    public override void InjureInteraction() { /*anim.CrossFade("Injure", 0.2f);*/ ChangeState(DTypeEntityStates.Indifference); }
    public override void ChaseInteraction() { ChangeState(DTypeEntityStates.Aggressive); }
    public override void SpeechlessInteraction() { ChangeState(DTypeEntityStates.Speechless); }
    public void ChangeState(DTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void ChasePlayer(){ nav.SetDestination(playerObject.transform.position);}

    public void CheckNearPlayer()
    {
        if (onceWatch)
            return;
        if (CheckDistance())
            ChangeState(DTypeEntityStates.Watch);
    }

    public void MaintainWatch()
    {
        if (!CheckDistance())
            ChangeState(DTypeEntityStates.Indifference);
    }

    public void StartTimer() { StartCoroutine("WatchTimer"); }
    public void EndTimer() { StopCoroutine("WatchTimer"); }

    public IEnumerator WatchTimer()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentType == DTypeEntityStates.Watch && !onceWatch)
        {
            onceWatch = true;
            InjureInteraction();
        }
        yield break;
    }

    public void SetReposition() { StartCoroutine("ResetPosition"); }
    public IEnumerator ResetPosition()
    {
        nav.isStopped = true;
        nav.ResetPath();
        yield return new WaitForSeconds(0.4f);
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        nav.isStopped = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerObject)
            Debug.Log("Player Dead!");
    }

    public void SetAnimation(DTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case DTypeEntityStates.Indifference:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case DTypeEntityStates.Watch:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case DTypeEntityStates.Interaction:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case DTypeEntityStates.Aggressive:
                anim.CrossFade("ATTACK", 0.2f);
                break;
            case DTypeEntityStates.Chase:
                anim.CrossFade("CHASE", 0.2f);
                break;
        }
    }
}
