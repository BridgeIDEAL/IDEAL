using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InStudyRoom : MonoBehaviour
{
    [SerializeField]
    InStudyroomType studyroomType = InStudyroomType.Classroom;

    bool isSelfStudyroom = false;

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

        if (studyroomType == InStudyroomType.SelfStudyroom)
            isSelfStudyroom = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Principal == null)
                return;
            Principal.PlayerInStudyRoom(keepAnEyeTransform, isSelfStudyroom);
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
