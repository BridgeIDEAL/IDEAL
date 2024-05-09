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
    [SerializeField] private float triggerZ = 8.8f;

    private Coroutine audioCoroutine;
    private IdealArea currentArea = IdealArea.Outside;

    [SerializeField] private float insideAudioVolume;
    [SerializeField] private float outsideAudioVolume;
    [SerializeField] private float soundFadeTime = 0.7f;

    void Awake(){
        outsideAudioSource.volume = outsideAudioVolume;
        insideAudioSource.volume = insideAudioVolume;
    }

    public void SoundFadeIn(bool isOutSide){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        if(isOutSide){
            audioCoroutine = StartCoroutine(OutsideSoundCoroutine(soundFadeTime*2));
        }
        else{
            audioCoroutine = StartCoroutine(InsideSoundCoroutine(soundFadeTime*2));
        }
    }

    
    void Update()
    {
        bool isOutSide = (playerTransform.localPosition.z < triggerZ && playerTransform.localPosition.x > 7.0f && playerTransform.localPosition.x < 18.5f)
                        || playerTransform.localPosition.z < -1.4f;
        if(!isOutSide && currentArea == IdealArea.Outside){
            if(audioCoroutine != null){
                StopCoroutine(audioCoroutine);
            }
            audioCoroutine = StartCoroutine(InsideSoundCoroutine(soundFadeTime));
            currentArea = IdealArea.Inside;
        }
        
        if(isOutSide && currentArea == IdealArea.Inside){
            if(audioCoroutine != null){
                StopCoroutine(audioCoroutine);
            }
            audioCoroutine = StartCoroutine(OutsideSoundCoroutine(soundFadeTime));
            currentArea = IdealArea.Outside;
        }
    }

    private IEnumerator InsideSoundCoroutine(float fadeTime){
        float outsideVol = outsideAudioSource.volume;
        insideAudioSource.volume = 0.0f;
        insideAudioSource.Play();
        float stepTimer = 0.0f;
        while(stepTimer <= fadeTime){
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, 0.0f, stepTimer / fadeTime);
            insideAudioSource.volume = Mathf.Lerp(0.0f, insideAudioVolume, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        outsideAudioSource.Stop();
    }

    private IEnumerator OutsideSoundCoroutine(float fadeTime){
        float insideVol = insideAudioSource.volume;
        outsideAudioSource.volume = 0.0f;
        outsideAudioSource.Play();
        float stepTimer = 0.0f;
        while(stepTimer <= fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, 0.0f, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(0.0f, outsideAudioVolume, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        insideAudioSource.Stop();
    }
}
