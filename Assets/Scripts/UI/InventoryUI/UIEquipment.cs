using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEquipment : MonoBehaviour
{
    [SerializeField] private UIEquipmentSlot leftEquipmentSlot;
    [SerializeField] private UIEquipmentSlot rightEquipmentSlot;

    private UIEquipmentSlot pointerOverSlot;

    [SerializeField] private UIItemTooltip itemTooltip;

    [SerializeField] private Image pillImage;
    private float pillTime = 1.0f;

    private Coroutine pillCoroutine;

    private int leftClick = 0;
    private int rightClick = 1;


    public void EquipItem(bool isLeft, Item item){
        if(isLeft){
            leftEquipmentSlot.EquipItem(item);
        }
        else{
            rightEquipmentSlot.EquipItem(item);
        }
    }

    public void RemoveItem(bool isLeft){
        if(isLeft) leftEquipmentSlot.RemoveItem();
        else rightEquipmentSlot.RemoveItem();
    }

    public void SetSlotActive(bool isLeft, bool active){
        if(isLeft){
            leftEquipmentSlot.SetSlotActive(active);
        }
        else{
            rightEquipmentSlot.SetSlotActive(active);
        }
    }


    /***********************************************************************
    *                               Unity Event Methods
    ***********************************************************************/
    #region Unity Event Methods
    public void GameUpdate(){

        ShowOrHideItemTooltip();
        OnPointerDown();
        OnPointerUp();
    }
    #endregion

    /***********************************************************************
    *                               Mouse Event Methods
    ***********************************************************************/

    #region Mouse Event Methods

    /// <summary> 아이템 정보 툴팁 보여주거나 감추기 </summary>
    private void ShowOrHideItemTooltip(){
        pointerOverSlot = IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIRayCaster.RaycastAndGetFirstComponent<UIEquipmentSlot>();

        // 마우스가 유효한 아이템 아이콘 위에 올라와 있다면 툴팁 보여주기
        bool isValid =
            pointerOverSlot != null && pointerOverSlot.HasItem;

        if (isValid){
            UpdateTooltipUI(pointerOverSlot);
            itemTooltip.Show();
        }
    }

    /// <summary> 툴팁 UI의 슬롯 데이터 갱신 </summary>
    private void UpdateTooltipUI(UIEquipmentSlot slot){
        if(!slot.HasItem){
            return;
        }
        // 툴팁 정보 갱신
        itemTooltip.SetItemInfo(slot.currentItem.Data);

        // 툴팁 위치 조정
        itemTooltip.SetRectPosition();
    }

    /// <summary> 슬롯에 클릭하는 경우 </summary>
    private void OnPointerDown(){
        // Drag 기능은 구현하지 않음
        // Right Click : Detach(탈착)
        // if(Input.GetMouseButtonDown(rightClick)){
        //     UIEquipmentSlot slot = IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIRayCaster.RaycastAndGetFirstComponent<UIEquipmentSlot>();

        //     if(slot != null && slot.HasItem){
        //         EquipmentManager.Instance.DetachEquipedItem(slot.IsLeftSlot);
        //     }
        // }

        // Left Click: 사용 상호작용
        if(Input.GetMouseButtonDown(leftClick)){
            UIEquipmentSlot slot = IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIRayCaster.RaycastAndGetFirstComponent<UIEquipmentSlot>();

            if(slot != null && slot.HasItem){
                if(pillCoroutine != null){
                    pillCoroutine = StartCoroutine(UseInteractionCoroutine(slot.currentItem));
                }
            }
        }
    }

    private void OnPointerUp(){
        if(Input.GetMouseButtonUp(leftClick)){
            StopCoroutine(pillCoroutine);
            pillImage.fillAmount = 0.0f;
        }
    }

    private IEnumerator UseInteractionCoroutine(Item item){
        float stepTimer = 0.0f;
        while(stepTimer <= pillTime){
            pillImage.fillAmount = stepTimer / pillTime;
            stepTimer += Time.deltaTime;
            yield return null;
        }

        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.ActivePillUI(true);
    }


    #endregion
}
