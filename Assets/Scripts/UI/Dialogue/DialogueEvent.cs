using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    string folderPath = "ItemAssetData/";
    public void Damaged(List<string> _parameterList)
    {
        // Damage
        // ConversationManager에 있는 Hurt 추가
    }

    public void GetItem(List<string> _parameterList)
    {
        // ConversationManager에서 Inventory에 추가
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

    // Scripthub - > thirdplayermovelock => 멈추기
    // uimanager => updatemovelock 
}
