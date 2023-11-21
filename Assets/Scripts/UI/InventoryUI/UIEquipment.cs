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
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> rrList;

    [SerializeField] private UIItemTooltip itemTooltip;

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

    private void Awake(){
        TryGetComponent(out graphicRaycaster);
        if(graphicRaycaster == null){
            graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        }

        // Graphic Raycaster
        pointerEventData = new PointerEventData(EventSystem.current);
        rrList = new List<RaycastResult>(10);
    }
    private void Update(){
        pointerEventData.position = Input.mousePosition;

        ShowOrHideItemTooltip();
        OnPointerDown();
    }

    /***********************************************************************
    *                               Mouse Event Methods
    ***********************************************************************/

    #region Mouse Event Methods
    private bool IsOverUI()
            => EventSystem.current.IsPointerOverGameObject();

    /// <summary> 레이캐스트하여 얻은 첫 번째 UI에서 컴포넌트 찾아 리턴 </summary>
    private T RaycastAndGetFirstComponent<T>() where T : Component{
        rrList.Clear();

        graphicRaycaster.Raycast(pointerEventData, rrList);
        
        if(rrList.Count == 0)
            return null;

        return rrList[0].gameObject.GetComponent<T>();
    }

    /// <summary> 아이템 정보 툴팁 보여주거나 감추기 </summary>
    private void ShowOrHideItemTooltip(){
        pointerOverSlot = RaycastAndGetFirstComponent<UIEquipmentSlot>();
        
        // 마우스가 유효한 아이템 아이콘 위에 올라와 있다면 툴팁 보여주기
        bool isValid =
            pointerOverSlot != null && pointerOverSlot.HasItem;

        if (isValid){
            UpdateTooltipUI(pointerOverSlot);
            itemTooltip.Show();
        }
        else {
            itemTooltip.Hide();
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
        if(Input.GetMouseButtonDown(rightClick)){
            UIEquipmentSlot slot = RaycastAndGetFirstComponent<UIEquipmentSlot>();

            if(slot != null && slot.HasItem){
                EquipmentManager.Instance.DetachEquipedItem(slot.IsLeftSlot);
            }
        }
    }


    #endregion
}
