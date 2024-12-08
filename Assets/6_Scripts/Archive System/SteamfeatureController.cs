using Stove.PCSDK.NET;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamfeatureController : MonoBehaviour
{
    [SerializeField]  PlatformType platformType;

    bool isConnectStove = false;
    bool isConnectSteam = false;
    const uint steamAppId = 3263940;
    //[SerializeField] string testAchievementID = "ACHIEV_02";

    private static SteamfeatureController instance = null;
    public static SteamfeatureController Instance { get { return instance; } private set { instance = value; } }

    [SerializeField] SteamfeatureManager featureManager; 
    public SteamfeatureManager FeatureManager { get { return featureManager; }  private set { featureManager = value; } }

    #region Unity Life Cycle
    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        if (featureManager == null)
        {
            featureManager = GetComponent<SteamfeatureManager>();
        }

        StovePCResult result = StovePC.GetUser();
        if (result == StovePCResult.NoError)
        {
            isConnectStove = true;
        } 
    }

    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(steamAppId);
            isConnectSteam = true;
            if (Steamworks.SteamClient.IsValid)
            {
                isConnectSteam = true;
            }
            else
            {
                isConnectSteam = false;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e );
        }
    }

    void FixedUpdate()
    {
        if(isConnectSteam && platformType == PlatformType.Steam)
            Steamworks.SteamClient.RunCallbacks();    
    }

    void OnApplicationQuit()
    {
        if(isConnectSteam && platformType == PlatformType.Steam)
            Steamworks.SteamClient.Shutdown();
    }
    #endregion

    public void LockAchievement(string _ID)
    {
        if (Steamworks.SteamClient.IsValid && platformType == PlatformType.Steam)
        {
            var achievement = new Steamworks.Data.Achievement(_ID);
            achievement.Clear();
            return;
        }

        //_ID += "_1";
        StovePCResult result = StovePC.GetAchievement(_ID);
        if (result == StovePCResult.NoError && isConnectStove && platformType == PlatformType.Stove)
        {
            StoveAchievementHandler.UnlockAchievement(_ID);
        }
    }

    public void UnLockAchievement(string _ID)
    {
        if (Steamworks.SteamClient.IsValid && platformType == PlatformType.Steam)
        {
            var achievement = new Steamworks.Data.Achievement(_ID);
            if (achievement.State == true)
                return;
            achievement.Trigger(true);
            return;
        }

        _ID += "_1";
        StovePCResult result = StovePC.GetAchievement(_ID);
        if (result == StovePCResult.NoError && isConnectStove && platformType == PlatformType.Stove)
        {
            StoveAchievementHandler.UnlockAchievement(_ID);
        }
    }

    /// <summary>
    /// 업적 테스트
    /// </summary>
//    private void OnGUI()
//    {
//        if (GUI.Button(new Rect(0, 0, 50, 50), "업적 해제"))
//        {
//            UnLockAchievement(testAchievementID);
//        }
//        if (GUI.Button(new Rect(50, 0, 50, 50), "업적 잠금"))
//        {
//            LockAchievement(testAchievementID);
//        }

//#if UNITY_EDITOR

//#endif
//    }
}
