using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private AbstractInteraction abstractInteraction;
    public void DetectedRay(){
        abstractInteraction.DetectedRay();
    }

    public void DetectedInteraction(){
        abstractInteraction.DetectedInteraction();
    }

}
