using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    #region Component
    protected float chaseSpeed;
    protected Animator anim;
    protected NavMeshAgent nav;
    protected State<BType>[] states;
    protected StateMachine<BType> stateMachine;
    [SerializeField] protected Quaternion towardRotation;
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
        chaseSpeed = stat.monsterSpeed;
        // add component
        AdditionalSetup();
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

    public override void AdditionalSetup()
    {
        anim = GetComponent<Animator>();
        this.gameObject.AddComponent<NavMeshAgent>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = chaseSpeed;
        nav.radius = 0.4f;
        towardRotation = Quaternion.Euler(initRotation.x, initRotation.y, initRotation.z);
    }

    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void StartConversationInteraction() { ChangeState(BTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(BTypeEntityStates.Indifference);}
    public override void ChaseInteraction() { ChangeState(BTypeEntityStates.Aggressive); }
    public override void SpeechlessInteraction() { ChangeState(BTypeEntityStates.Speechless); }

    public override void LookPlayer()
    {
        if (isLookPlayer)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerObject.transform.position - transform.position);
            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            float step = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
            if (angle < marginalAngle)
                isLookPlayer = false;
        }
    }

    public override void LookOriginal()
    {
        if (isLookOriginal)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, towardRotation, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, towardRotation) < marginalAngle)
                isLookOriginal = false;
        }
    }
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
        nav.ResetPath();
        nav.isStopped = true;
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
