using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected GameObject player;
    protected bool chasePlayer = false;
    protected LayerMask playerMask =1<<3;

    [Header("이형체 변수")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float chaseSpeed;
    [SerializeField] protected float detectDist;
    [SerializeField] protected float maxChaseDist;
    [SerializeField] protected float attackRange;
    [SerializeField] protected bool patrolMonster = false;
    [SerializeField] protected AnimationState monsterState = AnimationState.Idle;

    public virtual AnimationState State
    {
        get { return monsterState; }
        set
        {
            //Animator anim = GetComponent<Animator>();
            monsterState = value;
            switch (monsterState)
            {
                case AnimationState.Idle:
                    //anim.CrossFade("Idle", 0.1f);
                    break;
                case AnimationState.Chase:
                    //anim.CrossFade("Run", 0.1f);
                    break;
                case AnimationState.Attack:
                    //anim.CrossFade("Attack", 0.1f, -1, 0);
                    break;
            }
        }
    }

    void Start(){ Init();}

    void Update()
    {
        switch (monsterState)
        {
            case AnimationState.Idle:
                UpdateIdle();
                break;
            case AnimationState.Chase:
                UpdateChase();
                break;
            case AnimationState.Attack:
                UpdateAttack();
                break;
        }
    }

    public abstract void Init();
    protected virtual void Patroll() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateChase() { }
    protected virtual void UpdateAttack() { }
    
}



