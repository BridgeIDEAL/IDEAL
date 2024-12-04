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

    public void StopEffectSound(){
        audioSource.Stop();
    }
}
