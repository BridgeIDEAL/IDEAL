using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyRoom1F : MonoBehaviour
{
    [SerializeField] PrincipalPatrol principal;

    private void Start()
    {
        if (!EntityDataManager.Instance.IsLastEvent)
            principal = EntityDataManager.Instance.Controller.GetEntity("PatrolPrincipal").gameObject.GetComponent<PrincipalPatrol>();
        else
            Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (principal == null)
                return;
            principal.IsInStudyRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (principal == null)
                return;
            principal.IsInStudyRoom = false;
        }
    }
}
