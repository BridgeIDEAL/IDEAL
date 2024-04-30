using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPlace : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && IdealSceneManager.Instance.CurrentGameManager.EntityEvent.IsChase)
        {
            Debug.Log("�浹!");
            IdealSceneManager.Instance.CurrentGameManager.EntityEvent.SendStateEventMessage(StateEventType.Rest);
        }
    }
}
