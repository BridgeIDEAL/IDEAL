using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamfeatureManager : MonoBehaviour
{
    [SerializeField] uint steamAppId = 3263940;

    private static SteamfeatureManager instance = null;
    public static SteamfeatureManager Instance { get { return instance; } private set { instance = value; } }
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

    public void PrintSteamName()
    {
        Debug.Log(Steamworks.SteamClient.Name);
    }
}
