using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAudioSetting : MonoBehaviour
{
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private TextMeshProUGUI masterVolumeText;
    [SerializeField] private TextMeshProUGUI bgmVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    private void Start(){
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);


        // Apply Slider & Text Value
        masterVolumeSlider.value = Mathf.Pow(10, SettingDataManager.Instance.playerSettingData.masterVolume / 20);
        masterVolumeText.text = (masterVolumeSlider.value * 100).ToString("F0");

        bgmVolumeSlider.value = Mathf.Pow(10, SettingDataManager.Instance.playerSettingData.bgmVolume / 20);
        bgmVolumeText.text = (bgmVolumeSlider.value * 100).ToString("F0");

        sfxVolumeSlider.value = Mathf.Pow(10, SettingDataManager.Instance.playerSettingData.sfxVolume / 20);
        sfxVolumeText.text = (sfxVolumeSlider.value * 100).ToString("F0");
    }

    public void SetMasterVolume(float volume){
        masterVolumeText.text = (volume * 100).ToString("F0");
        SettingDataManager.Instance.SetVolumeValue(0, volume);
    }

    public void SetBGMVolume(float volume){
        bgmVolumeText.text = (volume * 100).ToString("F0");
        SettingDataManager.Instance.SetVolumeValue(1, volume);
    }

    public void SetSFXVolume(float volume){
        sfxVolumeText.text = (volume * 100).ToString("F0");
        SettingDataManager.Instance.SetVolumeValue(2, volume);

    }
}
