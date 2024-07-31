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
}
