using System;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance;
    public SteamManager SteamManager;
    public StoveManager StoveManager;

    [SerializeField]
    public PlatformSetting platformSetting;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        switch (platformSetting.platformType)
        {
            case PlatformType.Steam:
                {
                    Debug.Log("Steam");
                    if (SteamManager != null) SteamManager.gameObject.SetActive(true);
                    if (StoveManager != null) StoveManager.gameObject.SetActive(false);
                    break;
                }

            case PlatformType.Stove:
                {
                    Debug.Log("Stove");
                    if (SteamManager != null) SteamManager.gameObject.SetActive(false);
                    if (StoveManager != null) StoveManager.gameObject.SetActive(true);
                    break;
                }

            default:
                break;
        }
    }
}
