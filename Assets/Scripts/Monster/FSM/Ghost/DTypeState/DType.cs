using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DType : BaseEntity
{
    #region Component
    protected bool isWatch = false;
    protected float chaseSpeed = 3f;
    protected Animator anim;
    protected NavMeshAgent nav;
    protected State<DType>[] states;
    protected StateMachine<DType> stateMachine;
    [SerializeField] protected Quaternion towardRotation;
    public DTypeEntityStates CurrentType { private set; get; }
    #endregion

    #region Virtual
    public virtual void WatchExecute() { if (!CanDetectPlayer()) ChangeState(DTypeEntityStates.Indifference); } // 경계 상태 유지
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
        anim = GetComponent<Animator>();
        this.gameObject.AddComponent<NavMeshAgent>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = chaseSpeed;
        nav.radius = 0.2f;
        towardRotation = Quaternion.Euler(initRotation.x, initRotation.y, initRotation.z);
        // set statemachine
        CurrentType = DTypeEntityStates.Indifference;
        states = new State<DType>[6];
        states[(int)DTypeEntityStates.Indifference] = new DTypeStates.Indifference();
        states[(int)DTypeEntityStates.Interaction] = new DTypeStates.Interaction();
        states[(int)DTypeEntityStates.Watch] = new DTypeStates.Watch();
        states[(int)DTypeEntityStates.Aggressive] = new DTypeStates.Aggressive();
        states[(int)DTypeEntityStates.Chase] = new DTypeStates.Chase();
        states[(int)DTypeEntityStates.Speechless] = new DTypeStates.Speechless();
        stateMachine = new StateMachine<DType>();
        stateMachine.Setup(this, states[(int)CurrentType]);
    }

    public override void UpdateBehavior() { stateMachine.Execute(); }
    public override void StartConversationInteraction() { ChangeState(DTypeEntityStates.Interaction); }
    public override void EndConversationInteraction() { ChangeState(DTypeEntityStates.Indifference); }
    public override void ChaseInteraction() { ChangeState(DTypeEntityStates.Aggressive); }
    public override void SpeechlessInteraction() { ChangeState(DTypeEntityStates.Speechless); }
    public override void LookPlayer() {
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
    public void ChangeState(DTypeEntityStates newState)
    {
        CurrentType = newState;
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void ChasePlayer(){ nav.SetDestination(playerObject.transform.position);}

    public void DetectPlayer()
    {
        if (isWatch)
            return;
        if (CanDetectPlayer())
            ChangeState(DTypeEntityStates.Watch);
    }

    public bool CanDetectPlayer()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude > sightDistance)
            return false;
        else
            return true;
    }

    public void StartWatchTimer() { StartCoroutine("WatchTimer"); }
    public void EndWatchTimer() { StopCoroutine("WatchTimer"); }
    public IEnumerator WatchTimer()
    {
        yield return new WaitForSeconds(10f);
        if (CurrentType == DTypeEntityStates.Watch && !isWatch)
            isWatch = true;
        yield break;
    }

    public void SetReposition() { StartCoroutine("ResetPosition"); }
    public IEnumerator ResetPosition()
    {
        //nav.isStopped = true;
        //nav.speed = 0f;
        nav.ResetPath();
        nav.enabled = false;
        yield return new WaitForSeconds(0.8f);
        transform.position = initPosition;
        transform.eulerAngles = initRotation;
        nav.enabled = true;
        //nav.speed = chaseSpeed;
        //nav.isStopped = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && CurrentType == DTypeEntityStates.Chase)
        {
            GameOverManager.Instance.GameOver("학생에게 끌려간 후 실종됨.");
        }
    }

    public void SetAnimation(DTypeEntityStates entityAnim)
    {
        switch (entityAnim)
        {
            case DTypeEntityStates.Indifference:
                anim.CrossFade("Idle", 0.2f);
                break;
            case DTypeEntityStates.Watch:
                anim.CrossFade("Idle", 0.2f);
                break;
            case DTypeEntityStates.Interaction:
                anim.CrossFade("Idle", 0.2f);
                break;
            case DTypeEntityStates.Aggressive:
                anim.CrossFade("Aggressive", 0.2f);
                break;
            case DTypeEntityStates.Chase:
                anim.CrossFade("Walk", 0.2f);
                break;
        }
    }
    #endregion
}
