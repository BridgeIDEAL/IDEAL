using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeCheatKey : MonoBehaviour
{
    bool studentGetKey = true;
    public InteractionItemData interactionItemData_StudentRoomKey;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && studentGetKey)
        {
            studentGetKey = false;
            Inventory.Instance.Add(interactionItemData_StudentRoomKey, 1);
            ProgressManager.Instance.SetItemLog(interactionItemData_StudentRoomKey.ID, 1);
        }
    }
}
