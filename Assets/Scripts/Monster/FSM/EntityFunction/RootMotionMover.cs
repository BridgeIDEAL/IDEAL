using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotionMover : MonoBehaviour
{
    [SerializeField, Tooltip("쓰고자 하는 루트 모션의 종류 : 걷기, 뛰기")] RootMotionType motionType;
    [SerializeField, Range(0f, 0.5f)] float stopDistance = 0.1f;
    [SerializeField] Transform destination;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;

    /// <summary>
    /// Call this when you use root motion in timeline scene
    /// </summary>    
    public void SetMovePoint()
    {
        if (agent == null)
            agent = GetComponentInChildren<NavMeshAgent>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        StartCoroutine(SetMovePointCor());
    }

    public IEnumerator SetMovePointCor()
    {
        SetAnimation(true);
        agent.SetDestination(destination.position);
        while(agent.remainingDistance>stopDistance)
        {
            yield return null;
        }
        SetAnimation(false);
        agent.ResetPath();
    }

    public void SetAnimation(bool _setBool)
    {
        switch (motionType)
        {
            case RootMotionType.Walk:
                anim.SetBool("Walk", _setBool);
                break;
            case RootMotionType.Run:
                anim.SetBool("Run", _setBool);
                break;
            default:
                break;
        }
    }
}
