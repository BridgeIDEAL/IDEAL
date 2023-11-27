using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] private ScriptHub scriptHub;
    private FirstPersonController firstPersonController;
    
    void Awake(){
        firstPersonController = scriptHub.firstPersonController;
    }
    public void VisibleMouseCursor(){
        firstPersonController.CameraRotationLock = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void InvisibleMouseCursor(){
        firstPersonController.CameraRotationLock = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
