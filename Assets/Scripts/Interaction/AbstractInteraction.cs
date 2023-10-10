using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    public void DetectedRay(){
        Debug.Log("Detected Ray!  " + GetDetectedString());
    }

    protected abstract string GetDetectedString();


    public void DetectedInteraction(){
        Debug.Log("Detected Interaction!");
        ActInteraction();
    }

    protected abstract void ActInteraction();
}
