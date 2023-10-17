using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    [SerializeField]
    private InteractionManager interactionManager;
    
    public void DetectedRay(){
        interactionManager.uIManager.PrintInteractionText(GetDetectedString());
    }

    public void OutOfRay(){
        interactionManager.uIManager.DeleteInteractionText();
    }

    protected abstract string GetDetectedString();


    public void DetectedInteraction(){
        ActInteraction();
    }

    protected abstract void ActInteraction();
}
