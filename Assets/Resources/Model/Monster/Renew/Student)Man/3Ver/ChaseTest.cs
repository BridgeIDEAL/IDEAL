using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseTest : MonoBehaviour
{
    public GameObject go;
    NavMeshAgent nav;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {
        if (nav.hasPath)
        {
            nav.SetDestination(go.transform.position);
            Debug.Log("플레이어 추격!");
        }
        else
        {
            nav.SetDestination(go.transform.position);
            Debug.Log("플레이어 재추격!");
        }
    }
}
