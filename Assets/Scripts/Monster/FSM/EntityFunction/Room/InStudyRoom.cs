using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InStudyRoom : MonoBehaviour
{
    [SerializeField] PrincipalPatrol principal;
    [SerializeField] Transform keepAnEyeTransform;
    private void Start()
    {
        if (!EntityDataManager.Instance.IsLastEvent)
        {
            if(principal == null)
                principal = EntityDataManager.Instance.Controller.GetEntity("PatrolPrincipal").gameObject.GetComponent<PrincipalPatrol>();
        }
        else
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (principal == null)
                return;
            principal.PlayerInStudyRoom(keepAnEyeTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (principal == null)
                return;
            principal.PlayerOutStudyRoom();
        }
    }
}
