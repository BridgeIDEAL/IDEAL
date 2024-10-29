using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareSudden : JumpScare
{
    public override void SetCameraSetting()
    {
        virtualCam.transform.rotation = jumpscareCamTransform.rotation;
        virtualCam.transform.position = jumpscareCamTransform.position;
        jumpscareCharacter.gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 setPosition, Quaternion rotation)
    {
        jumpscareCharacter.transform.position = setPosition;
        jumpscareCharacter.transform.rotation = rotation;
    }
}
