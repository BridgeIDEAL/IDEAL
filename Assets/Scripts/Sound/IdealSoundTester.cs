using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdealSoundTester : MonoBehaviour
{
    
    [SerializeField]private AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            audioSource.loop = false;
            audioSource.Play();
        }
        if(Input.GetKeyDown(KeyCode.L)){
            audioSource.loop = true;
            audioSource.Play();
        }
        if(Input.GetKeyDown(KeyCode.J)){
            audioSource.Stop();
        }
    }

    
}
