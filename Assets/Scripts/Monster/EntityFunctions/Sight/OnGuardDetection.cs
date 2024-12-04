using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGuardDetection : MonoBehaviour
{
    [SerializeField] JumpScareSudden jumpscareSudden = null;
    [SerializeField] Transform jumpscareSuddentTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jumpscareSudden.SetPosition(jumpscareSuddentTransform.position, jumpscareSuddentTransform.rotation);
            jumpscareSudden.ActiveJumpScare();
        }
    }
}
