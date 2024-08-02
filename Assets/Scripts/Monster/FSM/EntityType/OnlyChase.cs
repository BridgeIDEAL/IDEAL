using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class OnlyChase : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Animator anim;
    [SerializeField, Tooltip("애니메이션 속도")] protected float multiValue;
    [SerializeField] protected int deathIndex;

    [SerializeField] protected Transform playerTransform;

    protected virtual void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetFloat("MultiValue", multiValue);
    }

    protected virtual void Start()
    {
        if (playerTransform == null)
            playerTransform = EntityDataManager.Instance.Controller.PlayerTransform;
        EntityDataManager.Instance.Controller.IsChase = true;
    }

    private void Update()
    {
        Chase();
    }

    public void Chase()
    {
        agent.SetDestination(playerTransform.position);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(deathIndex);
            Destroy(EntityDataManager.Instance.Controller.gameObject);
        }
    }

    private void OnDisable()
    {
        EntityDataManager.Instance.Controller.IsChase = false;
    }
}
