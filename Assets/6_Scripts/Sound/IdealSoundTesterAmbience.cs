using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdealSoundTesterAmbience : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private GameObject defaultAmbienceObject;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N)){
            audioSource.loop = false;
            audioSource.Play();
        }
        if(Input.GetKeyDown(KeyCode.M)){
            audioSource.loop = true;
            audioSource.Play();
        }
        if(Input.GetKeyDown(KeyCode.B)){
            audioSource.Stop();
        }
        if(Input.GetKeyDown(KeyCode.V)){
            defaultAmbienceObject.SetActive(false);
        }
    }
}
