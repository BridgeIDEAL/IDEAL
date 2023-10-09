using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LowerMonsterController : BaseController
{
    [SerializeField] float speed = 5f;
    [SerializeField] float range= 10f;
    [SerializeField] GameObject findParticle;
    NavMeshAgent nav;
    
    public override void Init()
    {
        moveSpeed = speed;
        detectDist = range;
        nav = GetComponent<NavMeshAgent>();
    }

    protected override void UpdateIdle()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        if (target == null)
            return;
        float dist = (target.transform.position - transform.position).magnitude;
        if (dist <= range)
        {
            player = target;
            monsterState = AnimationState.Walk;
        }
    }
    protected override void UpdateWalk()
    {
        if (player != null)
        {
            findParticle.SetActive(true);
            float distance = (player.transform.position- transform.position).magnitude;
            Vector3 dir = player.transform.position - transform.position;
            if(distance > 0.2f)
            {
                nav.SetDestination(player.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), moveSpeed * Time.deltaTime);
            }
        }
    }

    protected override void UpdateAttack()
    {

    }
}
