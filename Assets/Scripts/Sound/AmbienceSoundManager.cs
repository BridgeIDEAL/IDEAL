using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IdealArea{
    Outside,
    Inside
}

public class AmbienceSoundManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource outsideAudioSource;
    [SerializeField] private AudioSource insideAudioSource;
    [SerializeField] private float triggerX = 6.8f;

    private Coroutine audioCoroutine;
    private IdealArea currentArea = IdealArea.Outside;

    [SerializeField] private float insideAudioVolume;
    [SerializeField] private float outsideAudioVolume;
    [SerializeField] private float soundFadeTime = 0.7f;

    void Awake(){
        outsideAudioSource.volume = outsideAudioVolume;
        insideAudioSource.volume = insideAudioVolume;
        outsideAudioSource.Play();
    }
    
    void Update()
    {
        if(playerTransform.localPosition.x > triggerX && currentArea == IdealArea.Outside){
            if(audioCoroutine != null){
                StopCoroutine(audioCoroutine);
            }
            audioCoroutine = StartCoroutine(InsideSoundCoroutine());
            currentArea = IdealArea.Inside;
        }
        
        if(playerTransform.localPosition.x < triggerX && currentArea == IdealArea.Inside){
            if(audioCoroutine != null){
                StopCoroutine(audioCoroutine);
            }
            audioCoroutine = StartCoroutine(OutsideSoundCoroutine());
            currentArea = IdealArea.Outside;
        }
    }

    private IEnumerator InsideSoundCoroutine(){
        float outsideVol = outsideAudioSource.volume;
        insideAudioSource.volume = 0.0f;
        insideAudioSource.Play();
        float stepTimer = 0.0f;
        while(stepTimer <= soundFadeTime){
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, 0.0f, stepTimer / soundFadeTime);
            insideAudioSource.volume = Mathf.Lerp(0.0f, insideAudioVolume, stepTimer / soundFadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        outsideAudioSource.Stop();
    }

    private IEnumerator OutsideSoundCoroutine(){
        float insideVol = insideAudioSource.volume;
        outsideAudioSource.volume = 0.0f;
        outsideAudioSource.Play();
        float stepTimer = 0.0f;
        while(stepTimer <= soundFadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, 0.0f, stepTimer / soundFadeTime);
            outsideAudioSource.volume = Mathf.Lerp(0.0f, outsideAudioVolume, stepTimer / soundFadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        insideAudioSource.Stop();
    }
}
