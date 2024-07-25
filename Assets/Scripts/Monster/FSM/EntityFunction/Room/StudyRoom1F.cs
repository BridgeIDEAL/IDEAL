using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyRoom1F : MonoBehaviour
{
    [SerializeField] PrincipalPatrol principal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            principal.IsInStudyRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            principal.IsInStudyRoom = false;
        }
    }
}
