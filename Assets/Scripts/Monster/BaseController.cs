using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    protected bool chasePlayer = false;
    protected LayerMask playerMask =1<<3;
    
    [Header("이형체 변수")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float detectDist;
    [SerializeField] protected float maxChaseDist;
    [SerializeField] protected AnimationState monsterState = AnimationState.Idle;

    void Start(){ Init();}

    void Update()
    {
        DetectPlayer();
        switch (monsterState)
        {
            //anim.CrossFade("Idle", 0.1f); 애니메이션 이름
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
    protected virtual void DetectPlayer() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateChase() { }
    protected virtual void UpdateAttack() { }
    
}

/*public virtual AnimationState State
{
    get { return playerState; }
    set
    {
        Animator anim = GetComponent<Animator>();
        playerState = value;
        switch (playerState)
        {
            case AnimationState.Idle:
                anim.CrossFade("Idle", 0.1f);
                break;
            case AnimationState.Walk:
                anim.CrossFade("Run", 0.1f);
                break;
            case AnimationState.Attack:
                anim.CrossFade("Attack", 0.1f, -1, 0);
                break;
        }
    }
}*/

