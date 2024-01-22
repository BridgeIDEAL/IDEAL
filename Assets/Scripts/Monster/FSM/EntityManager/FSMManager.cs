using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMManager
{
    private GameObject playerGameObject; // player 
    private List<BaseEntity> entityList; // store all monsterentities for update 
    private Dictionary<string, BaseEntity> entityDictionary; // store all monsterentities for search 
    public void Init()
    {
        entityList = new List<BaseEntity>();
        entityDictionary = new Dictionary<string, BaseEntity>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        foreach(MonsterData.MonsterStat stat in GameManager.Data.initMonsterInfoDict.Values){ Spawn<BaseEntity>(stat); } // init spawn
        // state event
        GameManager.EntityEvent.StartConversationAction += StartConversationActionUpdate;
        GameManager.EntityEvent.EndConversationAction += EndConversationActionUpdate;
        GameManager.EntityEvent.ChaseAction += ChaseActionUpdate;
        // spawn event
        GameManager.EntityEvent.SpawnAction += SpawnUpdate;
    }

    void Spawn<T>(MonsterData.MonsterStat stat) where T : BaseEntity
    {
        GameObject go = Object.Instantiate<GameObject>(GameManager.Resource.Load<GameObject>($"Prefab/MonsterPrefab/{stat.monsterPrefabName}"));
        switch (stat.monsterType)
        {
            case "AL":
                go.AddComponent<ALowAction>();
                break;
            case "AH":
                go.AddComponent<AHighAction>();
                break;
            case "B":
                go.AddComponent<BType>();
                //go.AddComponent<NavMeshAgent>();
                break;
            case "D":
                go.AddComponent<DType>();
                go.AddComponent<NavMeshAgent>();
                break;
            default:
                break;
        }

        T type = go.GetComponentInChildren<T>(); 
        type.Setup(stat);
        type.playerObject = playerGameObject;
        entityList.Add(type);
        entityDictionary.Add(stat.monsterName, type);
        InteractionConversation interactionConversation = go.GetComponent<InteractionConversation>();
        if(interactionConversation != null){
            interactionConversation.dialogueRunner = GameManager.Instance.scriptHub.dialogueRunner;
            interactionConversation.conversationManager = GameManager.Instance.scriptHub.conversationManager;
            interactionConversation.detectedStr = stat.detectedStr;
            interactionConversation.dialogueName = stat.dialogueName;
            interactionConversation.monsterName = stat.monsterName;
        }
    }

    public void Update() { for (int i = 0; i < entityList.Count; i++) { entityList[i].UpdateBehavior(); }  }
   
    private void StartConversationActionUpdate(string _Key) // 대화 시작 시
    {
        GameManager.EntityEvent.CanInteraction = false;
        foreach (KeyValuePair<string, BaseEntity> entities in entityDictionary) {
            if (_Key == entities.Key)
                continue;
            entities.Value.SpeechlessInteraction(); 
        }
        entityDictionary[_Key].StartConversationInteraction();
    }

    private void EndConversationActionUpdate() // 대화 종료 시, 휴식 공간 진입 시
    {
        GameManager.EntityEvent.CanInteraction = true;
        foreach (KeyValuePair<string, BaseEntity> entities in entityDictionary) { entities.Value.EndConversationInteraction(); }
    }

    private void ChaseActionUpdate(string _Key) // 상호작용 불가능, 해당 개체 이외엔 다 휴식 상태로 전환
    {
        foreach (KeyValuePair<string, BaseEntity> entities in entityDictionary)
        {
            if (entities.Key == _Key)
                continue;
            entities.Value.SpeechlessInteraction();     
        }
        GameManager.EntityEvent.CanInteraction = false;
        entityDictionary[_Key].ChaseInteraction();
    }

    public void SpawnUpdate(string _Name)
    {
        if (entityDictionary.ContainsKey(_Name))
            return;
        Spawn<BaseEntity>(GameManager.Data.spawnMonsterInfoDict[_Name]);
    }
}
