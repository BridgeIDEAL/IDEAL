using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Stovemanager : MonoBehaviour
{
    [SerializeField]
    private PlatformSetting platformSetting;
    static Stovemanager instance = null;
    public Stovemanager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        // ...
        if (platformSetting.platformType == PlatformType.Stove)
        {
            StoveAchievementHandler.UnlockAchievement("STAT_ID");
        }
    }
}
