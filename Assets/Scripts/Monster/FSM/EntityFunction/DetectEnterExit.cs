using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnterExit : DetectPlayer
{
    public override bool DetectExecute()
    {
        return isDetectPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDetectPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDetectPlayer = false;
        }
    }
}
