using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMap : MonoBehaviour
{
    [SerializeField] private GameObject mapUIObject;
    private int mapItemCode = 1106;
    void Update()
    {
        if(Inventory.Instance.FindItemIndex(mapItemCode) == -1){
            mapUIObject.SetActive(false);
        }
        else{
            mapUIObject.SetActive(true);
        }
    }
}
