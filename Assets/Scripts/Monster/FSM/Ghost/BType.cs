using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    #region Component
    State<BType>[] states;
    StateMachine<BType> stateMachine;
    NavMeshAgent nav;
    //Animator anim;
    #endregion
   
    public BTypeEntityStates CurrentType { private set; get; }
    
    public override void Setup(MonsterData.MonsterStat stat)
    {
       // add component
        nav = GetComponent<NavMeshAgent>();
        //anim = GetComponentInChildren<Animator>();
        // set information
        CurrentType = BTypeEntityStates.Indifference;
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        nav.speed = stat.speed;
        gameObject.name = stat.name;
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
    public override void RestInteraction() { StartCoroutine("ResetPosition"); }
    public override void ConversationInteraction() { ChangeState(BTypeEntityStates.Interaction); }
    public override void SuccessInteraction() { ChangeState(BTypeEntityStates.Indifference); } 
    public override void FailInteraction() { ChangeState(BTypeEntityStates.Indifference); }
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
        // switch (entityAnim)
        // {
        //     case BTypeEntityStates.Indifference:
        //         anim.CrossFade("IDLE", 0.2f);
        //         break;
        //     case BTypeEntityStates.Interaction:
        //         anim.CrossFade("IDLE", 0.2f);
        //         break;
        //     case BTypeEntityStates.Aggressive:
        //         anim.CrossFade("ATTACK", 0.2f);
        //         break;
        //     case BTypeEntityStates.Chase:
        //         anim.CrossFade("CHASE", 0.2f);
        //         break;
        // }
    }
     IEnumerator ResetPosition()
    {
        ChangeState(BTypeEntityStates.Interaction);
        nav.isStopped = true;
        nav.ResetPath();
        yield return new WaitForSeconds(0.4f);
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        ChangeState(BTypeEntityStates.Indifference);
        nav.isStopped = false;
    }
}
