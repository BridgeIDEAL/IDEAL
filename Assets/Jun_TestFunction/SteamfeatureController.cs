using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamfeatureController : MonoBehaviour
{
    bool isConnectSteam = false;
    const uint steamAppId = 3263940;
    [SerializeField] string testAchievementID = "ACHIEV_01";

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

    void Update()
    {
        if(isConnectSteam)
            Steamworks.SteamClient.RunCallbacks();    
    }

    void OnApplicationQuit()
    {
        if(isConnectSteam)
            Steamworks.SteamClient.Shutdown();
    }
    #endregion

    public void LockAchievement(string _ID)
    {
        if (Steamworks.SteamClient.IsValid)
        {
            var achievement = new Steamworks.Data.Achievement(_ID);
            achievement.Clear();
        }
    }

    public void UnLockAchievement(string _ID)
    {
        if (Steamworks.SteamClient.IsValid)
        {
            var achievement = new Steamworks.Data.Achievement(_ID);
            if (achievement.State == true)
                return;
            achievement.Trigger(true);
        }
    }

    /// <summary>
    /// 업적 테스트
    /// </summary>
//    private void OnGUI()
//    {
//#if UNITY_EDITOR
//        if (GUI.Button(new Rect(0,0,50,50), "업적 해제")){
//            UnLockAchievement(testAchievementID);
//        }
//        if (GUI.Button(new Rect(50, 0, 50, 50), "업적 잠금"))
//        {
//            LockAchievement(testAchievementID);
//        }
//#endif
//    }
}
