using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareOnGuard : JumpScare
{
    public override void SetCameraSetting()
    {
        virtualCam.transform.position = jumpscareCamTransform.position;
        virtualCam.transform.rotation = jumpscareCamTransform.rotation;

        if (jumpscareCharacter.activeSelf == false)
                jumpscareCharacter.SetActive(true);
    }
}
