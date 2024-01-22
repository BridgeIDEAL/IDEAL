using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CType : BaseEntity
{
    #region Component
    protected float turnSpeed = 5f;
    protected bool onceWatch = false;
    protected Animator anim;
    protected State<CType>[] states;
    protected StateMachine<CType> stateMachine;

    public CTypeEntityStates CurrentType { private set; get; }
    #endregion

    #region Virtual
    public virtual void IndifferenceExecute() { } // 무관심 상태일때 실행
    public virtual void WatchEnter() { } // 경계 상태로 진입할 때 실행
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
    public override void InjureInteraction() { /*anim.CrossFade("Injure", 0.2f);*/ ChangeState(CTypeEntityStates.Indifference); }
    public override void SpeechlessInteraction() { ChangeState(CTypeEntityStates.Speechless); }
    public override void LookPlayer()
    {
        Vector3 dir = playerObject.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * turnSpeed);
    }
    #endregion



    #region Method
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
    public void EndTimer () { StopCoroutine("WatchTimer"); }
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
    #endregion
}
