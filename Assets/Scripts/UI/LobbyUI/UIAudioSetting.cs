using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIAudioSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer idealAudioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [SerializeField] private TextMeshProUGUI masterVolumeText;
    [SerializeField] private TextMeshProUGUI bgmVolumeText;
    [SerializeField] private TextMeshProUGUI sfxVolumeText;

    private void Awake(){
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        // Initialize slider values based on AudioMixer settings
        float volume;
        
        if (idealAudioMixer.GetFloat("Master", out volume)) {
            masterVolumeSlider.value = Mathf.Pow(10, volume / 20);
            masterVolumeText.text = (masterVolumeSlider.value * 100).ToString("F0");
        }

        if (idealAudioMixer.GetFloat("BGM", out volume)) {
            bgmVolumeSlider.value = Mathf.Pow(10, volume / 20);
            bgmVolumeText.text = (bgmVolumeSlider.value * 100).ToString("F0");
        }

        if (idealAudioMixer.GetFloat("SFX", out volume)) {
            sfxVolumeSlider.value = Mathf.Pow(10, volume / 20);
            sfxVolumeText.text = (sfxVolumeSlider.value * 100).ToString("F0");
        }
    }

    public void SetMasterVolume(float volume){
        masterVolumeText.text = (volume * 100).ToString("F0");
        idealAudioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume){
        bgmVolumeText.text = (volume * 100).ToString("F0");
        idealAudioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume){
        sfxVolumeText.text = (volume * 100).ToString("F0");
        idealAudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
