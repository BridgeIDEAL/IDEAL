using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScarePrincipal : JumpScare
{
    public Transform playerTransform { get; set; } = null;
    [SerializeField] float lookPrincipalTime;
    public override void SetCameraSetting()
    {
       MainCamEffect mainCamEffect = Camera.main.GetComponent<MainCamEffect>();
       mainCamEffect.FallDownVision(CallPrincipalJumpScare);
       mainCamEffect.CallGraduallySetFieldOfView(16);
    }

    public void CallPrincipalJumpScare()
    {
        if(playerTransform == null)
        {
            Debug.LogError("플레이어 정보를 주지 않았다!");
            return;
        }

        // To Do ~~ Select Delet or Use
        //Vector3 jumpscarePosition = jumpscareCamTransform.position;
        //jumpscarePosition.y = 0;
        float distance = Vector3.Distance(transform.position, jumpscareCamTransform.position);

        Vector3 direction = transform.position - playerTransform.position;
        direction.y = 0;
        direction = direction.normalized;

        jumpscareCharacter.transform.position = playerTransform.position + distance * direction;

        if(jumpscareCharacter.activeSelf==false)
            jumpscareCharacter.SetActive(true);
        
        StartCoroutine(PrincipalJumpScare());
    }

    IEnumerator PrincipalJumpScare()
    {
        float timer = 0f;
        

        while (timer< lookPrincipalTime) 
        {
            timer+= Time.deltaTime;

            yield return null;
        }
    }
}
