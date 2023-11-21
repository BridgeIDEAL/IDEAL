using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting;

public class UIInventory : MonoBehaviour
{
    /***********************************************************************
    *                               Option Fields
    ***********************************************************************/
    #region Option Fields
    private int verticalSlotCount = 32;

    [Header("Options")]
    [SerializeField] private bool showTooltip = true;
    [SerializeField] private bool showHighlight =true;

    
    [Header("Connected Objects")]
    [SerializeField] private RectTransform slotArea;
    [SerializeField] private GameObject slotUIPrefab;
    [SerializeField] private UIItemTooltip itemTooltip;
    

    [Space(8)]
    [SerializeField] private bool mouseReversed = false; // 마우스 클릭 반전 여부
    
    #endregion

    
    /***********************************************************************
    *                               Private Fields
    ***********************************************************************/

    #region Private Fields
    
    /// <summary> 연결된 인벤토리 </summary>
    private Inventory inventory;

    private List<UIItemSlot> slotUIList = new List<UIItemSlot>();
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> rrList;

    private UIItemSlot pointerOverSlot;    // 현재 포인터가 위치한 곳의 슬롯


    private int leftClick = 0;
    private int rightClick = 1;


    #endregion

    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    #region Unity Events

    private void Awake(){
        Init();
        InitSlots();
        
    }

    private void Update(){
        pointerEventData.position = Input.mousePosition;

        OnPointerEnterAndExit();
        if(showTooltip) ShowOrHideItemTooltip();
        OnPointerDown();
    }

    #endregion

    /***********************************************************************
    *                               Init Methods
    ***********************************************************************/
    #region Init Methods
    private void Init(){
        TryGetComponent(out graphicRaycaster);
        if(graphicRaycaster == null){
            graphicRaycaster = gameObject.AddComponent<GraphicRaycaster>();
        }

        // Graphic Raycaster
        pointerEventData = new PointerEventData(EventSystem.current);
        rrList = new List<RaycastResult>(10);

        // Item Tooltip UI
        if(itemTooltip == null){
            itemTooltip = GetComponentInChildren<UIItemTooltip>();
            Debug.Log("인스펙터에서 아이템 툴팁 UI를 직접 지정하지 않아 자식에서 발견하여 초기화하였습니다.");
        }
    }


    /// <summary> 지정된 개수만큼 슬롯을 동적 생성 </summary>
    private void InitSlots(){

        slotUIList = new List<UIItemSlot>(verticalSlotCount);



        for(int i = 0; i < verticalSlotCount; i++){
            int slotIndex = i;

            var slotRectTranform = CloneSlot();
            slotRectTranform.gameObject.SetActive(false);
            slotRectTranform.gameObject.name = $"Item Slot [{slotIndex}]";

            var slotUI = slotRectTranform.GetComponent<UIItemSlot>();
            slotUI.SetSlotIndex(slotIndex);
            slotUIList.Add(slotUI);
        }
    }

    private RectTransform CloneSlot(){
        GameObject slotGameObject = Instantiate(slotUIPrefab);
        RectTransform rt = slotGameObject.GetComponent<RectTransform>();
        rt.SetParent(slotArea);
        
        return rt;
    }

    #endregion

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

     /// <summary> 슬롯에 포인터가 올라가는 경우, 슬롯에서 포인터가 빠져나가는 경우 </summary>
    private void OnPointerEnterAndExit(){
        // 이전 프레임의 슬롯
        var prevSlot = pointerOverSlot;

        // 현재 프레임의 슬롯
        var curSlot = pointerOverSlot = RaycastAndGetFirstComponent<UIItemSlot>();

        if (prevSlot == null){
            // Enter
            if (curSlot != null){
                OnCurrentEnter();
            }
        }
        else{
            // Exit
            if (curSlot == null){
                OnPrevExit();
            }
            // Change
            else if (prevSlot != curSlot){
                OnPrevExit();
                OnCurrentEnter();
            }
        }

        // ===================== Local Methods ===============================
        void OnCurrentEnter()
        {
            if(showHighlight){
                curSlot.Highlight(true);
            }
        }
        void OnPrevExit()
        {
            prevSlot.Highlight(false);
        }
    }

