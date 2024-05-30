using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DetectPlayer : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject player;
    public Animator anim;
    public float detectDist = 5f;
    bool isChase = false;

    public void CheckPlayer()
    {
        float dist = (transform.position - player.transform.position).magnitude;
        if (dist < detectDist)
        {
            isChase = true;
        }
        else
        {
            isChase = false;
        }
    }

    public void Update()
    {
        CheckPlayer();
        if (isChase)
        {
            anim.SetBool("WALK", true);
            anim.SetFloat("WALKVAL", 0.5f);
            anim.SetFloat("WalkAnimSpeed", nav.speed/1.5f);
            anim.SetBool("IDLE", false);
            nav.SetDestination(player.transform.position);
        }
        else
        {
            anim.SetBool("IDLE", true);
            anim.SetBool("WALK", false);
            nav.ResetPath();
        }
    }
}
