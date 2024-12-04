using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ChaseEntityNameType
{
    Principal,
    HeadOfTeacher,
    ReverseGirl,
    Guard
}

public class OnlyChase : MonoBehaviour
{
    [SerializeField] ChaseEntityNameType type;
    [SerializeField] ChaseCollisionDetect collisionDetect;
    [SerializeField] JumpScare jumpScare;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator anim;
    [SerializeField, Tooltip("�ִϸ��̼� �ӵ�")] protected float multiValue;
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

        collisionDetect.Init(EntityDataManager.Instance.Controller.PlayerTransform);
    }

    protected virtual void Start()
    {
        if (playerTransform == null)
            playerTransform = EntityDataManager.Instance.Controller.PlayerTransform;
        // To Do ~~ ���̷� ����
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.ChaseStart();
        HealthPointManager.Instance.chased = true;
        PenaltyPointManager.Instance.SetChase(true);

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
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.MoveLock = true;
            jumpScare.ActiveJumpScare();
            EntityDataManager.Instance.Controller.InActiveInteractionEntities();
            EntityDataManager.Instance.Controller.DisableChaseGroupExceptOne(this);

            switch (type)
            {
                case ChaseEntityNameType.Principal:
                    if (SteamfeatureController.Instance.FeatureManager.Achievement03.isPrincipalDeath == false)
                    {
                        SteamfeatureController.Instance.FeatureManager.Achievement03.isPrincipalDeath = true;
                        SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
                    }
                    break;
                case ChaseEntityNameType.HeadOfTeacher:
                    if (SteamfeatureController.Instance.FeatureManager.Achievement03.isCatchTeacherDeath == false)
                    {
                        SteamfeatureController.Instance.FeatureManager.Achievement03.isCatchTeacherDeath = true;
                        SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
                    }
                    break;
                case ChaseEntityNameType.ReverseGirl:
                    if (SteamfeatureController.Instance.FeatureManager.Achievement03.isCatchGirlDeath == false)
                    {
                        SteamfeatureController.Instance.FeatureManager.Achievement03.isCatchGirlDeath = true;
                        SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
                    }
                    break;
            }
        }
    }

    public void Chase() { agent.SetDestination(playerTransform.position); }
}
