using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private AbstractInteraction abstractInteraction;
    [SerializeField] private InteractionOutline interactionOutline;
    [SerializeField] private bool outlineActive = true;

    public void DetectedRay(){
        abstractInteraction.DetectedRay();
        if(outlineActive) interactionOutline.SetOutlineObject(true);
    }

    public void OutOfRay(){
        abstractInteraction.OutOfRay();
        if(outlineActive) interactionOutline.SetOutlineObject(false);
    }

    public void DetectedInteraction(){
        abstractInteraction.DetectedInteraction();
    }

    public float GetRequiredTime(){
        return abstractInteraction.RequiredTime;
    }

}
