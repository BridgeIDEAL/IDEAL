using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipmentSlot : MonoBehaviour
{
    [SerializeField] private GameObject iconGameObject;
    private Image iconImage;
    public Item currentItem = null;

    public bool HasItem => iconImage.sprite != null;

    public bool IsLeftSlot;

    private void Awake(){
        iconImage = iconGameObject.GetComponent<Image>();
        iconImage.sprite = null;
    }
    public void EquipItem(Item item){
        currentItem = item;
        iconImage.sprite = item.Data.IconSprite;
        iconGameObject.SetActive(true);
    }

    public void RemoveItem(){
        currentItem = null;
        iconImage.sprite = null;
        iconGameObject.SetActive(false);
    }
}
