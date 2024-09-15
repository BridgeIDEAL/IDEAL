using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeCheatKey : MonoBehaviour
{
    bool canGetMasterKey = true;
    public InteractionItemData masterKeyData;
    // Update is called once per frame

    private void Start()
    {
        if (Inventory.Instance.FindItemIndex(Inventory.MasterMey) != -1)
            canGetMasterKey = false;
        else
            canGetMasterKey = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && canGetMasterKey)
        {
            canGetMasterKey = false;
            Inventory.Instance.Add(masterKeyData, 1);
            ProgressManager.Instance.SetItemLog(masterKeyData.ID, 1);
        }
    }
}
