using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CType : BaseEntity
{
    State<CType>[] states;
    StateMachine<CType> stateMachine;
    NavMeshAgent nav;
    public CTypeEntityStates CurrentType { private set; get; }
    public override void Setup(MonsterData.MonsterStat stat)
    {
        //base.Setup();
        nav.speed = stat.speed;
        gameObject.name = stat.name;
        CurrentType = CTypeEntityStates.Indifference;
        states = new State<CType>[3];
        states[(int)CTypeEntityStates.Indifference] = new CTypeStates.Indifference();
        states[(int)CTypeEntityStates.Watch] = new CTypeStates.Watch();
        states[(int)CTypeEntityStates.Interaction] = new CTypeStates.Interaction();
        stateMachine = new StateMachine<CType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
        nav = GetComponent<NavMeshAgent>();
    }

    public override void UpdateBehavior(){stateMachine.Execute();}
    public override void RestInteraction()
    {
        //transform.position = InitTransform.position;
        //nav.SetDestination(InitTransform.position);
        ChangeState(CTypeEntityStates.Indifference);
    }
    public override void StartInteraction() { ChangeState(CTypeEntityStates.Interaction); }
    public override void SuccessInteraction() { ChangeState(CTypeEntityStates.Indifference); }
    public override void FailInteraction() { ChangeState(CTypeEntityStates.Indifference); Debug.Log("경계시간 초과 페널티 부과!"); }
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

    public void StartTimer() { StartCoroutine("WatchTimer"); }
    IEnumerator WatchTimer()
    {
        yield return new WaitForSeconds(10f);
        if (CanInteraction)
        {
            GameManager.EntityEvent.SendMessage(EventType.FailInteraction, this.gameObject);
            ChangeState(CTypeEntityStates.Indifference);
        }
        yield break;   
    }
}
