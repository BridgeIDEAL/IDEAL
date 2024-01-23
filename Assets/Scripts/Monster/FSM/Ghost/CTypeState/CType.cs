using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CType : BaseEntity
{
    #region Component
    protected bool isWatch = false;
    protected Animator anim;
    protected State<CType>[] states;
    protected StateMachine<CType> stateMachine;
    public CTypeEntityStates CurrentType { private set; get; }
    #endregion

    #region Virtual
    public virtual void IndifferenceExecute() { } // 무관심 상태일때 실행
    public virtual void WatchEnter() { } // 경계 상태로 진입할 때 실행
    public virtual void WatchExecute() { if (!CanDetectPlayer()) ChangeState(CTypeEntityStates.Indifference); } // 경계 상태 유지
    public virtual void SetAnimation(CTypeEntityStates entityAnim) { }
    #endregion

    #region Override
    public override void Setup(MonsterData.MonsterStat stat)
    {
        // set information
        gameObject.name = stat.monsterName;
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        // add component
        AdditionalSetup();
        anim = GetComponentInChildren<Animator>();
        // set statemachine
        CurrentType = CTypeEntityStates.Indifference;
        states = new State<CType>[4];
        states[(int)CTypeEntityStates.Indifference] = new CTypeStates.Indifference();
        states[(int)CTypeEntityStates.Watch] = new CTypeStates.Watch();
        states[(int)CTypeEntityStates.Interaction] = new CTypeStates.Interaction();
        states[(int)CTypeEntityStates.Speechless] = new CTypeStates.Speechless();
        stateMachine = new StateMachine<CType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }

    public override void UpdateBehavior(){stateMachine.Execute();}
    public override void StartConversationInteraction() { ChangeState(CTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(CTypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(CTypeEntityStates.Speechless); }

    #endregion

    #region Method
    public void ChangeState(CTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    public void DetectPlayer()
    {
        if (isWatch)
            return;
        if (CanDetectPlayer())
            ChangeState(CTypeEntityStates.Watch);
    }
    public bool CanDetectPlayer()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude > sightDistance)
            return false;
        if (Physics.Raycast(transform.position, playerObject.transform.position, sightDistance, playerMask))
            return true;
        else
            return false;
    }

    public void StartTimer() { StartCoroutine("WatchTimer"); }
    public void EndTimer () { StopCoroutine("WatchTimer"); }
    public IEnumerator WatchTimer()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentType == CTypeEntityStates.Watch && !isWatch)
            isWatch = true;
        yield break;   
    }
    #endregion
}
