using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastEventTrigger : MonoBehaviour
{
    bool once = true;
    public ChaseEventType[] chaseEventTypes;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && once)
        {
            once = false;
            ActiveChaseEntity();
        }
    }

    public void ActiveChaseEntity()
    {
        int cnt = chaseEventTypes.Length;
        for(int idx=0; idx<cnt; idx++)
        {
            EntityDataManager.Instance.Controller.ActiveChaseEntity(chaseEventTypes[idx]);
        }
    }
}
