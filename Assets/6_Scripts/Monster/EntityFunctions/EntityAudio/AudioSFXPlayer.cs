using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSFXPlayer : MonoBehaviour
{
    [SerializeField] protected AudioSource source;
    [SerializeField] protected AudioClip[] clips;

    protected virtual void Awake()
    {
        if (source == null)
            source = GetComponent<AudioSource>();
    }

    public void SFXPlayOneShot(int _idx) 
    {
        source.PlayOneShot(clips[_idx]);
    }

    public void SFXPlay(int _idx)
    {
        source.Stop();
        source.clip = clips[_idx];
        source.Play();
    }
}
public enum CamSFXPlayerSoundType
{
    Damaged_Arm = 0,
    Damaged_Leg = 1,
    BoardErase = 2,
    TalkLineEnd = 3,
}