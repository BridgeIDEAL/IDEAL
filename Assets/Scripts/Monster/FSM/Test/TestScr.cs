using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScr : MonoBehaviour
{
    GameObject interObj;
    private void Awake()
    {
        interObj = this.gameObject;
    }
    void Update()
    {
        if (interObj != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.EntityEvent.SendMessage(EventType.ChaseInteraction, interObj);
                Debug.Log("추격 지시");
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                GameManager.EntityEvent.SendMessage(EventType.RestInteraction, interObj);
                Debug.Log("제자리 위치");
            }
        }

    }
}
