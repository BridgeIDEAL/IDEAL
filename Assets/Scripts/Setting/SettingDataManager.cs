using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class PlayerSettingData{
    public FullScreenMode screenMode = FullScreenMode.ExclusiveFullScreen;  // 전체화면
    public float masterVolume = Mathf.Log10(0.5f) * 20;
    public float bgmVolume = Mathf.Log10(0.5f) * 20;
    public float sfxVolume = Mathf.Log10(0.5f) * 20;
}

public class SettingDataManager : MonoBehaviour
{
    private static SettingDataManager instance = null;
    public static SettingDataManager Instance {
        get {
            if(instance == null) return null;
            return instance;
        }
    }

    [SerializeField] private AudioMixer idealAudioMixer;

    private string playerSettingPath;
    public PlayerSettingData playerSettingData = null;

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }
        else{
            Destroy(this.gameObject);
        }

        playerSettingPath = Path.Combine(Application.persistentDataPath, "PlayerSettingData.json");

        // 저장된 파일 불러오기
        if (File.Exists(playerSettingPath)){
            LoadPlayerSettingData();
        }
        else{
            playerSettingData = new PlayerSettingData();
        }

        SavePlayerSettingData();        
    }

    private void LoadPlayerSettingData(){
        string loadJson = File.ReadAllText(playerSettingPath);
        playerSettingData = new PlayerSettingData();
        playerSettingData = JsonUtility.FromJson<PlayerSettingData>(loadJson);

        // Sound Setting Data Apply
        idealAudioMixer.SetFloat("Master", playerSettingData.masterVolume);
        idealAudioMixer.SetFloat("BGM", playerSettingData.bgmVolume);
        idealAudioMixer.SetFloat("SFX", playerSettingData.sfxVolume);

        // Window Setting Data Apply
        Screen.fullScreenMode = playerSettingData.screenMode;
    }

    private void SavePlayerSettingData(){
        string json = JsonUtility.ToJson(playerSettingData, true);
        File.WriteAllText(playerSettingPath, json);
    }

    public void SetVolumeValue(int mode, float volume){  // mode 0 == Master 1 == BGM 2 == SFX
        switch(mode){
            case 0:
                idealAudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
                playerSettingData.masterVolume = Mathf.Log10(volume) * 20;
                break;
            case 1:
                idealAudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
                playerSettingData.bgmVolume = Mathf.Log10(volume) * 20;
                break;
            case 2:
                idealAudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
                playerSettingData.sfxVolume = Mathf.Log10(volume) * 20;
                break;
            default:
                Debug.LogError("mode num out of boundary");
                return;
        }
        SavePlayerSettingData();
    }

    public void SetScreenMode(int mode){
        switch (mode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                playerSettingData.screenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow; // Borderless mode 설정
                playerSettingData.screenMode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                playerSettingData.screenMode = FullScreenMode.Windowed;
                break;
        }
        SavePlayerSettingData();
    }
}
