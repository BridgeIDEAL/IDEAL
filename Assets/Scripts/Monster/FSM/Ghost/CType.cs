using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CType : BaseEntity
{
    #region Component
    State<CType>[] states;
    StateMachine<CType> stateMachine;
    //Animator anim;
    bool onceWatch = false;
    #endregion

    public CTypeEntityStates CurrentType { private set; get; }

    public override void Setup(MonsterData.MonsterStat stat)
    {
        // add component
        //anim = GetComponentInChildren<Animator>();
        // set information
        gameObject.name = stat.monsterName;
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
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
    public void SetAnimation(CTypeEntityStates entityAnim)
    {
        //switch (entityAnim)
        //{
        //    case CTypeEntityStates.Indifference:
        //        anim.CrossFade("IDLE", 0.2f);
        //        break;
        //    case CTypeEntityStates.Watch:
        //        anim.CrossFade("IDLE", 0.2f);
        //        break;
        //    case CTypeEntityStates.Interaction:
        //        anim.CrossFade("IDLE", 0.2f);
        //        break;
        //}
    }
}
