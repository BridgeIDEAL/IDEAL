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
    [SerializeField] int randomNum;
    NavMeshAgent agent;
    Animator anim;
    AudioSource audioSource;

    int last =-1;

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
        if (agent.remainingDistance < 0.3f)
        {
            curIdx += 1;
            if (maxIdx <= curIdx)
                curIdx = 0;
            agent.SetDestination(positions[curIdx]);
        }
    }

    public void FootStepWalkClip()
    {
        int cnt = audioWalkClips.Length;
        randomNum = Random.Range(0, cnt);
        if (randomNum == last)
            randomNum += 1;
        if (randomNum >= cnt)
            randomNum = 0;
        last = randomNum;
        audioSource.Stop();
        audioSource.clip = audioWalkClips[randomNum];
        audioSource.Play();
    }

    public void FootStepRunClip()
    {
        int cnt = audioRunClips.Length;
        randomNum = Random.Range(0, cnt);
        if (randomNum == last)
            randomNum += 1;
        if (randomNum >= cnt)
            randomNum = 0;
        last = randomNum;
        audioSource.Stop();
        audioSource.clip = audioRunClips[randomNum];
        audioSource.Play();
    }
}
