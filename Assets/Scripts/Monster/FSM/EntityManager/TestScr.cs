using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScr : MonoBehaviour
{
    GameObject interObj;

    // Update is called once per frame

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
                FSMManager.instance.entityEvent.SendMessage(EventType.ChaseInteraction, interObj);
                Debug.Log("�߰� ����");
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                FSMManager.instance.entityEvent.SendMessage(EventType.RestInteraction, interObj);
                Debug.Log("���ڸ� ��ġ");
            }
        }

    }
}
