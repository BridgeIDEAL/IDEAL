using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGuardDetectController : MonoBehaviour
{
    [SerializeField] OnGuardDetection[] onguardDetection;
    
    private void Start()
    {
        SetJumpScareCollisionState(false);
    }

    protected bool isOnGuard = false;
    public bool IsOnGuard
    {
        get
        {
            return isOnGuard;
        }
        set
        {
            isOnGuard = value;
            if (value)
            {
                SetJumpScareCollisionState(true);
            }
            else
            {
                SetOnGuardCollisionState(false);
            }
        }
    }

    void SetOnGuardCollisionState(bool isActive) { this.gameObject.SetActive(isActive); }

    void SetJumpScareCollisionState(bool isActive)
    {
        int cnt = onguardDetection.Length;
        for(int i = 0; i < cnt; i++)
        {
            onguardDetection[i].gameObject.SetActive(isActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsOnGuard = true;
        }
    }
}
