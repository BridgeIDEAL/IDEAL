using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CType : BaseEntity
{
    #region Component
    State<CType>[] states;
    StateMachine<CType> stateMachine;
    NavMeshAgent nav;
    Animator anim;
    #endregion

    public CTypeEntityStates CurrentType { private set; get; }

    public override void Setup(MonsterData.MonsterStat stat)
    {
        // add component
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        // set information
        CurrentType = CTypeEntityStates.Indifference;
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        nav.speed = stat.speed;
        gameObject.name = stat.name;
        // set statemachine
        states = new State<CType>[3];
        states[(int)CTypeEntityStates.Indifference] = new CTypeStates.Indifference();
        states[(int)CTypeEntityStates.Watch] = new CTypeStates.Watch();
        states[(int)CTypeEntityStates.Interaction] = new CTypeStates.Interaction();
        stateMachine = new StateMachine<CType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
        nav = GetComponent<NavMeshAgent>();
    }
    public override void UpdateBehavior(){stateMachine.Execute();}
    public override void RestInteraction(){ StartCoroutine("ResetPosition");}
    public override void ConversationInteraction() { ChangeState(CTypeEntityStates.Interaction); }
    public override void SuccessInteraction() { ChangeState(CTypeEntityStates.Indifference); }
    public override void FailInteraction() { ChangeState(CTypeEntityStates.Indifference); }
    public void ChangeState(CTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void WatchPlayer()
    {
        float dist = (playerObject.transform.position - transform.position).magnitude;
        Vector3 dir = playerObject.transform.position - transform.position;
        if (sightDistance >= dist)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), lookSpeed * Time.deltaTime);
        else
            ChangeState(CTypeEntityStates.Indifference);
    }

    public void SetAnimation(CTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case CTypeEntityStates.Indifference:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case CTypeEntityStates.Watch:
                anim.CrossFade("IDLE", 0.2f);
                break;
            case CTypeEntityStates.Interaction:
                anim.CrossFade("IDLE", 0.2f);
                break;
        }
    }

    public void StartTimer() { StartCoroutine("WatchTimer"); }

    IEnumerator WatchTimer()
    {
        yield return new WaitForSeconds(10f);
        if (GameManager.EntityEvent.CanInteraction)
        {
            GameManager.EntityEvent.SendMessage(EventType.FailInteraction, this.gameObject);
            ChangeState(CTypeEntityStates.Indifference);
        }
        yield break;   
    }
    
      IEnumerator ResetPosition()
    {
        ChangeState(CTypeEntityStates.Indifference);
        nav.isStopped = true;
        nav.ResetPath();
        yield return new WaitForSeconds(0.4f);
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        ChangeState(CTypeEntityStates.Indifference);
        nav.isStopped = false;
    }

}
