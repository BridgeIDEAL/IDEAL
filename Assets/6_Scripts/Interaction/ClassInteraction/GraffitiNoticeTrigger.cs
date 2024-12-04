using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraffitiNoticeTrigger : MonoBehaviour
{
    [SerializeField] GraffitiClass graffitiClass;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            graffitiClass.ActiveGraffitiEvent();
            this.gameObject.SetActive(false);
        }
    }
}
