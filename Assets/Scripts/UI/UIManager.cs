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
    private ScriptHub scriptHub;
    
    [SerializeField]
    private GameObject[] Canvases;

    private bool[] UIActives = new bool[System.Enum.GetValues(typeof(UIType)).Length];

    private FirstPersonController firstPersonController;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    private UIInteraction uIInteraction;

    private UIInventory uIInventory;

    private UIIngame uIIngame;

    private bool isDialogueActive = false;
    public bool IsDialogueActive {
        get {return isDialogueActive;}
        set { isDialogueActive = value;}
    }
    private bool isInventoryActive = false;
    public bool IsInventoryActive {
        get{return isInventoryActive;}
        set{isInventoryActive = value;}
    }


    public void Init() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }

        firstPersonController = scriptHub.firstPersonController;
        cinemachineVirtualCamera = scriptHub.cinemachineVirtualCamera;
        uIInteraction = scriptHub.uIInteraction;
        uIInventory = scriptHub.uIInventory;
        uIIngame = scriptHub.uIIngame;
        
        StartCoroutine(ActivateCanvasCoroutine());

        for(int i = 0; i < UIActives.Length; i++){
            UIActives[i] = false;
        }

    }

    private IEnumerator ActivateCanvasCoroutine(){
        // Canvas 별 해당 Canvas가 꺼져 있더라도 Awake 작업을 해야하는 경우가 있으므로
        // Awake 부분을 따로 함수로 작동해주어도 Canvas가 꺼져 있으면 제대로 Init함수가 작동하지 않는 경우 발생
        for(int i = 0; i < Canvases.Length; i++){
            Canvases[i].SetActive(true);
        }
        yield return null;
        for(int i = 0; i < Canvases.Length; i++){
            Canvases[i].SetActive(false);
        }

        SetUIActive(UIType.InteractionUI, true);
        DeleteInteractionText();

        SetUIActive(UIType.IngameUI, true);
        uIIngame.SetVisualFilter(0.0f);

        LoadingImageManager.Instance.DeleteLoadingCanvas();
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
            isInventoryActive = true;
        }
        else if(Input.GetKeyUp(KeyCode.Tab)){
            UIActives[(int)UIType.InventoryUI] = false;
            isInventoryActive = false;

            uIInventory.HideHighlightAllSlot();

            ActivationLogManager.Instance.InActiveActivationLog();
        }
        SetUIActive(UIType.InventoryUI, UIActives[(int)UIType.InventoryUI]);

        UpdateMouseLock();
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

    private void UpdateMouseLock(){
        // CameraLock & MouseUnLock이 필요한 경우
        if(isDialogueActive || isInventoryActive){
            firstPersonController.CameraRotationLock = true;
            Cursor.lockState = CursorLockMode.None;
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
            // 상호작용 텍스트 비활성화
            uIInteraction.SetTextActive(false);
        }
        else{
            firstPersonController.CameraRotationLock = false;
            Cursor.lockState = CursorLockMode.Locked;
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.3f;
            // 상호작용 텍스트 활성화
            uIInteraction.SetTextActive(true);
        }
    }
}
