using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    string folderPath = "ItemAssetData/";
    public void Damaged(List<string> _parameterList)
    {
        // Damage
        // ConversationManager�� �ִ� Hurt �߰�
    }

    public void GetItem(List<string> _parameterList)
    {
        // ConversationManager���� Inventory�� �߰�
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

    // Scripthub - > thirdplayermovelock => ���߱�
    // uimanager => updatemovelock 
}
