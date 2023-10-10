using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LowerMonsterController : BaseController
{
    NavMeshAgent nav;
    [SerializeField] GameObject findParticle;
    
    public override void Init()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void DetectPlayer()
    {
        if (chasePlayer) // �Ѵ� �� ���� �޶����ų� �ִ� ���� ������ ����� �߰� ����
        {
            float yDist = Mathf.Abs(transform.position.y - player.transform.position.y);
            chasePlayer = (maxChaseDist < (transform.position - player.transform.position).magnitude || yDist > 2.5f) ? false : true;
        }        
        else
        {
            if (Physics.CheckSphere(transform.position, detectDist, playerMask)) // ���� ���� ���� �÷��̾ �ִٸ� �߰�
            {
                chasePlayer = true;
                monsterState = AnimationState.Chase;
            }
        }
    }

    protected override void UpdateIdle(){  }
    protected override void UpdateChase()
    {
        if (chasePlayer)
        {
            findParticle.SetActive(true);
            float distance = (player.transform.position - transform.position).magnitude;
            Vector3 dir = player.transform.position - transform.position;
            if (distance > 1f)
            {
                nav.SetDestination(player.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            monsterState = AnimationState.Idle;
            nav.SetDestination(transform.position);
        }
    }

    protected override void UpdateAttack(){ }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectDist);
    }
}
