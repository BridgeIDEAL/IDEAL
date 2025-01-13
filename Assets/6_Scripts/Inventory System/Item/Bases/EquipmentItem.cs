using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItem : Item
{
    public EquipmentItemData EquipmentData {get; private set;}


    /// <summary> 현재 내구도 </summary>
    private int _durability;

    public int Durability{
        get => _durability;
        set{
            if(value < 0) value = 0;
            if(value > EquipmentData.MaxDurability){
                value = EquipmentData.MaxDurability;
            }

            _durability = value;
        }
    }

    public EquipmentItem(EquipmentItemData data) : base(data){
        EquipmentData = data;
        Durability = data.MaxDurability;
    }
}