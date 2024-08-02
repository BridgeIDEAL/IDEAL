using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneColliderManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Rest")) {
            PenaltyPointManager.Instance.GoSafeZone(true);
            Debug.Log("Trigger IN");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Rest")) {
            PenaltyPointManager.Instance.GoSafeZone(false);
            Debug.Log("Tirgger Exit");
        }
    }
}
