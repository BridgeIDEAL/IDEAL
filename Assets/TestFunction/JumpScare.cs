using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JumpScare : MonoBehaviour
{
    [SerializeField, Header("Death Scene Last Camera Position")] protected Transform jumpscareCamTransform;
    [SerializeField] protected GameObject jumpscareCharacter;
    protected CinemachineVirtualCamera virtualCam = null;

    public virtual void GameOver() 
    {
        /************* Chan hee ***********************/
        /***** Put GameOver Camera Effect ********/
    }

    /// <summary>
    /// Principal : Must Init PlayerTransform
    /// </summary>
    public virtual void ActiveJumpScare()
    {
        FindFollowCameraNRelease();
        SetCameraSetting();
    }

    public void FindFollowCameraNRelease()
    {
        if (virtualCam == null)
        {
            CinemachineBrain brain = Camera.main.GetComponent<CinemachineBrain>();
            if (brain != null && brain.ActiveVirtualCamera is CinemachineVirtualCamera vCam)
            {
                virtualCam = vCam;
            }
        }

        // Release Follow Cam
        if (virtualCam != null) 
        {
            virtualCam.Follow = null;
        }
    }

    public abstract void SetCameraSetting();
}