using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    string folderPath = "ItemAssetData/";
    public void Damaged(List<string> _parameterList)
    {
        // How many body part??? => Question
        // ConversationManager�� �ִ� Hurt �߰�
    }

    public void GetItem(List<string> _parameterList)
    {
        // Resourece.Load�� �ҷ�����

        // ConversationManager���� Inventory�� �߰�
        //string _itemName = _parameterList[0];
        //string path = folderPath + _itemName;
        //InteractionItemData _itemData = Resources.Load<InteractionItemData>(path);
        //if (_itemData == null)
        //    return;
        //Inventory.Instance.Add(_itemData, 1);
    }

    public void UnableCommunicate(List<string> _parameterList)
    {
        // DialogueManager.Instance.CurrentTalkEntity�� ���� Interaction ��ũ��Ʈ ���� ��, �� �̻� ��ȭ���� ���ϵ��� ����
        InteractionConversation conversation = DialogueManager.Instance.CurrentTalkEntity.GetComponent<InteractionConversation>();
        conversation.cantalk = false;
    }

    // Scripthub - > thirdplayermovelock => ���߱�
    // uimanager => updatemovelock 
}
