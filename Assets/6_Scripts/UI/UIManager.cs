using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private AudioSource inventoryUISound;

    private bool[] UIActives = new bool[System.Enum.GetValues(typeof(UIType)).Length];

    // private FirstPersonController firstPersonController;
    private ThirdPersonController thirdPersonController;
    public bool CameraShake_Lock = false;
    
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
        
        // 아래 F1으로 속도 조절하는 코드는 전처리기를 통해 유니티 에디터 에서만 실행가능
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.F1)){
            UIActives[(int)UIType.MoveSettingUI] = !UIActives[(int)UIType.MoveSettingUI];
            SetUIActive(UIType.MoveSettingUI, UIActives[(int)UIType.MoveSettingUI]);
        }
        #endif

        // Inventory UI 관련 코드
        // Map과 Dialogue가 활성화 되어 있지 않은 경우에만 Inventory UI 활성화
        if(Input.GetKeyDown(KeyCode.Tab) 
        && !UIActives[(int)UIType.MapUI] && !isDialogueActive && !UIActives[(int)UIType.SettingUI]){
            UIActives[(int)UIType.InventoryUI] = true;
            isInventoryActive = true;
            inventoryUISound.Play();
            ProgressManager.Instance.TurnOffCheckListIcon();
        }
        else if(Input.GetKeyUp(KeyCode.Tab)){
            UIActives[(int)UIType.InventoryUI] = false;
            isInventoryActive = false;

            uIInventory.HideHighlightAllSlot();

            // ActivationLogManager.Instance.InActiveActivationLog();
        }
        SetUIActive(UIType.InventoryUI, UIActives[(int)UIType.InventoryUI]);

        // M 누른 경우 Map 활성화 비활성화
        // Inventory, Dialogue, 설정창이 활성화 되어 있지 않은 경우에만 Map UI 활성화
        if(Input.GetKeyDown(KeyCode.M) 
        && (Inventory.Instance.FindItemIndex(mapItemCode) != -1 || Inventory.Instance.FindItemIndex(mapPieceItemCode) != -1)
        && !UIActives[(int)UIType.InventoryUI] && !isDialogueActive && !UIActives[(int)UIType.SettingUI])
        {
            UIActives[(int)UIType.MapUI] = !UIActives[(int)UIType.MapUI];
            SetUIActive(UIType.MapUI, UIActives[(int)UIType.MapUI]);
            if(UIActives[(int)UIType.MapUI]) {
                uIMap.ActiveMap();
            }
            else{
                InActiveMapUI();
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            UIActives[(int)UIType.SettingUI] = !UIActives[(int)UIType.SettingUI];
            SetUIActive(UIType.SettingUI, UIActives[(int)UIType.SettingUI]);
            if(UIActives[(int)UIType.SettingUI]){
                CloseInventoryAndMapUI();
            }
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
            
            CameraShake_Lock = true;
            // 상호작용 텍스트 비활성화
            uIInteraction.SetTextActive(false);
        }
        else{
            thirdPersonController.CameraRotationLock = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            CameraShake_Lock = false;
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

    public void InActiveMapUI(){
        UIActives[(int)UIType.MapUI] = false;
        SetUIActive(UIType.MapUI, UIActives[(int)UIType.MapUI]);
    }

    public void InActiveGuideBookUI(){
        UIActives[(int)UIType.GuideBookUI] = false;
        SetUIActive(UIType.GuideBookUI, UIActives[(int)UIType.GuideBookUI]);
        thirdPersonController.MoveLock = false;
    }

    public void OnActiveDialogue(){
        isDialogueActive = true;
        CloseInventoryAndMapUI();
    }

    private void CloseInventoryAndMapUI(){
        // MapUI와 Inventory가 켜져 있는 경우 비활성화
        // MapUI 처리
        if(UIActives[(int)UIType.MapUI]){
            InActiveMapUI();
        }

        // Inventory 처리
        if(UIActives[(int)UIType.InventoryUI]){
            UIActives[(int)UIType.InventoryUI] = false;
            isInventoryActive = false;

            uIInventory.HideHighlightAllSlot();
            SetUIActive(UIType.InventoryUI, UIActives[(int)UIType.InventoryUI]);
        }
    }

    public void OnJumpScare(){
        uIInputLock = true;
        // MapUI, Inventory, Setting이 켜져 있는 경우 비활성화
        // MapUI 처리
        if(UIActives[(int)UIType.MapUI]){
            InActiveMapUI();
        }

        // Inventory 처리
        if(UIActives[(int)UIType.InventoryUI]){
            UIActives[(int)UIType.InventoryUI] = false;
            isInventoryActive = false;

            uIInventory.HideHighlightAllSlot();
            SetUIActive(UIType.InventoryUI, UIActives[(int)UIType.InventoryUI]);
        }

        // Setting 처리
        if(UIActives[(int)UIType.SettingUI]){
            UIActives[(int)UIType.SettingUI] = false;
            SetUIActive(UIType.SettingUI, UIActives[(int)UIType.SettingUI]);
        }
    }
}
