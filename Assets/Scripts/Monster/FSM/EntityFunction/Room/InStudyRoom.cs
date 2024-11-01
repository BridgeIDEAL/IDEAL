using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InStudyRoom : MonoBehaviour
{
    PrincipalPatrol principal = null;
    PrincipalPatrol Principal
    {
        get
        {
            if (principal == null)
                principal = EntityDataManager.Instance.Controller.GetEntity("PatrolPrincipal").gameObject.GetComponent<PrincipalPatrol>();
            return principal;
        }
    }
    [SerializeField] Transform keepAnEyeTransform;
    private void Start()
    {
        if (EventDataManager.Instance.RingAfterSchoolBell)
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Principal == null)
                return;
            Principal.PlayerInStudyRoom(keepAnEyeTransform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Principal == null)
                return;
            Principal.PlayerOutStudyRoom();
        }
    }
}
