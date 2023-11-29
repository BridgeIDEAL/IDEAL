using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 상호작용 아이템 정보  </summary>
[CreateAssetMenu(fileName = "Item_Interaction_", menuName ="Inventory System/Item Data/Interaction", order = 4)]
public class InteractionItemData : CountableItemData
{
    public override Item CreateItem()
    {
        return new InteractionItem(this);
    }
}
