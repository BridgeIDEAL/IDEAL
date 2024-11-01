using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class OnlyChase : MonoBehaviour
{
    [SerializeField] ChaseCollisionDetect collisionDetect;
    [SerializeField] JumpScare jumpScare;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator anim;
    [SerializeField, Tooltip("애니메이션 속도")] protected float multiValue;
    [SerializeField] protected int deathIndex;

    [SerializeField] protected Transform playerTransform;

    bool isCatch = false;
    protected virtual void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        anim.SetFloat("MultiValue", multiValue);

        collisionDetect.Init(EntityDataManager.Instance.Controller.PlayerTransform, this.transform);
    }

    protected virtual void Start()
    {
        if (playerTransform == null)
            playerTransform = EntityDataManager.Instance.Controller.PlayerTransform;
        EntityDataManager.Instance.Controller.IsChase = true;
        EntityDataManager.Instance.Controller.AddChaseGroup(this);
    }

    private void Update()
    {
        if (isCatch)
            return;

        Chase();
        if (collisionDetect.IsCollidePlayer())
        {
            agent.speed = 0;
            agent.enabled = false;
            isCatch = true;
            anim.enabled = false;
            jumpScare.ActiveJumpScare();
            EntityDataManager.Instance.Controller.InActiveInteractionEntities();
            EntityDataManager.Instance.Controller.DisableChaseGroupExceptOne(this);
        }
    }

    public void Chase() { agent.SetDestination(playerTransform.position); }
}
