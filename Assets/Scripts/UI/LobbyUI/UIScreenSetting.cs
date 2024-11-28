using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenSetting : MonoBehaviour
{
    public TMP_Dropdown screenModeDropdown;
    public TMP_Dropdown resolutionModeDropdown;
    [SerializeField] private Button closeBookBtn;

    private Resolution[] availableResolutions;

    void Start()
    {
        // 화면 모드 드롭다운 옵션 설정
        screenModeDropdown.options.Clear();
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "전체화면 모드" });
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "테두리없는 창모드" });
        screenModeDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "창모드" });

        switch (SettingDataManager.Instance.playerSettingData.screenMode)
        {
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

        // 해상도 드롭다운 옵션 설정 (인덱스가 큰 것부터 상단에 표시되도록 역순으로 추가)
        resolutionModeDropdown.options.Clear();
        availableResolutions = Screen.resolutions;
        int savedResolutionIndex = 0;
        for (int i = availableResolutions.Length - 1; i >= 0; i--)
        {
            Resolution res = availableResolutions[i];
            resolutionModeDropdown.options.Add(new TMP_Dropdown.OptionData { text = res.width + " x " + res.height + " @ " + res.refreshRate + "Hz" });

            // 저장된 해상도와 일치하는지 확인
            if (res.width == SettingDataManager.Instance.playerSettingData.resolutionWidth &&
                res.height == SettingDataManager.Instance.playerSettingData.resolutionHeight &&
                res.refreshRate == SettingDataManager.Instance.playerSettingData.resolutionRefreshRate)
            {
                savedResolutionIndex = availableResolutions.Length - 1 - i;
            }
        }
        resolutionModeDropdown.value = savedResolutionIndex; // 저장된 해상도를 선택하거나 기본적으로 가장 높은 해상도를 선택
        resolutionModeDropdown.RefreshShownValue();

        // 화면 모드 변경 이벤트 리스너 추가
        screenModeDropdown.onValueChanged.AddListener(delegate { OnScreenModeChange(screenModeDropdown.value); });
        // 해상도 변경 이벤트 리스너 추가
        resolutionModeDropdown.onValueChanged.AddListener(delegate { OnResolutionModeChange(resolutionModeDropdown.value); });
    }

    public void OnScreenModeChange(int mode)
    {
        SettingDataManager.Instance.SetScreenMode(mode);
    }

    public void OnResolutionModeChange(int index)
    {
        // 역순으로 추가했으므로 인덱스 변환 필요
        int actualIndex = availableResolutions.Length - 1 - index;
        if (actualIndex >= 0 && actualIndex < availableResolutions.Length)
        {
            Resolution selectedResolution = availableResolutions[actualIndex];
            SettingDataManager.Instance.SetResolution(selectedResolution);
        }
        else
        {
            Debug.LogError("Invalid resolution index selected!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            closeBookBtn?.onClick.Invoke();
        }
    }
}
