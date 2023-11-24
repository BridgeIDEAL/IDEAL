using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 장비 - 손전등 아이템 </summary>
public class FlashlightItem : EquipmentItem, IUsableItem
{
    public FlashlightItem(FlashlightItemData data) : base(data) { }

    public bool Use(){
        if(EquipmentManager.Instance.GetHandActive(true)
            && EquipmentManager.Instance.GetEquipedItem(true) == null){
            EquipmentManager.Instance.EquipItem(true, this);
            return true;
        }
        if( EquipmentManager.Instance.GetHandActive(false)
            && EquipmentManager.Instance.GetEquipedItem(false) == null){
            EquipmentManager.Instance.EquipItem(false, this);
            return true;
        }
        return false;
    }
}
