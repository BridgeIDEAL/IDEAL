using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMManager
{
    #region Component 
    private List<BaseEntity> entityList; // store all monsterentities for update 
    public Dictionary<string, BaseEntity> entityDictionary; // store all monsterentities for search 
    #endregion

    #region Method
    public void Init()
    {
        // init setting
        entityList = new List<BaseEntity>();
        entityDictionary = new Dictionary<string, BaseEntity>();
        foreach(MonsterData.MonsterStat stat in GameManager.Data.initMonsterInfoDict.Values){ Spawn<BaseEntity>(stat); }
        // state event
        GameManager.EntityEvent.StartConversationAction += StartConversationActionUpdate;
        GameManager.EntityEvent.EndConversationAction += EndConversationActionUpdate;
        GameManager.EntityEvent.ChaseAction += ChaseActionUpdate;
        // spawn event
        GameManager.EntityEvent.SpawnAction += SpawnUpdate;
    }

    void Spawn<T>(MonsterData.MonsterStat stat) where T : BaseEntity
    {
        if (stat == null)
        {
            Debug.Log("읽는데 실패");
            return;
        }
            
        GameObject go = Object.Instantiate<GameObject>(GameManager.Resource.Load<GameObject>($"Prefab/Monster/{stat.monsterPrefabName}"));
        if (go == null)
        {
            Debug.Log("찾는데 실패");
            return;
        }
            
        if(stat.monsterType == "XR" || stat.monsterType == "XL"|| stat.monsterType == "ZR"|| stat.monsterType == "ZL")
        {
            AMiddleAction middle = go.GetComponent<AMiddleAction>();
            middle.SetCalDir(stat.monsterType);
        }

        switch (stat.monsterType)
        {
            case "AH":
                go.AddComponent<AHighAction>();
                break;
            case "AL":
                go.AddComponent<ALowAction>();
                break;
            case "B":
                go.AddComponent<BType>();
                break;
            case "CA":
                go.AddComponent<CAction>();
                break;
            //case "D":
            //    go.AddComponent<DType>();
            //    break;
            default:
                break;
        }

        T type = go.GetComponentInChildren<T>();
        type.Setup(stat);
        type.playerObject = GameManager.Instance.playerObject;
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
        else
        {
            InteractionNurse interactionNurese = go.GetComponent<InteractionNurse>();
            interactionNurese.dialogueRunner = GameManager.Instance.scriptHub.dialogueRunner;
            interactionNurese.conversationManager = GameManager.Instance.scriptHub.conversationManager;
            interactionNurese.detectedStr = stat.detectedStr;
            interactionNurese.dialogueName = stat.dialogueName;
            interactionNurese.monsterName = stat.monsterName;
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
        //추격중이라는 변수를 줌
    }

    public void SpawnUpdate(string _Name)
    {
        if (entityDictionary.ContainsKey(_Name))
            return;
        Spawn<BaseEntity>(GameManager.Data.spawnMonsterInfoDict[_Name]);
    }

    public void DeleteUpdate(string _Name)
    {
        if (entityDictionary[_Name] != null)
        {
            entityDictionary.Remove(_Name);
            for(int i=0; i<entityList.Count; i++)
            {
                if (entityList[i].gameObject.name == _Name)
                {
                    entityList[i].gameObject.SetActive(false);
                    entityList.RemoveAt(i);
                    return;
                }
            }
        }
    }
    #endregion
}
