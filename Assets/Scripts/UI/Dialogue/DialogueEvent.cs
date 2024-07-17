using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    string folderPath = "ItemAssetData/";
    public void Damaged(List<string> _parameterList)
    {
        // Damage
    }

    public void GetItem(List<string> _parameterList)
    {
        string _itemName = _parameterList[0];
        string path = folderPath + _itemName;
        InteractionItemData _itemData = Resources.Load<InteractionItemData>(path);
        if (_itemData == null)
            return;
        Inventory.Instance.Add(_itemData, 1);
        //ActivationLogManager.Instance.AddActivationLog(activationLogNum);
    }

    public void GameOver(List<string> _parameterList)
    {
        // GameOver
    }
}
