using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePenaltyObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float activeTime = 15.0f;
    private float stepTimer = 0.0f;

    private Transform playerTransform = null;

    public void PlayActiveSound(){
        Debug.Log("PlayActiveSound");
        audioSource.Play();
    }

    public void SetPlayerTransform(Transform playerT){
        playerTransform = playerT;
    }

    void Update(){
        if(playerTransform != null){
            Vector3 targetDir = (playerTransform.position - transform.position).normalized;
            // 해당 방향으로의 회전을 생성합니다.
            Quaternion lookRotation = Quaternion.LookRotation(targetDir);
            
            // 현재 오브젝트의 회전에서 y축만 변경하여 적용합니다.
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        if(stepTimer > activeTime){
            stepTimer = 0.0f;
            TurnOff();
        }
        stepTimer += Time.deltaTime;
    }

    private void TurnOff(){
        playerTransform = null;
        this.gameObject.SetActive(false);
    }
}
