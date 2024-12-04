using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CareerDevelopSound : MonoBehaviour
{
    [SerializeField] private AudioSource careerSound;

    public void PlayAudio(){
        careerSound.Play();
    }
}
