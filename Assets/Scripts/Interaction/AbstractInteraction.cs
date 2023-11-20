using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    public abstract float RequiredTime {get;}

    private Coroutine interactionCoroutine;
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
