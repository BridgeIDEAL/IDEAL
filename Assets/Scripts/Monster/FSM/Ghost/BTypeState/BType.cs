using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BType : BaseEntity
{
    #region Component
    protected bool check = false;
    protected float stopangle = 3f;
    protected float turnSpeed = 2f;
    protected Animator anim;
    protected NavMeshAgent nav;
    protected State<BType>[] states;
    protected StateMachine<BType> stateMachine;
    public BTypeEntityStates CurrentType { private set; get; }
    #endregion

    #region Override
    public override void Setup(MonsterData.MonsterStat stat)
    {
        // add component
        anim = GetComponent<Animator>();
       
        // set information
        gameObject.name = stat.monsterName;
        initPosition = stat.initTransform;
        initRotation = stat.initRotation;
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        this.gameObject.AddComponent<NavMeshAgent>();
        nav = GetComponent<NavMeshAgent>();
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
    public override void StartConversationInteraction() { ChangeState(BTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(BTypeEntityStates.Indifference); check = false; }
    public override void ChaseInteraction() { ChangeState(BTypeEntityStates.Chase); }
    public override void SpeechlessInteraction() { ChangeState(BTypeEntityStates.Speechless); }
    public override void LookPlayer()
    {
        //Vector3 dir = playerObject.transform.position - transform.position;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * turnSpeed);
        if (!check)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerObject.transform.position - transform.position);
            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            float step = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);

            // 목표 각도와의 차이가 일정 각도 미만이면 코드 실행 중지
            if (angle < stopangle)
            {
                check = true;
            }
        }
    }
    public override void LookOriginal()
    {
        Quaternion targetRotation = Quaternion.Euler(initRotation);
        transform.rotation = targetRotation;
        //Quaternion originalDir = Quaternion.Euler(initRotation);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, originalDir, 5 * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, , 5 * Time.deltaTime);
    }
    #endregion

    #region Method
    public void ChangeState(BTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }
    public void ChasePlayer(){ nav.SetDestination(playerObject.transform.position); }
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
    #endregion
}
