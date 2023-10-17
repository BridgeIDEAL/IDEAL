using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    
    public void DetectedRay(){
        UIManager.Instance.PrintInteractionText(GetDetectedString());
    }

    public void OutOfRay(){
        UIManager.Instance.DeleteInteractionText();
    }

    protected abstract string GetDetectedString();


    public void DetectedInteraction(){
        ActInteraction();
    }

    protected abstract void ActInteraction();
}
