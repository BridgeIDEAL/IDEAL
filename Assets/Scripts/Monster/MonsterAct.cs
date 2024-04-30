using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI
    ;
public class MonsterAct : MonoBehaviour
{
    Animator anim;
    NavMeshAgent nav;
    public Transform player;
    private void Awake()
    {
        anim = GetComponentInParent<Animator>();
        nav = GetComponentInParent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (nav.remainingDistance < 0.5f)
        {
            nav.ResetPath();
            anim.SetBool("WALK", false);
            anim.SetBool("IDLE", true);
        }
        else
        {
            nav.SetDestination(player.position);
            anim.SetBool("IDLE", false);
            anim.SetBool("WALK", true);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            nav.SetDestination(player.position);
            anim.SetBool("IDLE", false);
            anim.SetBool("WALK", true);
        }
    }
    private void LateUpdate()
    {
        
    }
}
