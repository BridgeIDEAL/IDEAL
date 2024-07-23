using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    string folderPath = "ItemAssetData/";
    public void Damaged(List<string> _parameterList)
    {
        // How many body part??? => Question
        // ConversationManager에 있는 Hurt 추가
    }

    public void GetItem(List<string> _parameterList)
    {
        // Resourece.Load로 불러오기

        // ConversationManager에서 Inventory에 추가
        //string _itemName = _parameterList[0];
        //string path = folderPath + _itemName;
        //InteractionItemData _itemData = Resources.Load<InteractionItemData>(path);
        //if (_itemData == null)
        //    return;
        //Inventory.Instance.Add(_itemData, 1);
    }

    public void UnableCommunicate(List<string> _parameterList)
    {
        // DialogueManager.Instance.CurrentTalkEntity를 통해 Interaction 스크립트 접근 후, 더 이상 대화하지 못하도록 수정
        InteractionConversation conversation = DialogueManager.Instance.CurrentTalkEntity.GetComponent<InteractionConversation>();
        conversation.cantalk = false;
    }

    // Scripthub - > thirdplayermovelock => 멈추기
    // uimanager => updatemovelock 
}
