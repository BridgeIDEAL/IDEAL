using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LowerMonsterController : BaseController
{
    Rigidbody rb;
    NavMeshAgent nav;
    ParticleSystem findParticle;
    public override void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        findParticle = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    protected override void UpdateIdle() {
        nav.speed = moveSpeed;
        if(!patrolMonster)
            Patroll();
        else
            if (nav.remainingDistance <= nav.stoppingDistance)
                patrolMonster = false;
        if (Physics.CheckSphere(transform.position, detectDist, playerMask) && Mathf.Abs(transform.position.y - player.transform.position.y) < 5.5f) // 감지 범위 내에 플레이어가 있다면 추격
        {
            findParticle.Play();
            nav.speed = chaseSpeed;
            monsterState = AnimationState.Chase;
        }
    }

    protected override void Patroll()
    {
        Vector3 point;
        patrolMonster = true;
        if (RandomPoint(transform.position, detectDist, out point)) //pass in our centre point and radius of area
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            nav.SetDestination(point);
        }
    }

    protected bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    protected override void UpdateChase()
    {
        chasePlayer = (maxChaseDist < (transform.position - player.transform.position).magnitude) ? false : true;
        if (chasePlayer)
        {
            float dist = (transform.position - player.transform.position).magnitude;
            if (dist<=nav.stoppingDistance) 
            {
                nav.SetDestination(transform.position);
                monsterState = AnimationState.Attack;
            }
            else
            {
                nav.SetDestination(player.transform.position);
                Vector3 dir = player.transform.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            monsterState = AnimationState.Idle;
            nav.SetDestination(transform.position);
        }
    }

    protected override void UpdateAttack(){ HitPlayer(); }

    public void HitPlayer()
    {
        if ((transform.position - player.transform.position).magnitude <= attackRange)
        {
            Debug.Log("플레이어를 공격중입니다.");
            State = AnimationState.Attack;
        }
        else
            State = AnimationState.Chase;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectDist);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, maxChaseDist);
    }
}
