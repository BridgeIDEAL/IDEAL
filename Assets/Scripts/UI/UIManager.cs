using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
        }
        else if(Input.GetKeyUp(KeyCode.Tab)){
            UIActives[(int)UIType.InventoryUI] = false;
            uIInventory.HideHighlightAllSlot();
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
