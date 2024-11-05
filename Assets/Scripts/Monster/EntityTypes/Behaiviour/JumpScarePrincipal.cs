using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScarePrincipal : JumpScare
{
    Transform playerTransform = null;
    public Transform PlayerTransform
    {
        get
        {
            if (playerTransform == null) 
            {
                playerTransform = EntityDataManager.Instance.Controller.PlayerTransform;
            }
            return playerTransform;
        }
    }
    [SerializeField] float waitTime;
    [SerializeField] float lookPrincipalTime;
    public override void SetCameraSetting()
    {
       MainCamEffect mainCamEffect = Camera.main.GetComponent<MainCamEffect>();
       mainCamEffect.FallDownVision(CallPrincipalJumpScare, waitTime);
       //mainCamEffect.CallGraduallySetFieldOfView(18.9f);
    }

    public void CallPrincipalJumpScare()
    {
        if(PlayerTransform == null)
        {
            Debug.LogError("플레이어 정보를 주지 않았다!");
            return;
        }

        //Vector3 jumpscarePosition = jumpscareCamTransform.position;
        //jumpscarePosition.y = 0;
        //float distance = Vector3.Distance(transform.position, jumpscareCamTransform.position);
        float distance = 3f;

        Vector3 jumpscareDir = PlayerTransform.position - transform.position;
        jumpscareDir.y = 0;
        jumpscareDir = jumpscareDir.normalized;

        Vector3 rotateDirection = transform.position - PlayerTransform.position;
        rotateDirection.y = 0;
        rotateDirection = rotateDirection.normalized;

        Vector3 tempPlayer = PlayerTransform.position;
        tempPlayer.y = 0;

        Quaternion lookRot = Quaternion.LookRotation(rotateDirection);
        jumpscareCharacter.transform.position = tempPlayer + distance * rotateDirection;
        
        Quaternion jumpScareRot = Quaternion.LookRotation(jumpscareDir);
        jumpscareCharacter.transform.rotation = jumpScareRot;

        if (jumpscareCharacter.activeSelf==false)
            jumpscareCharacter.SetActive(true);
        
        StartCoroutine(PrincipalJumpScare());
    }

    IEnumerator PrincipalJumpScare()
    {
        float timer = 0f;
        Quaternion stRot = virtualCam.transform.rotation;
        Quaternion edRot = jumpscareCamTransform.rotation;
        Vector3 stPos = virtualCam.transform.position;
        Vector3 edPos = jumpscareCamTransform.transform.position;

        while (timer< lookPrincipalTime) 
        {
            timer+= Time.deltaTime;
            virtualCam.transform.rotation = Quaternion.Slerp(stRot, edRot, timer / lookPrincipalTime);
            virtualCam.transform.position = Vector3.Lerp(stPos, edPos, timer / lookPrincipalTime);
            yield return null;
        }
        virtualCam.transform.rotation = edRot;
        virtualCam.transform.position = edPos;
    }
}
