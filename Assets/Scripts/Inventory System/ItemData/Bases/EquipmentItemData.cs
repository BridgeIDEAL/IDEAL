using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItemData : ItemData
{
    /// <summary> 최대 내구도 </summary>
    [SerializeField] private int _maxDurability = 100;
    public int MaxDurability => _maxDurability;
}
