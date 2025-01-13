using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSky : MonoBehaviour
{
    public float rotationSpeed=1f;
    [SerializeField] Light[] dirLights;
    [SerializeField] Color whiteColor;
    [SerializeField] Color redColor;
    void Awake()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 0f);    
    }
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);    
    }

    public void SetLightColor()
    {
        int cnt = dirLights.Length;
        Color setColor; 
        if (EventDataManager.Instance.RingAfterSchoolBell)
            setColor = redColor;
        else
            setColor = whiteColor;
        for (int i=0; i<cnt; i++)
        {
            dirLights[i].color = setColor;
        }
    }
}
