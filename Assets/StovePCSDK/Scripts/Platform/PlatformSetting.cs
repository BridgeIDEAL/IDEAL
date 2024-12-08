using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlatformSetting", menuName = "Platform/Platform Setting", order = 0)]
public class PlatformSetting : ScriptableObject
{

    public PlatformType platformType;

    private void OnEnable()
    {

    }
}
