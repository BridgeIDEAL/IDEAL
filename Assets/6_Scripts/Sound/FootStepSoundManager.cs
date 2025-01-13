using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FootStepSoundManager : MonoBehaviour
{
    // [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private ScriptHub scriptHub;
    private ThirdPersonController thirdPersonController;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footStepClips;
    private bool canSound = true;
    private float stepTimer = 0.0f;
    private float soundInterval = 0.6f;
    private int lastClip = -1;
    
    void Awake(){
        thirdPersonController = scriptHub.thirdPersonController;
    }
    void Update()
    {
        // 움직일 때 발걸음
        if(thirdPersonController._speed > 0.2f){
            if(canSound){
                canSound = false;
                SoundFootStep();
            }
        }
        // 사운드 간격 조정
        if(stepTimer >= soundInterval){
            stepTimer = 0.0f;
            canSound = true;
        }
        if(!canSound) stepTimer += Time.deltaTime;
    }

    private void SoundFootStep(){
        audioSource.Stop();
        // 가장 최근에 호출한 오디오 클립은 호출하지 않도록함
        // 이후에는 배치가 알맞은 애들끼리 조합시키는 방법 고안
        int curClip;
        do{
            curClip = Random.Range(1, footStepClips.Length);
        }while(lastClip == curClip);

        audioSource.clip = footStepClips[curClip];
        audioSource.Play();
        lastClip = curClip;
    }
}
