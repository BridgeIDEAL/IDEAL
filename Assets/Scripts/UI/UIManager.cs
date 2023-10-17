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
    [SerializeField]
    private GameObject[] Canvases;

    private bool[] UIActives = new bool[System.Enum.GetValues(typeof(UIType)).Length];
    
    [SerializeField]
    private UIInteraction uIInteraction;

    void Awake() {
        for(int i = 0; i < Canvases.Length; i++){
            Canvases[i].SetActive(false);
        }
        for(int i = 0; i < UIActives.Length; i++){
            UIActives[i] = false;
        }

        SetUIActive(UIType.InteractionUI, true);
        DeleteInteractionText();

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
