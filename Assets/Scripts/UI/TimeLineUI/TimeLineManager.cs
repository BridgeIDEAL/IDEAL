using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineManager : MonoBehaviour
{   
    [SerializeField] TimeLineConversation conversation;
    [SerializeField] TimeLineFadeEffect fade;
    [SerializeField] TimeLineBlackLineEffect blackLine;

    /// <summary>
    /// Ÿ�Ӷ��ο��� ������ ��ȭ�� �ӵ�, ��ȭ ���� �� �׸��� ��ȭ ������ ����
    /// </summary>
    /// <param name="_typeTerm"></param>
    /// <param name="_nextTypeTerm"></param>
    /// <param name="_conversationTexts"></param>
    public void SettingConversationOption(float _typeTerm, float _nextTypeTerm, string[] _conversationTexts)
    {
        conversation.SettingConversationOption(_typeTerm,_nextTypeTerm,_conversationTexts);
    }
}
