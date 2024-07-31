using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clips;

    public void PlayOneShot(int _index)
    {
        int cnt = clips.Length;
        if (cnt - 1 > _index)
            return;
        source.PlayOneShot(clips[_index]);
    }
}
