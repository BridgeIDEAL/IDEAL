using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;

public class LampSensor : MonoBehaviour
{
    [SerializeField] GameObject realtimeLamp;
    [SerializeField] float turnOffTime = 3f;
    bool isNearSensor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearSensor = true;
            realtimeLamp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearSensor = false;
            TurnOff();
        }
    }

    public void TurnOff() { StopAllCoroutines(); StartCoroutine(CLampTimer()); }

    IEnumerator CLampTimer()
    {
        float timer = 0f;
        while (timer<= turnOffTime)
        {
            if (isNearSensor)
                yield break;
            timer+= Time.deltaTime;
            yield return null; 
        }

        if (isNearSensor)
            yield break;
        realtimeLamp.SetActive(false);
    }

}
