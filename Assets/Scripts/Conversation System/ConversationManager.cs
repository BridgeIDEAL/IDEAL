using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] FirstPersonController firstPersonController;
    public void VisibleMouseCursor(){
        firstPersonController.CameraRotationLock = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void InvisibleMouseCursor(){
        firstPersonController.CameraRotationLock = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
