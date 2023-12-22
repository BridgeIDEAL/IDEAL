using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    #region Component
    //Animator anim;
    State<BType>[] states;
    StateMachine<BType> stateMachine;
    NavMeshAgent nav;
    #endregion
   
    public BTypeEntityStates CurrentType { private set; get; }
    
    public override void Setup(MonsterData.MonsterStat stat)
    {
       // add component
        nav = GetComponent<NavMeshAgent>();
        //anim = GetComponentInChildren<Animator>();
        // set information
        gameObject.name = stat.monsterName;
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        nav.speed = stat.monsterSpeed; 
        // set statemachine
        CurrentType = BTypeEntityStates.Indifference;
        states = new State<BType>[5];
        states[(int)BTypeEntityStates.Indifference] = new BTypeStates.Indifference();
        states[(int)BTypeEntityStates.Interaction] = new BTypeStates.Interaction();
        states[(int)BTypeEntityStates.Aggressive] = new BTypeStates.Aggressive();
        states[(int)BTypeEntityStates.Chase] = new BTypeStates.Chase();
        states[(int)BTypeEntityStates.Speechless] = new BTypeStates.Speechless();
        stateMachine = new StateMachine<BType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }

    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void RestInteraction() { ChangeState(BTypeEntityStates.Indifference); StartCoroutine("ResetPosition"); }
    public override void StartConversationInteraction() { ChangeState(BTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(BTypeEntityStates.Indifference); }
    public override void ChaseInteraction() { ChangeState(BTypeEntityStates.Aggressive); }
    public override void SpeechlessInteraction() { ChangeState(BTypeEntityStates.Speechless); }
    public void ChangeState(BTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    
    public void ChasePlayer(){ nav.SetDestination(playerObject.transform.position); }

    public void SetAnimation(BTypeEntityStates entityAnim)
    {
        // switch (entityanim)
        // {
        //     case btypeentitystates.indifference:
        //         anim.crossfade("idle", 0.2f);
        //         break;
        //     case btypeentitystates.interaction:
        //         anim.crossfade("idle", 0.2f);
        //         break;
        //     case btypeentitystates.aggressive:
        //         anim.crossfade("attack", 0.2f);
        //         break;
        //     case btypeentitystates.chase:
        //         anim.crossfade("chase", 0.2f);
        //         break;
        // }
    }

    IEnumerator ResetPosition()
    {
        nav.isStopped = true;
        nav.ResetPath();
        yield return new WaitForSeconds(0.4f);
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        nav.isStopped = false;
    }
}
