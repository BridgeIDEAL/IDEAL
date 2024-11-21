using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamfeatureController : MonoBehaviour
{
    [SerializeField] uint steamAppId = 3263940;
    [SerializeField] string testAchievementID;

    private static SteamfeatureController instance = null;
    public static SteamfeatureController Instance { get { return instance; } private set { instance = value; } }

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
    }

    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(steamAppId);
            PrintSteamName();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }

    void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
    #endregion

    /// <summary>
    /// Use for Test
    /// </summary>
    public void PrintSteamName()
    {
        Debug.Log(Steamworks.SteamClient.Name);
    }

    public void UnlockAchievement(string _ID)
    {
        var achievement = new Steamworks.Data.Achievement(_ID);
        if (achievement.State == true)
        {
            Debug.Log("이미 클리어한 업적입니다.");
            return;
        }
        achievement.Trigger();
    }

    private void OnGUI()
    {
#if UNITY_EDITOR
       if(GUI.Button(new Rect(0,0,50,50), "업적 테스트")){
            UnlockAchievement(testAchievementID);
       }
#endif
    }
}
