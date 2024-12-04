using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Flashlight_", menuName = "Inventory System/Item Data/Flashlight", order = 2)]
public class FlashlightItemData : EquipmentItemData
{
    /// <summary> 손전등 빛 세기 </summary>
    [SerializeField] private int _intensity = 10;
    public int Intensity => _intensity;

    public override Item CreateItem(){
        return new FlashlightItem(this);
    }
}
