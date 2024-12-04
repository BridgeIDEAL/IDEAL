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
    public int resolutionWidth = -1;
    public int resolutionHeight = -1;
    public int resolutionRefreshRate = -1;
    public float cameraRotationSpeed = 1.0f;
    public float brightness = 0.0f;
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
    private Coroutine audioMixerCoroutine = null;
    private float audioMixerCoroutineTime = 0.59f;

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
       
    }

    void Start(){
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
        idealAudioMixer.SetFloat("GameOver", playerSettingData.sfxVolume);

        // Window Setting Data Apply
        Resolution savedResolution = new Resolution();
        savedResolution.width = playerSettingData.resolutionWidth;
        savedResolution.height = playerSettingData.resolutionHeight;
        savedResolution.refreshRate = playerSettingData.resolutionRefreshRate;

        if(CheckUsableResolution(savedResolution)){
            // Have Saved Resolution
            Screen.SetResolution(savedResolution.width, savedResolution.height, playerSettingData.screenMode, savedResolution.refreshRate);
        }
        else{
            if(Screen.resolutions.Length > 0){
                // No Saved Resolution
                Resolution defaultResolution = Screen.resolutions[Screen.resolutions.Length - 1];
                SetResolution(defaultResolution);
            }
            else{
                // if No Enable Resolution
                Screen.fullScreenMode = playerSettingData.screenMode;
            }
        }

        // Setting Camera Speed Didn't Apply Yet
        // SettingDataManager는 Lobby에서 생성되므로 Prototype이나 Prototype_Second에 갈때마다 적용시켜줘야함

        // Brightness Aplly
        IdealSceneManager.Instance.SetPostExposure(playerSettingData.brightness);
    }

    public void ApplyCameraRotationSpeed(){
        if(IdealSceneManager.Instance.CurrentGameManager != null){
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.cameraRotationSpeed = playerSettingData.cameraRotationSpeed;
            Debug.Log("Camera Rotation Speed Applied: " + IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.cameraRotationSpeed);
        }
        else{
            Debug.LogError("GameManager is not exist");
        }
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
                idealAudioMixer.SetFloat("GameOver", Mathf.Log10(volume) * 20);
                playerSettingData.sfxVolume = Mathf.Log10(volume) * 20;
                break;
            default:
                Debug.LogError("mode num out of boundary");
                return;
        }
        SavePlayerSettingData();
    }

    public void SetCameraRotationSpeed(float speed){
        playerSettingData.cameraRotationSpeed = speed;
        ApplyCameraRotationSpeed();
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

    public void SetResolution(Resolution resolution){
        if(CheckUsableResolution(resolution)){
            Screen.SetResolution(resolution.width, resolution.height, playerSettingData.screenMode, resolution.refreshRate);
            playerSettingData.resolutionWidth = resolution.width;
            playerSettingData.resolutionHeight = resolution.height;
            playerSettingData.resolutionRefreshRate = resolution.refreshRate;
            Debug.Log("Resolution changed to: " + resolution.width + " x " + resolution.height + " @ " + resolution.refreshRate + "Hz");
            SavePlayerSettingData();
        }
        else{
            Debug.LogError("Invalid Resolution Change!!!");
        }
    }

    private bool CheckUsableResolution(Resolution resolution){
        Resolution[] resolutions = Screen.resolutions;
        foreach(Resolution res in resolutions){
            if(res.width == resolution.width && res.height == resolution.height && res.refreshRate == resolution.refreshRate){
                return true;
            }
        }
        Debug.LogError("Saved Resolution Invalid");
        return false;
    }

    public void SetBrightness(float brightnessValue){
        playerSettingData.brightness = brightnessValue;
        IdealSceneManager.Instance.SetPostExposure(brightnessValue);
        SavePlayerSettingData();
    }

    public void LateCloseSFX(){
        Invoke("CloseSFX", 1.5f);
    }

    private void CloseSFX(){
        if(audioMixerCoroutine != null){
            StopCoroutine(audioMixerCoroutine);
        }
        audioMixerCoroutine = StartCoroutine(CloseSFXCoroutine());
    }

    IEnumerator CloseSFXCoroutine(){

        float stepTimer = 0.0f;
        while(stepTimer < audioMixerCoroutineTime){
            stepTimer += Time.deltaTime;
            idealAudioMixer.SetFloat("SFX", Mathf.Lerp(playerSettingData.sfxVolume, -80.0f, stepTimer / audioMixerCoroutineTime));
            yield return null;
        }
    }

    public void OpenSFX(){
        if(audioMixerCoroutine != null){
            StopCoroutine(audioMixerCoroutine);
        }
        audioMixerCoroutine = StartCoroutine(OpenSFXCoroutine());
    }

    IEnumerator OpenSFXCoroutine(){
        float stepTimer = 0.0f;
        float currentVolume = -80.0f;
        idealAudioMixer.GetFloat("SFX", out currentVolume);
        while(stepTimer < audioMixerCoroutineTime){
            stepTimer += Time.deltaTime;
            idealAudioMixer.SetFloat("SFX", Mathf.Lerp(currentVolume, playerSettingData.sfxVolume, stepTimer / audioMixerCoroutineTime));
            yield return null;
        }
    }
}
