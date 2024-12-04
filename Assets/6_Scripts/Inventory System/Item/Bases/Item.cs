using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    [상속 구조]
    Item : 기본 아이템
        - EquipmentItem : 장비 아이템
        - CountableItem : 수량이 존재하는 아이템
*/
public abstract class Item
{
    public ItemData Data { get; private set;}

    public Item(ItemData data) => Data = data;
}


public interface IUsableItem {
    // 아이템 사용 : 성공여부 리턴
    bool Use();
}

public interface INotUseInventoryUI{
    // InventoryUI에서 사용 불가능한지 표시해주는 용도
}