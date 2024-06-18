using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoyStudentToward : MonoBehaviour
{
    public Transform endPos;
    NavMeshAgent nav;
    public float timer = 3f;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    public void Starting()
    {
        nav.SetDestination(endPos.position);
    }


    public IEnumerator Toward()
    {
        float curtimer = 0f;
        Vector3 pos = transform.position;
        while (curtimer <= 3f)
        {
            transform.position = Vector3.Lerp(pos, endPos.position, curtimer / timer);
            curtimer += Time.deltaTime;
            yield return null;
        }
    }
}
