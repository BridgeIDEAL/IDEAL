using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private AbstractInteraction abstractInteraction;
    [SerializeField] private InteractionOutline interactionOutline;

    public void DetectedRay(){
        abstractInteraction.DetectedRay();
        interactionOutline.SetOutlineObject(true);
    }

    public void OutOfRay(){
        interactionOutline.SetOutlineObject(false);
    }

    public void DetectedInteraction(){
        abstractInteraction.DetectedInteraction();
    }

}
