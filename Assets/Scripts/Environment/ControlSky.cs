using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSky : MonoBehaviour
{
    public float rotationSpeed=1f;
    void Awake()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 0f);    
    }
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);    
    }
}
