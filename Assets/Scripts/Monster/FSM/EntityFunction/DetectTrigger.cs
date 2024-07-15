using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrigger : DetectPlayer
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
}
