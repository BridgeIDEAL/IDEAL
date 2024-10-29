using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScarePrincipal : JumpScare
{
    public Transform playerTransform { get; set; } = null;
    [SerializeField] float waitTime;
    [SerializeField] float lookPrincipalTime;
    public override void SetCameraSetting()
    {
       MainCamEffect mainCamEffect = Camera.main.GetComponent<MainCamEffect>();
       mainCamEffect.FallDownVision(CallPrincipalJumpScare, waitTime);
       mainCamEffect.CallGraduallySetFieldOfView(18.9f);
    }

    public void CallPrincipalJumpScare()
    {
        if(playerTransform == null)
        {
            Debug.LogError("플레이어 정보를 주지 않았다!");
            return;
        }

        //Vector3 jumpscarePosition = jumpscareCamTransform.position;
        //jumpscarePosition.y = 0;
        //float distance = Vector3.Distance(transform.position, jumpscareCamTransform.position);
        float distance = 3.5f;

        Vector3 direction = transform.position - playerTransform.position;
        direction.y = 0;
        direction = direction.normalized;

        Vector3 tempPlayer = playerTransform.position;
        tempPlayer.y = 0;

        Vector3 rotateDirection = playerTransform.position - transform.position;
        rotateDirection.y = 0;
        rotateDirection = rotateDirection.normalized;
        Quaternion lookRot = Quaternion.LookRotation(rotateDirection, Vector3.up);
        
        jumpscareCharacter.transform.position = tempPlayer + distance * direction;
        jumpscareCharacter.transform.rotation = lookRot;

        if (jumpscareCharacter.activeSelf==false)
            jumpscareCharacter.SetActive(true);
        
        StartCoroutine(PrincipalJumpScare());
    }

    IEnumerator PrincipalJumpScare()
    {
        float timer = 0f;
        Quaternion stRot = virtualCam.transform.rotation;
        Quaternion edRot = jumpscareCamTransform.rotation;

        while (timer< lookPrincipalTime) 
        {
            timer+= Time.deltaTime;
            virtualCam.transform.rotation = Quaternion.Slerp(stRot, edRot, timer / lookPrincipalTime);
            yield return null;
        }
        virtualCam.transform.rotation = edRot;
    }
}
