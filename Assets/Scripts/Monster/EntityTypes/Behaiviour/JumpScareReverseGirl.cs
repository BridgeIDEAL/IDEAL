using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareReverseGirl : JumpScare
{
    [SerializeField] float roateTime;

    public override void SetCameraSetting()
    {
        //MainCamEffect mainCamEffect = Camera.main.GetComponent<MainCamEffect>();
        //mainCamEffect.CallGraduallySetFieldOfView(16);
        
        if (jumpscareCharacter.activeSelf == false)
            jumpscareCharacter.SetActive(true);
        CallRotate();
    }

    public void CallRotate()
    {
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        float timer = 0f;
        Quaternion stRot = virtualCam.transform.rotation;
        Quaternion edRot = jumpscareCamTransform.rotation;

        Vector3 stPos = virtualCam.transform.position;
        Vector3 edPos = jumpscareCamTransform.position;

        while (timer < roateTime)
        {
            timer += Time.deltaTime;
            virtualCam.transform.rotation = Quaternion.Slerp(stRot, edRot, timer / roateTime);
            virtualCam.transform.position = Vector3.Lerp(stPos, edPos, timer / roateTime);
            yield return null;
        }
        virtualCam.transform.rotation = edRot;
        virtualCam.transform.position = edPos;
    }
}
