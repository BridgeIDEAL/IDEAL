using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class OnlyChase : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField, Tooltip("애니메이션 속도")] float multiValue;
    [SerializeField] string deathReason;

    Transform playerTransform;

    private void Awake()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        if (anim == null)
            anim = GetComponent<Animator>();
        anim.SetFloat("MultiValue", multiValue);
    }

    private void Start()
    {
        if (playerTransform == null)
            playerTransform = EntityDataManager.Instance.Controller.PlayerTransform;
    }

    private void Update()
    {
        Chase();
    }

    public void Chase()
    {
        agent.SetDestination(playerTransform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameOver(deathReason);
        }
    }

    public void GameOver(string str, int guideLogID = -1)
    {
        int attempts = CountAttempts.Instance.GetAttemptCount();
        if (str.Contains("$attempts"))
        {
            str = str.Replace("$attempts", attempts.ToString());
        }
        if (guideLogID > -1)
        {
            GuideLogManager.Instance.UpdateGuideLogRecord(guideLogID, attempts);
        }
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(str);
    }
}