    /// <summary> 아이템 정보 툴팁 보여주거나 감추기 </summary>
    private void ShowOrHideItemTooltip(){
        // 마우스가 유효한 아이템 아이콘 위에 올라와 있다면 툴팁 보여주기
        bool isValid =
            pointerOverSlot != null && pointerOverSlot.HasItem && pointerOverSlot.IsAccessible;

        if (isValid){
            UpdateTooltipUI(pointerOverSlot);
            itemTooltip.Show();
        }
        else {
            itemTooltip.Hide();
        }
    }

    /// <summary> 슬롯에 클릭하는 경우 </summary>
    private void OnPointerDown(){
        // Drag 기능은 구현하지 않음
        // Right Click : Use Item
        if(Input.GetMouseButtonDown(rightClick)){
            UIItemSlot slot = RaycastAndGetFirstComponent<UIItemSlot>();

            if(slot != null && slot.HasItem && slot.IsAccessible){
                TryUseItem(slot.Index);
            }
        }
    }


    #endregion

    /***********************************************************************
    *                               Private Methods
    ***********************************************************************/
    #region Private Methods
    /// <summary> UI 및 인벤토리에서 아이템 제거 </summary>
    private void TryRemoveItem(int index){
        inventory.Remove(index);
    }

    /// <summary> 아이템 사용 </summary>
    private void TryUseItem(int index){
        inventory.Use(index);
    }

    /// <summary> 툴팁 UI의 슬롯 데이터 갱신 </summary>
    private void UpdateTooltipUI(UIItemSlot slot){
        if(!slot.IsAccessible || !slot.HasItem){
            return;
        }

        // 툴팁 정보 갱신
        itemTooltip.SetItemInfo(inventory.GetItemData(slot.Index));

        // 툴팁 위치 조정
        itemTooltip.SetRectPosition();
    }

    #endregion

    /***********************************************************************
    *                               Public Methods
    ***********************************************************************/
    #region Public Methods
    /// <summary> 인벤토리 참조 등록 (인벤토리에서 직접 호출) </summary>
    public void SetInventoryReference(Inventory _inventory){
        inventory = _inventory;
    }

    /// <summary> 마우스 클릭 좌우 반전시키기 (true : 반전) </summary>
    public void InvertMouse(bool value){
        leftClick = value ? 1 : 0;
        rightClick = value ? 0 : 1;

        mouseReversed = value;
    }

    /// <summary> 슬롯에 아이템 아이콘 등록 </summary>
    public void SetItemIcon(int index, Sprite icon){
        slotUIList[index].SetItem(icon);
    }

    /// <summary> 슬롯에 아이템 이름 등록 </summary>
    public void SetItemName(int index, string name){
        slotUIList[index].SetItemName(name);
    }

    /// <summary> 해당 슬롯의 아이템 개수 텍스트 지정 </summary>
    public void SetItemAmountText(int index, int amount){
        // NOTE : amount가 1 이하일 경우 텍스트 미표시
        slotUIList[index].SetItemAmount(amount);
    }

    /// <summary> 해당 슬롯의 아이템 개수 텍스트 숨기기 </summary>
    public void HideItemAmountText(int index){
        slotUIList[index].SetItemAmount(1);
    }

    /// <summary> 슬롯에서 아이템 아이콘 제거, 개수 텍스트 숨기기 </summary>
    public void RemoveItem(int index){
        slotUIList[index].RemoveItem();
    }

    /// <summary> 슬롯 오브젝트 활성화/비활성화 </summary>
    public void SetActiveSlot(int index, bool active){
        slotUIList[index].gameObject.SetActive(active);
    }

    /// <summary> 하이라이트가 켜져있는 슬롯 UI 하이라이트 끄기 </summary>
    public void HideHighlightAllSlot(){
        for(int i = 0; i < verticalSlotCount; i++){
            slotUIList[i].HideHighlight();
        }
    }
    

    #endregion

}
