using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum TempEffectSounds { PaperTurn, KeyGet, PillGet, ItemGet, CCTVActive, WarningSiren}

public class TempEffectSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClip;

    public void PlayEffectSound(TempEffectSounds _EffectSound)
    {
        audioSource.Stop();
        audioSource.clip = audioClip[(int)_EffectSound];
        audioSource.Play();
    }

    public void PlayEffectSound(TempEffectSounds _EffectSound, float startTime)
    {
        audioSource.Stop();
        audioSource.clip = audioClip[(int)_EffectSound];
        audioSource.time = startTime;
        audioSource.Play();
    }

    public void StopEffectSound(){
        audioSource.Stop();
    }

    public void FadeStopEffectSound(float fadeTime){
        StartCoroutine(FadeOutEffectSound(fadeTime));
    }

    private IEnumerator FadeOutEffectSound(float fadeTime){
        float startVolume = audioSource.volume;
        float stepTimer = 0.0f;
        while(stepTimer < fadeTime){
            audioSource.volume = Mathf.Lerp(startVolume, 0.0f, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
        // 소리 크기 원상 복구
        audioSource.volume = startVolume;
    }
}
