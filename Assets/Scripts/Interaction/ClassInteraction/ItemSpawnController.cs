using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemSpawnController : MonoBehaviour
{
    [SerializeField] InteractionPickupItem[] items;
    //[SerializeField] EventItemNames test;
    private void Awake()
    {
        EventDataManager.Instance.ItemController = this;
        int cnt = items.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (items[i] != null)
            {
                items[i].Init();
            }
        }
    }

    private void Start()
    {
        #region Except LastEvent
        int cnt = items.Length;
        if (EventDataManager.Instance.RingAfterSchoolBell)
        {
           
            for (int i = 0; i < cnt; i++)
            {
                if (items[i] != null)
                {
                    items[i].gameObject.SetActive(false);
                }
            }
            return;
        }
        #endregion
    }

    public void DecideActiveItemState(EventItemNames itemName, bool isActive, Vector3 position)
    {
        int itemIndex = (int)itemName;
        items[itemIndex].gameObject.SetActive(isActive);
        if (isActive)
            items[itemIndex].gameObject.transform.position = position;
    }

    public InteractionPickupItem GetItemInteraction(EventItemNames itemName)
    {
        int itemIndex = (int)itemName;
        return items[itemIndex];
    }
}
