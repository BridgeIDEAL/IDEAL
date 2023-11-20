using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public enum UIType{
    IngameUI = 0,
    InteractionUI,
    MapUI,
    InventoryUI,
    SettingUI,
    MoveSettingUI
}

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;

    public static UIManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }
    
    
    
    [SerializeField]
    private GameObject[] Canvases;

    private bool[] UIActives = new bool[System.Enum.GetValues(typeof(UIType)).Length];

    [SerializeField]
    private FirstPersonController firstPersonController;
    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    [SerializeField]
    private UIInteraction uIInteraction;

    [SerializeField]
    private UIInventory uIInventory;


    void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(this.gameObject);
        }
        
        for(int i = 0; i < Canvases.Length; i++){
            // Canvas 별 해당 Canvas가 꺼져 있더라도 Awake 작업을 해야하는 경우가 있으므로
            // Awake 부분을 따로 함수로 작동해주어도 Canvas가 꺼져 있으면 제대로 Init함수가 작동하지 않는 경우 발생
            Canvases[i].SetActive(true);
            Canvases[i].SetActive(false);
        }
        for(int i = 0; i < UIActives.Length; i++){
            UIActives[i] = false;
        }

        SetUIActive(UIType.InteractionUI, true);
        DeleteInteractionText();

    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.F1)){
            UIActives[(int)UIType.MoveSettingUI] = !UIActives[(int)UIType.MoveSettingUI];
            SetUIActive(UIType.MoveSettingUI, UIActives[(int)UIType.MoveSettingUI]);
            Cursor.lockState = UIActives[(int)UIType.MoveSettingUI] ? CursorLockMode.None : CursorLockMode.Locked;
        }

        // Inventory UI 관련 코드
        if(Input.GetKeyDown(KeyCode.Tab)){
            UIActives[(int)UIType.InventoryUI] = true;
            firstPersonController.CameraRotationLock = true;
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
        }
        else if(Input.GetKeyUp(KeyCode.Tab)){
            UIActives[(int)UIType.InventoryUI] = false;
            firstPersonController.CameraRotationLock = false;
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.3f;
            uIInventory.HideHighlightAllSlot();

            ActivationLogManager.Instance.InActiveActivationLog();
        }
        SetUIActive(UIType.InventoryUI, UIActives[(int)UIType.InventoryUI]);
        Cursor.lockState = UIActives[(int)UIType.InventoryUI] ? CursorLockMode.None : CursorLockMode.Locked;


    }

    public void SetUIActive(UIType uIType, bool active){
        Canvases[(int)uIType].SetActive(active);
        UIActives[(int)uIType] = active;
    }


    public void PrintInteractionText(string textContents){
        uIInteraction.SetTextContents(textContents);
        uIInteraction.SetTextActive(true);
    }

    public void DeleteInteractionText(){
        uIInteraction.SetTextContents(null);
        uIInteraction.SetTextActive(false);
    }

}
