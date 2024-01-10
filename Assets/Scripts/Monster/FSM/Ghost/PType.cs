using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PType : BaseEntity
{
    #region Component
    State<PType>[] states;
    StateMachine<PType> stateMachine;
    NavMeshAgent nav;
    Animator anim;
    bool onceWatch = false;
    private List<Vector3> wayPoints = new List<Vector3>(); 
    private int currentWayPointIndex = 0;
    #endregion

    public CTypeEntityStates CurrentType { private set; get; }

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
        wayPoints.Add(new Vector3(-10, 0, 0));
        wayPoints.Add(new Vector3(-20, 0, 0));
        // set statemachine
        CurrentType = CTypeEntityStates.Indifference;
        states = new State<PType>[4];
        states[(int)CTypeEntityStates.Indifference] = new PTypeStates.Indifference();
        states[(int)CTypeEntityStates.Interaction] = new PTypeStates.Interaction();
        states[(int)CTypeEntityStates.Watch] = new PTypeStates.Watch();
        states[(int)CTypeEntityStates.Speechless] = new PTypeStates.Speechless();
        stateMachine = new StateMachine<PType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }

    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void StartConversationInteraction() { ChangeState(CTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(CTypeEntityStates.Indifference); }
    public override void InjureInteraction() { /*anim.CrossFade("Injure", 0.2f);*/ ChangeState(CTypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(CTypeEntityStates.Speechless); }

    public void StartPatrol()
    {
        nav.SetDestination(wayPoints[currentWayPointIndex]);
    }

    public void CheckNextPoint()
    {
        if (nav.remainingDistance < 0.5f)
            SetNextPoint();
    }

    public void SetNextPoint()
    {
        currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Count;
        nav.SetDestination(wayPoints[currentWayPointIndex]);
    }

    public void ChangeState(CTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void CheckNearPlayer()
    {
        if (onceWatch)
            return;
        if (CheckDistance())
            ChangeState(CTypeEntityStates.Watch);
    }

    public void MaintainWatch()
    {
        if (!CheckDistance())
            ChangeState(CTypeEntityStates.Indifference);
    }

    public void StartTimer() { StartCoroutine("WatchTimer"); }
    public void EndTimer() { StopCoroutine("WatchTimer"); }

    public IEnumerator WatchTimer()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentType == CTypeEntityStates.Watch && !onceWatch)
        {
            onceWatch = true;
            InjureInteraction();
        }
        yield break;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerObject)
            Debug.Log("Player Dead!");
    }

    public void SetAnimation(CTypeEntityStates entityAnim)
    {
        //switch (entityAnim)
        //{
        //    case DTypeEntityStates.Indifference:
        //        anim.CrossFade("IDLE", 0.2f);
        //        break;
        //    case DTypeEntityStates.Watch:
        //        anim.CrossFade("IDLE", 0.2f);
        //        break;
        //    case DTypeEntityStates.Interaction:
        //        anim.CrossFade("IDLE", 0.2f);
        //        break;
        //    case DTypeEntityStates.Aggressive:
        //        anim.CrossFade("ATTACK", 0.2f);
        //        break;
        //    case DTypeEntityStates.Chase:
        //        anim.CrossFade("CHASE", 0.2f);
        //        break;
        //}
    }
}
