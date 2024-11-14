using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeCheatKey : MonoBehaviour
{
    bool canGetMasterKey = true;
    public InteractionItemData masterKeyData;
    // Update is called once per frame


    bool onceLast = true;
    private void Start()
    {
#if UNITY_EDITOR
        if (Inventory.Instance.FindItemIndex(Inventory.MasterMey) != -1)
            canGetMasterKey = false;
        else
            canGetMasterKey = true;
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Z) && canGetMasterKey)
        {
            canGetMasterKey = false;
            Inventory.Instance.Add(masterKeyData, 1);
            ProgressManager.Instance.SetItemLog(masterKeyData.ID, 1);
        }

        if(Input.GetKeyDown(KeyCode.V) && onceLast)
        {
            onceLast = false;
            EventDataManager.Instance.TriggerController.TriggerLastEvent();
        }
    }
#endif
}
