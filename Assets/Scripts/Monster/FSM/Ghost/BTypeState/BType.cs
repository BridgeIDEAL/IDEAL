using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    #region Component
    protected Animator anim;
    protected NavMeshAgent nav;
    protected State<BType>[] states;
    protected StateMachine<BType> stateMachine;
    public BTypeEntityStates CurrentType { private set; get; }
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
        anim = GetComponent<Animator>();
        this.gameObject.AddComponent<NavMeshAgent>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = stat.monsterSpeed;
        nav.radius = 0.4f;
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
    public override void StartConversationInteraction() { ChangeState(BTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(BTypeEntityStates.Indifference);}
    public override void ChaseInteraction() { ChangeState(BTypeEntityStates.Aggressive); }
    public override void SpeechlessInteraction() { ChangeState(BTypeEntityStates.Speechless); }
    #endregion

    #region Method
    public void ChangeState(BTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void ChasePlayer(){ nav.SetDestination(playerObject.transform.position); }
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

    public void SetAnimation(BTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case BTypeEntityStates.Indifference:
                anim.CrossFade("Idle", 0.2f);
                break;
            case BTypeEntityStates.Speechless:
                anim.CrossFade("Idle", 0.2f);
                break;
            case BTypeEntityStates.Interaction:
                anim.CrossFade("Idle", 0.2f);
                break;
            case BTypeEntityStates.Aggressive:
                anim.CrossFade("Aggressive",0.2f);
                break;
            case BTypeEntityStates.Chase:
                anim.CrossFade("Walk", 0.2f);
                break;
        }
    } 
    #endregion
}
