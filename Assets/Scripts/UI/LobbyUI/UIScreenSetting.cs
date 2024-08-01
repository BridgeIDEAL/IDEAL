using System.Runtime.InteropServices;
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
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "전체화면 모드" });
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "테두리없는 창모드" });
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "창모드" });



        Debug.Log("Start Screen: " + SettingDataManager.Instance.playerSettingData.screenMode);
        switch(SettingDataManager.Instance.playerSettingData.screenMode){
            case FullScreenMode.ExclusiveFullScreen:
                screenModeDropdown.value = 0;
                break;
            case FullScreenMode.FullScreenWindow:
                screenModeDropdown.value = 1;
                break;
            case FullScreenMode.Windowed:
                screenModeDropdown.value = 2;
                break;
            default:
                Debug.LogError("Not allowed Screen mode!");
                break;
        }
        screenModeDropdown.RefreshShownValue();

        // 이벤트 리스너 추가
        screenModeDropdown.onValueChanged.AddListener(delegate { OnScreenModeChange(screenModeDropdown.value); });
    }

    public void OnScreenModeChange(int mode)
    {
        SettingDataManager.Instance.SetScreenMode(mode);
    }
}
