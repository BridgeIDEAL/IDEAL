using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootstepTest : MonoBehaviour
{
    [SerializeField] Vector3[] positions;
    [SerializeField] AudioClip[] audioWalkClips;
    [SerializeField] AudioClip[] audioRunClips;

    [SerializeField] int curIdx = 1;
    [SerializeField] int maxIdx = 0;

    NavMeshAgent agent;
    Animator anim;
    AudioSource audioSource;

    private void Awake()
    {
        maxIdx = positions.Length;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Walk", true);
        agent.SetDestination(positions[curIdx]);
    }

    public void Update()
    {
        if (agent.remainingDistance > 0.3f)
        {
            curIdx += 1;
            if (maxIdx <= curIdx)
                curIdx = 0;
            agent.SetDestination(positions[curIdx]);
        }
    }
}
