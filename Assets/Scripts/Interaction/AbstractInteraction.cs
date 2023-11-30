using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
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
        if(audioSource!= null) audioSource.Play();
        ActInteraction();
    }

    protected abstract void ActInteraction();
}
