using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenSetting : MonoBehaviour
{
    public TMP_Dropdown screenModeDropdown;

    void Start()
    {
        // 드롭다운 옵션 설정
        screenModeDropdown.options.Clear();
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Fullscreen" });
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Borderless Window" });
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Windowed" });

        // 초기 값 설정
        Debug.Log("Start Screen: " + Screen.fullScreenMode);
        screenModeDropdown.value = (int)Screen.fullScreenMode;
        screenModeDropdown.RefreshShownValue();

        // 이벤트 리스너 추가
        screenModeDropdown.onValueChanged.AddListener(delegate { OnScreenModeChange(screenModeDropdown.value); });
    }

    public void OnScreenModeChange(int mode)
    {
        switch (mode)
        {
            case 0:
                Debug.Log("Fullscreen");
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Debug.Log("Borderless");
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow; // Borderless mode 설정
                break;
            case 2:
                Debug.Log("Window");
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
        }
    }
}
