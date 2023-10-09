using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [Header("몬스터 변수")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float detectDist;
    [SerializeField] protected GameObject player;
    [SerializeField] protected AnimationState monsterState = AnimationState.Idle;

    void Start()
    {
        Init();
    }
    void Update()
    {
        switch (monsterState)
        {
            case AnimationState.Idle:
                UpdateIdle();
                break;
            case AnimationState.Walk:
                UpdateWalk();
                break;
            case AnimationState.Attack:
                UpdateAttack();
                break;
        }
    }

    public abstract void Init();
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateWalk() { }
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

