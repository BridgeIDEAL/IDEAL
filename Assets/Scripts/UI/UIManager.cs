using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum UIType{
    IngameUI = 0,
    InteractionUI,
    GuideBookUI,
    MapUI,
    InventoryUI,
    PillUI,
    SettingUI,
    MoveSettingUI
}

public class UIManager : MonoBehaviour
{
    
    [SerializeField]
    private ScriptHub scriptHub;
    
    [SerializeField]
    private GameObject[] Canvases;

    private bool[] UIActives = new bool[System.Enum.GetValues(typeof(UIType)).Length];

    // private FirstPersonController firstPersonController;
    private ThirdPersonController thirdPersonController;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    
    private UIInteraction uIInteraction;

    private UIInventory uIInventory;

    private UIIngame uIIngame;
    private UIMap uIMap;
    private UIGuideBook uIGuideBook;

    public bool uIInputLock = false;

    private int mapItemCode = 990;
    private int mapPieceItemCode = 99001;

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

        thirdPersonController = scriptHub.thirdPersonController;
        cinemachineVirtualCamera = scriptHub.cinemachineVirtualCamera;
        uIInteraction = scriptHub.uIInteraction;
        uIInventory = scriptHub.uIInventory;
        uIIngame = scriptHub.uIIngame;
        uIMap = scriptHub.uIMap;
        uIGuideBook = scriptHub.uIGuideBook;
        
        StartCoroutine(ActivateCanvasCoroutine());

        for(int i = 0; i < UIActives.Length; i++){
            UIActives[i] = false;
        }

        uIInventory.Init();
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
    }

    public void IngameFadeInEffect(){
        uIIngame.FadeInEffect();
    }

    public void GameUpdate(){
        if(uIInputLock){
            return;
        }
        
        if(Input.GetKeyDown(KeyCode.F1)){
            UIActives[(int)UIType.MoveSettingUI] = !UIActives[(int)UIType.MoveSettingUI];
            SetUIActive(UIType.MoveSettingUI, UIActives[(int)UIType.MoveSettingUI]);
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

            // ActivationLogManager.Instance.InActiveActivationLog();
        }
        SetUIActive(UIType.InventoryUI, UIActives[(int)UIType.InventoryUI]);

        if(Input.GetKeyDown(KeyCode.BackQuote) && (Inventory.Instance.FindItemIndex(mapItemCode) != -1 || Inventory.Instance.FindItemIndex(mapPieceItemCode) != -1)){    // ` 누른 경우 Map 활성화 비활성화
            UIActives[(int)UIType.MapUI] = !UIActives[(int)UIType.MapUI];
        }
        SetUIActive(UIType.MapUI, UIActives[(int)UIType.MapUI]);
        uIMap.ActiveMap();

        if(UIActives[(int)UIType.GuideBookUI] && Input.GetKeyDown(KeyCode.E)){    // 가이드북 보는 중에 E가 눌리는 경우
            UIActives[(int)UIType.GuideBookUI] = false;
            SetUIActive(UIType.GuideBookUI, UIActives[(int)UIType.GuideBookUI]);
            thirdPersonController.MoveLock = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            UIActives[(int)UIType.SettingUI] = !UIActives[(int)UIType.SettingUI];
            SetUIActive(UIType.SettingUI, UIActives[(int)UIType.SettingUI]);
            if(!isDialogueActive) thirdPersonController.MoveLock = UIActives[(int)UIType.SettingUI];
        }

        UpdateMouseLock();


        uIInventory.GameUpdate();
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
        if(isDialogueActive || isInventoryActive || UIActives[(int)UIType.GuideBookUI] || UIActives[(int)UIType.SettingUI] || UIActives[(int)UIType.MapUI]
            || UIActives[(int)UIType.MoveSettingUI]){
            thirdPersonController.CameraRotationLock = true;
            Cursor.lockState = CursorLockMode.None;
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.0f;
            // 상호작용 텍스트 비활성화
            uIInteraction.SetTextActive(false);
        }
        else{
            thirdPersonController.CameraRotationLock = false;
            Cursor.lockState = CursorLockMode.Locked;
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0.3f;
            // 상호작용 텍스트 활성화
            uIInteraction.SetTextActive(true);
        }
    }

    public void ActiveGuideBook(){
        UIActives[(int)UIType.GuideBookUI] = true;
        SetUIActive(UIType.GuideBookUI, UIActives[(int)UIType.GuideBookUI]);
        uIGuideBook.ActiveGuide();
        thirdPersonController.MoveLock = true;
    }

    public bool CanInteraction(){
        return !UIActives[(int)UIType.GuideBookUI] && !UIActives[(int)UIType.MapUI];
    }

    public void ActivePillUI(bool active){
        if(active){
            UIActives[(int)UIType.InventoryUI] = false;
            isInventoryActive = false;
            SetUIActive(UIType.InventoryUI, UIActives[(int)UIType.InventoryUI]);
            UIActives[(int)UIType.PillUI] = true;
            isDialogueActive = true;
            SetUIActive(UIType.PillUI, UIActives[(int)UIType.PillUI]);
        }
        else{
            UIActives[(int)UIType.PillUI] = false;
            isDialogueActive = false;
            SetUIActive(UIType.PillUI, UIActives[(int)UIType.PillUI]);
        }

    }
}
