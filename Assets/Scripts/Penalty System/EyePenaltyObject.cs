using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyePenaltyObject : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    private float activeTime = 5.0f;
    private float stepTimer = 0.0f;

    private Transform playerTransform = null;

    [SerializeField] private Transform headTransform;

    void Start(){
        headTransform = transform.GetChild(1);
    }

    public void PlayActiveSound(){
        audioSource.Play();
        // IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerEffectSound.PlayEffectSound(TempEffectSounds.CCTVActive);
    }

    public void SetPlayerTransform(Transform playerT){
        playerTransform = playerT;
    }

    void Update(){
        if(playerTransform != null){
            Vector3 targetDir = (playerTransform.position - headTransform.position).normalized;
            // 해당 방향으로의 회전을 생성합니다.
            Quaternion lookRotation = Quaternion.LookRotation(targetDir);
            
            // 현재 오브젝트의 회전에서 x축, y축만 변경하여 적용합니다.
            headTransform.rotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y, headTransform.rotation.eulerAngles.z);
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
