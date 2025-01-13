using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineManager : MonoBehaviour
{   
    [SerializeField] TimeLineConversation conversation;
    [SerializeField] TimeLineFadeEffect fade;
    [SerializeField] TimeLineBlackLineEffect blackLine;

    /// <summary>
    /// 타임라인에서 나오는 대화의 속도, 대화 간의 텀 그리고 대화 내용을 설정
    /// </summary>
    /// <param name="_typeTerm"></param>
    /// <param name="_nextTypeTerm"></param>
    /// <param name="_conversationTexts"></param>
    public void SettingConversationOption(float _typeTerm, float _nextTypeTerm, string[] _conversationTexts)
    {
        conversation.SettingConversationOption(_typeTerm,_nextTypeTerm,_conversationTexts);
    }
}
