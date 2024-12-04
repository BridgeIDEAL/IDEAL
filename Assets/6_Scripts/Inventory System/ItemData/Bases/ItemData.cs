using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    [상속 구조]

    ItemData(abstract)
        - CountableItemData(abstract)
            - PortionItemData
        - EquipmentItemData(abstract)
            - WeaponItemData
            - ArmorItemData

*/
public abstract class ItemData : ScriptableObject
{
    public int ID => _id;
    public string Name => _name;
    public string Tooltip => _tooltip;
    public Sprite IconSprite => _iconSprite;

    [SerializeField] private int      _id;
    [SerializeField] private string   _name;    // 아이템 이름
    [SerializeField] private string   _tooltip; // 아이템 설명
    [SerializeField] private Sprite   _iconSprite; // 아이템 아이콘

    /// <summary> 타입에 맞는 새로운 아이템 생성 </summary>
    public abstract Item CreateItem();
}
