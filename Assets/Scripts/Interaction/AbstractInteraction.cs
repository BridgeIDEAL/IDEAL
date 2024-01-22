using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public abstract class AbstractInteraction : MonoBehaviour
{
    public AudioSource audioSource;     // 상속받은 클래스에서 ActInteraction 부분에서 상호작용 성공 or  실패시 가져다가 쓰도록
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
