using System.Collections.Generic;
using UnityEngine;

public class EventDataManager : MonoBehaviour
{
    [Header("진로진학부 퀘스트"),SerializeField] TalkDialogueQuest careerQuest;
    public TalkDialogueQuest CareerQuest { get { return careerQuest; } }

    public static EventDataManager Instance;
    public bool RingAfterSchoolBell { get; set; } = false;
    Dictionary<string, EventData> eventGroup = new Dictionary<string, EventData>();


    #region Relate Controller
    public ItemSpawnController ItemController { get; set; } = null;
    public EventTriggerController TriggerController { get; set; } = null;
    #endregion

    #region Use For Principal Teleport
    private EntityNoticeManager notice = new EntityNoticeManager();
    public EntityNoticeManager Notice { get { return notice; } }
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public EventData GetEventData(string _eventName)
    {
        if(eventGroup.ContainsKey(_eventName))
            return eventGroup[_eventName];
        return null;
    }

    public void AddEventData(string _eventName, EventData _data)
    {
        if (eventGroup.ContainsKey(_eventName))
            return;
        eventGroup.Add(_eventName,_data);
        //Debug.Log(_eventName + "데이터 추가 완료~~");
    }

    public void ClearEventDatas() 
    {
        if(eventGroup.Count!=0) eventGroup.Clear();
        RingAfterSchoolBell = false;
        careerQuest?.Init(); 
    }
}


[System.Serializable]
public class TalkDialogueQuest
{
    Dictionary<string, bool> questDictionary = new Dictionary<string, bool>();
    [SerializeField] string[] dialogueQuestNames;
    [SerializeField] bool[] acceptDialogueQuests;
    public void Init()
    {
        Clear();
        int cnt = dialogueQuestNames.Length;
        for (int i = 0; i < cnt; i++)
        {
            if (questDictionary.ContainsKey(dialogueQuestNames[i]))
                continue;
            questDictionary.Add(dialogueQuestNames[i], acceptDialogueQuests[i]);
        }
    }

    public void Clear()
    {
        if (questDictionary.Count == 0)
            return;
        questDictionary.Clear();
    }

    public void AcceptDialogueQuest(string _questName, bool _acceptDialogueQuest)
    {
        if (questDictionary.ContainsKey(_questName))
        {
            questDictionary[_questName] = _acceptDialogueQuest;
        }
    }

    public bool CheckClearQuest()
    {
        List<string> keys = new List<string>(questDictionary.Keys); 
        int cnt = keys.Count;   
        for(int i=0; i<cnt; i++)
        {
            if (questDictionary[keys[i]] == false)
                return false;
        }

        return true;
    }
}