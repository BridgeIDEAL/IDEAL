using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    /***********************************************************************
    *                               Public Properties
    ***********************************************************************/
    #region Public Properties
    /// <summary> 아이템 수용 한도 </summary>
    public int Capacity { get; private set; }

    // Singleton 사용하기 위한 변수
    public static Inventory Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    #endregion

    /***********************************************************************
    *                               Private Fields
    ***********************************************************************/
    #region Private Fields

    // Singleton 사용하기 위한 변수
    private static Inventory instance = null;

    // 초기 수용 한도
    [SerializeField, Range(8, 32)]
    private int initalCapacity = 32;

    // 최대 수용 한도(아이템 배열 크기)
    [SerializeField, Range(8, 32)]
    private int maxCapacity = 32;

    
    public ScriptHub scriptHub;
    private UIInventory uIInventory; // 연결된 인벤토리 UI

    private TempEffectSound playerTempEffectSound;

    /// <summary> 아이템 목록 </summary>
    [SerializeField]
    private Item[] items;

    /// <summary> 합성되는 조각 아이템 목록 </summary>
    private int[] keyPieces_SecondFloor = {20101, 20102, 20103};
    private int[] keyPieces_StudentRoom = {99101, 99102, 99103};
    private int[] keyPieces_ServerRoom = {99201, 99202, 99203};
    private int[] mapPieces = {99001, 99002, 99003};

    /// <summary> 합성을 통해 생성되는 아이템 목록 </summary>
    [SerializeField] private ItemData secondFloorKeyItem;
    [SerializeField] private ItemData studentRoomKeyItem;
    [SerializeField] private ItemData serverRoomKeyItem;
    [SerializeField] private ItemData mapItem;

    /// <summary > 체크리스트 충족하는 아이템 목록 </summary>
    private Dictionary<int, int> checkListItemDic = new Dictionary<int, int>{
        {99001, 102},
        {401, 103},
        {308, 105},
        {903, 106},
        {20101, 109},
        {20102, 110},
        {20103, 111},
        {201, 108},
        {15, 204},
        {202, 205},
        {99101, 207},
        {99102, 208},
        {99103, 209},
        {991, 206},
        {994, 301},
        {901, 302},
        {102, 303},
        {99201, 305},
        {99202, 306},
        {99203, 307},
        {992, 304},
        {993, 308},
        
    };

    private int[] check3rdGradeRooms = {301, 303, 306};

    /// <summary>  업데이트 할 인덱스 목록 </summary>
    private readonly HashSet<int> indexSetForUpdate = new HashSet<int>();

    /// <summary> 아이템 데이터 타입별 정렬 가중치 </summary>
    private readonly static Dictionary<Type, int> sortWeightDict = new Dictionary<Type, int>{
        {typeof(PortionItemData), 10000},
        {typeof(FlashlightItemData), 20000},
        {typeof(InteractionItemData), 30000}
    };

    private class ItemComparer : IComparer<Item>{
        public int Compare(Item a, Item b){
            return (a.Data.ID + sortWeightDict[a.Data.GetType()]) - (b.Data.ID + sortWeightDict[b.Data.GetType()]);
        }
    }

    private static readonly ItemComparer itemComparer = new ItemComparer();

    #endregion

    /***********************************************************************
    *                       Unity Events -> Game Manager
    ***********************************************************************/
    #region Unity Events
    public void Init(){
        // Singleton 할당
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }


        items = new Item[maxCapacity];
        Capacity = initalCapacity;
    }

    public void EnterAnotherSceneInit(bool isLobby){
        if(isLobby){
            items = new Item[maxCapacity];
        }
        else{
            uIInventory = scriptHub.uIInventory;
            playerTempEffectSound = scriptHub.playerEffectSound;
            uIInventory.SetInventoryReference(this);
            SortAll();  // uIInventory에 Inventory 업데이트
        }
    }

    public void GameStart(){
        UpdateAccessibleStatesAll();
    }

    #endregion

    /***********************************************************************
    *                               Private Methods
    ***********************************************************************/
    #region Private Methods
    
    /// <summary> 인덱스가 수용 범위 내에 있는지 검사 </summary>
    private bool IsValidIndex(int index){
        return index >= 0 && index < Capacity;
    }

    /// <summary> 앞에서부터 비어있는 슬롯 인덱스 탐색 </summary>
    private int FindEmptySlotIndex(int startIndex = 0){
        for(int i = startIndex; i < Capacity; i++){
            if(items[i] == null){
                return i;
            }
        }
        return -1;
    }

    /// <summary> 앞에서부터 개수 여유가 있는 Countable 아이템의 슬롯 인덱스 탐색 </summary>
    private int FindCountableItemSlotIndex(CountableItemData target, int startIndex = 0){
        for(int i = startIndex; i < Capacity; i++){
            Item curItem = items[i];
            if(curItem == null) 
                continue;
            
            // 아이템 종류가 일치하는지 개수에 여유가 있는지 확인
            if(curItem.Data == target && curItem is CountableItem ci){
                if(!ci.IsMax){
                    return i;
                }
            }
        }

        return -1;
    }


    /// <summary> 해당하는 인덱스의 슬롯 상태 및 UI 갱신 </summary>
    private void UpdateSlot(int index){
        if(!IsValidIndex(index)) return;

        Item item = items[index];

        // 1. 아이템이 슬롯에 존재하는 경우
        if(item != null){
            // 해당 슬롯 활성화
            uIInventory.SetActiveSlot(index, true);

            // 아이콘, 이름 등록
            uIInventory.SetItemIcon(index, item.Data.IconSprite);
            uIInventory.SetItemName(index, item.Data.Name);

            // 인벤토리 UI 에서 사용 불가능한 아이템 처리해주기
            if(item is INotUseInventoryUI){
                uIInventory.SetItemAccessibleState(index, false);
            }
            else{
                uIInventory.SetItemAccessibleState(index, true);
            }

            // 1-1. 셀 수 있는 아이템
            if(item is CountableItem ci){
                // 1-1-1. 수량이 0인 경우, 아이템 제거
                if(ci.IsEmpty){
                    items[index] = null;
                    RemoveSlot();
                    return;
                }
                // 1-1-2. 수량 텍스트 표시
                else{
                    uIInventory.SetItemAmountText(index, ci.Amount);
                }
            }
            // 1-2. 셀 수 없는 아이템인 경우 수량 텍스트 제거
            else{
                uIInventory.HideItemAmountText(index);
            }
        }
        // 2. 빈 슬롯인 경우 : 아이콘 제거
        else{
            RemoveSlot();
        }

        void RemoveSlot(){
            uIInventory.RemoveItem(index);
            uIInventory.HideItemAmountText(index);
            uIInventory.SetActiveSlot(index, false);
        }
    }

    /// <summary> 해당하는 인덱스 슬롯들의 상태 및 UI 갱신 </summary>
    private void UpdateSlot(params int[] indices){
        foreach(var i in indices){
            UpdateSlot(i);
        }
    }

    /// <summary> 모든 슬롯들의 상태를 UI에 갱신 </summary>
    private void UpdateAllSlot(){
        for(int i = 0; i < Capacity; i++){
            UpdateSlot(i);
        }
    }

    /// <summary> 조각 아이템들이 충분히 모였는지 체크하여 온전품으로 변환 </summary>
    private void CheckPieceItems(){
        CheckPieceItem(secondFloorKeyItem, keyPieces_SecondFloor);
        CheckPieceItem(studentRoomKeyItem, keyPieces_StudentRoom);
        CheckPieceItem(serverRoomKeyItem, keyPieces_ServerRoom);
        CheckPieceItem(mapItem, mapPieces);
    }

    private void CheckPieceItem(ItemData completeItemData, int[] itemPieces){
        bool allPieces = true;
        foreach(var piece in itemPieces){
            if(FindItemIndex(piece) == -1){
                allPieces = false;
                break;
            }
        }
        if(allPieces){
            foreach(var piece in itemPieces){
                UseItemWithItemCode(piece);
            }
            Add(completeItemData, 1);
        }
    }

    private void CheckCheckList(int itemCode){
        if(checkListItemDic.ContainsKey(itemCode)){
            ProgressManager.Instance.UpdateCheckList(checkListItemDic[itemCode], 1);
        }
        bool Allitems = true;
        foreach (int itemC in check3rdGradeRooms){
            if(FindItemIndex(itemC) == -1){
                Allitems = false;
            }
        }
        if(Allitems){
            ProgressManager.Instance.UpdateCheckList(107, 1);
        }
    }

    #endregion


    /***********************************************************************
    *                               Check & Getter Methods
    ***********************************************************************/
    #region Check & Getter Methods

    /// <summary> 해당 슬롯이 아이템을 갖고 있는지 여부 </summary>
    public bool HasItem(int index){
        return IsValidIndex(index) && items[index] != null;
    }

    /// <summary> 해당 슬롯이 셀 수 있는 아이템인지 여부 </summary>
    public bool IsCountableItem(int index){
        return HasItem(index) && items[index] is CountableItem;
    }

    /// <summary>
    /// 해당 슬롯의 현재 아이템 개수 리턴
    /// <para/>  - 잘못된 인덱스 : -1 리턴
    /// <para/> - 빈 슬롯 : 0 리턴
    /// <para> - 셀 수 없는 아이템: 1 리턴
    /// </summary>
    public int GetCurrentAmount(int index){
        if(!IsValidIndex(index)) return -1;
        if(items[index] == null) return 0;

        CountableItem ci = items[index] as CountableItem;
        if(ci == null){
            return 1;
        }

        return ci.Amount;
    }


    /// <summary> 해당 슬롯의 아이템 정보 리턴 </summary>
    public ItemData GetItemData(int index){
        if(!IsValidIndex(index)) return null;
        if(items[index] == null) return null;

        return items[index].Data;
    }

    /// <summary> 해당 슬롯의 아이템 이름 리턴 </summary>
    public string GetItemName(int index){
        if(!IsValidIndex(index)) return "";
        if(items[index] == null) return "";

        return items[index].Data.Name;
    }

    #endregion

    /***********************************************************************
    *                               Public Methods
    ***********************************************************************/
    #region Public Methods
    /// <summary> 인벤토리 UI 연결 </summary>
    public void  ConnectUI(UIInventory _uIInventory){
        uIInventory = _uIInventory;
        uIInventory.SetInventoryReference(this);
    }

    /// <summary>
    /// <para/> 넣는데 실패하면 넣지 못한 아이템 개수 리턴
    /// <para/> 모두 성공했다면 리턴 0
    /// </summary>
    public int Add(ItemData itemData, int amount = 1){
        int amount_ = amount;
        int index;

        // 1. 수량이 있는 아이템
        if(itemData is CountableItemData ciData){
            bool findNextCountable = true;
            index = -1;

            while(amount > 0){
                // 1-1. 이미 해당 아이템이 인벤토리 내에 존재하고 max가 아닌지 검사
                if(findNextCountable){
                    index = FindCountableItemSlotIndex(ciData, index + 1);

                    // 개수가 여유 있는 기존재 슬롯이 더이상 없다면 빈 슬롯 탐색 시작
                    if(index == -1){
                        findNextCountable = false;
                    }
                    // 기존재 슬롯을 찾은 경우, 양 증가시키고 초과량 존재 시 amount에 초기화
                    else{
                        CountableItem ci = items[index] as CountableItem;
                        amount = ci.AddAmountAndGetExcess(amount);

                        UpdateSlot(index);
                    }
                }
                // 1-2. 빈 슬롯 탐색
                else{
                    index = FindEmptySlotIndex(index + 1);

                    // 빈 슬롯도 없는 경우 종료
                    if(index == -1){
                        break;
                    }
                    // 빈 슬롯 발견 시, 슬롯에 아이템 추가 및 잉여량 계산
                    else{
                        // 새로운 아이템 생성
                        CountableItem ci = ciData.CreateItem() as CountableItem;
                        ci.SetAmount(amount);

                        // 슬롯에 추가
                        items[index] = ci;

                        // 남은 개수 계산
                        amount = (amount > ciData.MaxAmount) ?(amount - ciData.MaxAmount) : 0;

                        UpdateSlot(index);
                    }
                }
            }
        }
        // 2. 수량이 없는 아이템
        else{
            // 2-1. 1개만 넣는 경우
            if(amount == 1){
                index = FindEmptySlotIndex();
                if(index != -1){
                    // 아이템을 생성하여 슬롯에 추가
                    items[index] = itemData.CreateItem();
                    amount = 0;

                    UpdateSlot(index);
                }
            }

            // 2-2. 2개 이상의 수량 없는 아이템을 동시에 추가하는 경우
            index = -1;
            for(; amount > 0; amount--){
                // 아이템 넣은 다음 인덱스부터 슬롯 탐색
                index = FindEmptySlotIndex(index + 1);

                // 다 넣지 못한 경우 루프 종료
                if(index == -1){
                    break;
                }

                // 아이템을 생성하여 슬롯에 추가
                items[index] = itemData.CreateItem();

                UpdateSlot(index);
            }
        }


        SortAll();
        if(amount == 0){
            GetItemSound(itemData);
            //ActivationLogManager.Instance.AddActivationLogWithItem(itemData.ID, true);
            ProgressManager.Instance.SetItemLog(itemData.ID, amount_);
            CheckPieceItems();
            CheckCheckList(itemData.ID);
            if(itemData.ID == 99001) ActiveInteraction.Instance.Active_01F_MapGuide(true);
        }
        return amount;
    }

    private void GetItemSound(ItemData itemData){
        if (itemData.ID == 1105)
            playerTempEffectSound.PlayEffectSound(TempEffectSounds.PillGet);
        else if (itemData.ID == 1103 || itemData.ID == 1102 || itemData.ID == 1101)
            playerTempEffectSound.PlayEffectSound(TempEffectSounds.KeyGet);
        else {
            playerTempEffectSound.PlayEffectSound(TempEffectSounds.ItemGet);
        }
    }

    /// <summary> 해당 슬롯의 아이템 제거 </summary>
    public void Remove(int index){
        if(!IsValidIndex(index)) return;

        items[index] = null;
        uIInventory.RemoveItem(index);
        SortAll();
    }

    // ---------------- Swap 기능은 사용하지 않음
    // ---------------- Separate 기능도 사용하지 않음

    /// <summary> 해당 슬롯의 아이템 사용 </summary>
    public void Use(int index){
        if(!IsValidIndex(index)) return;
        if(items[index] == null) return;

        // 사용 가능한 아이템인 경우
        if(items[index] is IUsableItem uItem){
            // 아이템 사용
            bool succeeded = uItem.Use();

            if(succeeded){
                //ActivationLogManager.Instance.AddActivationLogWithItem(items[index].Data.ID, false);
                if(!(items[index] is CountableItem)){
                    Remove(index);
                }

                UpdateSlot(index);
            }
        }
        SortAll();
    }

    /// <summary> 모든 슬롯 UI에 접근 가능 여부 업데이트 </summary>
    public void UpdateAccessibleStatesAll(){
        // TO DO
        // uIInventory.SetAccessibleSlotRange(Capacity);
    }

    /// <summary> 빈 슬롯 없이 앞에서부터 채우기 </summary>
    public void TrimAll(){
        // 배열 빈공간 채우기

        // i 커서와 j 커서
        // i 커서 : 가장 앞에 있는 빈칸을 찾는 커서
        // j 커서 : i 커서 위치에서부터 뒤로 이동하며 기존재 아이템을 찾는 커서

        // i커서가 빈칸을 찾으면 j 커서는 i+1 위치부터 탐색
        // j커서가 아이템을 찾으면 아이템을 옮기고, i 커서는 i+1 위치로 이동
        // j커서가 Capacity에 도달하면 루프 즉시 종료

        indexSetForUpdate.Clear();

        int i = -1;
        while(items[++i] != null);
        int j = i;

        while(true){
            while(++j < Capacity && items[j] == null);

            if(j == Capacity){
                break;
            }

            indexSetForUpdate.Add(i);
            indexSetForUpdate.Add(j);

            items[i] = items[j];
            items[j] = null;
            i++;
        }

        foreach( var index in indexSetForUpdate){
            UpdateSlot(index);
        }
    }


    /// <summary> 빈 슬롯 없이 채우면서 아이템 종류별로 정렬하기 </summary>
    public void SortAll(){
        // 1. Trim
        int i = -1;
        while(items[++i] != null) ;
        int j = i;

        while(true){
            while(++j < Capacity && items[j] == null) ;

            if(j == Capacity){
                break;
            }

            items[i] = items[j];
            items[j] = null;
            i++;
        }

        // 2. Sort
        Array.Sort(items, 0, i, itemComparer);
        
        // 3. Update
        UpdateAllSlot();
    }


    /// <summary> itemcode와 일치하는 item이 slots에 있다면 index, 없다면 -1 return </summary>
    public int FindItemIndex(int _itemcode){
        for(int i = 0; i < Capacity; i++){
            if(items[i] == null) break;
            if(items[i].Data.ID == _itemcode){
                return i;
            }
        }
        return -1;
    }

    /// <summary> itemCode에 해당하는 아이템이 있다면 사용하고 true 반환, 없다면 false 반환 </summary>
    public bool UseItemWithItemCode(int _itemcode){
        int itemIndex = FindItemIndex(_itemcode);
        if(itemIndex == -1) return false;
        else {
            Use(itemIndex);
            return true;
        }
    }

    #endregion
}
