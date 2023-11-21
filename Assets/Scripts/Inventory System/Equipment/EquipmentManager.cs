using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private static EquipmentManager instance = null;
    public static EquipmentManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    [SerializeField] UIEquipment uIEquipment;
    private Item leftHandItem = null;
    private Item rightHandItem = null;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }
    
    public void EquipItem(bool isLeft, Item item){
        if(isLeft){
            leftHandItem = item;
        }
        else{
            rightHandItem = item;
        }
        uIEquipment.EquipItem(isLeft, item);
    }

    public Item GetEquipedItem(bool isLeft){
        return isLeft ? leftHandItem : rightHandItem;
    }

    public void RemoveEquipedItem(bool isLeft){
        if(isLeft){
            leftHandItem = null;
            uIEquipment.RemoveItem(isLeft);
        }
        else{
            rightHandItem = null;
            uIEquipment.RemoveItem(isLeft);
        }
    }

    public void DetachEquipedItem(bool isLeft){
        if(isLeft){
            if(leftHandItem == null) return;
            Inventory.Instance.Add(leftHandItem.Data);
        }
        else{
            if(rightHandItem == null) return;
            Inventory.Instance.Add(rightHandItem.Data);
        }

        RemoveEquipedItem(isLeft);
    }


    // 장비 착용에 따른 작동도 필요
}
