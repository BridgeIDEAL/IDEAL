using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPlace : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && GameManager.EntityEvent.IsChase)
        {
            Debug.Log("Ãæµ¹!");
            GameManager.EntityEvent.SendStateEventMessage(StateEventType.Rest);
        }
    }
}
