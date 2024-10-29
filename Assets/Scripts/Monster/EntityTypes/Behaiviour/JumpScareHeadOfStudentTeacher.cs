using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareHeadOfStudentTeacher : JumpScare
{
    [SerializeField] Transform belowTransform;
    [SerializeField] float waitTime;
    [SerializeField] float lookupTime;
    public override void SetCameraSetting()
    {
        //MainCamEffect mainCamEffect = Camera.main.GetComponent<MainCamEffect>();
        //mainCamEffect.CallGraduallySetFieldOfView(16);
        virtualCam.transform.rotation = Quaternion.LookRotation(belowTransform.position - virtualCam.transform.position);
        virtualCam.transform.position = jumpscareCamTransform.position;
        if (jumpscareCharacter.activeSelf == false)
            jumpscareCharacter.SetActive(true);
        CallLookUp();
    }

    public void CallLookUp()
    {
        StartCoroutine(LookUp());
    }

    IEnumerator LookUp()
    {
        yield return new WaitForSeconds(waitTime);
        float timer = 0f;
        Quaternion stRot = virtualCam.transform.rotation;
        Quaternion edRot = jumpscareCamTransform.rotation;

        while (timer < lookupTime)
        {
            timer += Time.deltaTime;
            virtualCam.transform.rotation = Quaternion.Slerp(stRot, edRot, timer / lookupTime);
            yield return null;
        }
        virtualCam.transform.rotation = edRot;
    }
}
