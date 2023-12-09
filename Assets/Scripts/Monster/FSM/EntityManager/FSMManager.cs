using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMManager
{
    private GameObject playerGameObject;
    private List<BaseEntity> entityList;
    private Dictionary<int, BaseEntity> entityDictionary;
    public void Init()
    {
        entityList = new List<BaseEntity>();
        entityDictionary = new Dictionary<int, BaseEntity>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        foreach(MonsterData.MonsterStat stat in GameManager.Data.monsterInfoDict.Values){ Spawn<BaseEntity>(stat); }     
        GameManager.EntityEvent.RestAction += RestActionUpdate;
        GameManager.EntityEvent.ConversationAction += ConversationActionUpdate;
        GameManager.EntityEvent.SuccessAction += SuccessActionUpdate;
        GameManager.EntityEvent.FailAction += FailActionUpdate;
        GameManager.EntityEvent.ChaseAction += ChaseActionUpdate;
    }

    void Spawn<T>(MonsterData.MonsterStat stat) where T : BaseEntity
    {
        GameObject go = Object.Instantiate<GameObject>(GameManager.Resource.Load<GameObject>($"Prefab/Monster/{stat.name}"));
        // switch (stat.name){
        //     case "A":
        //     go.AddComponent<AType>();            
        //     break;
        //     case "B":
        //       go.AddComponent<BType>();
        //       go.AddComponent<NavMeshAgent>();
        //     break;
        //     case "C":
        //       go.AddComponent<CType>();
        //       go.AddComponent<NavMeshAgent>();
        //     break;
        //     case "D":
        //       go.AddComponent<DType>();
        //       go.AddComponent<NavMeshAgent>();
        //     break;
        // }
        T type = go.GetComponentInChildren<T>();
        type.Setup(stat);
        entityList.Add(type);
        type.ID = stat.monsterID;
        type.playerObject = playerGameObject;
        entityDictionary.Add(type.ID, type);

        InteractionConversation interactionConversation = go.GetComponent<InteractionConversation>();
        if(interactionConversation != null){
            interactionConversation.dialogueRunner = GameManager.Instance.scriptHub.dialogueRunner;
            interactionConversation.detectedStr = stat.detectedStr;
            interactionConversation.dialogueName = stat.dialogueName;
        }

    }

    public void Update() { for (int i = 0; i < entityList.Count; i++) { entityList[i].UpdateBehavior(); }  }
    
    private void RestActionUpdate() // 휴식 공간 진입 시
    {
        GameManager.EntityEvent.CanInteraction = true;
        foreach (KeyValuePair<int,BaseEntity> entities in entityDictionary){ entities.Value.RestInteraction();}
    }
    
    private void ConversationActionUpdate(int _ID) // 대화 시작 시
    {
        GameManager.EntityEvent.CanInteraction = false;
        entityDictionary[_ID].ConversationInteraction();
    }

    private void SuccessActionUpdate(int _ID) // 상호작용 성공 시
    {
        GameManager.EntityEvent.CanInteraction = true;
        entityDictionary[_ID].SuccessInteraction();
    }
    private void FailActionUpdate(int _ID) // 상호작용 실패 시
    {
        GameManager.EntityEvent.CanInteraction = true;
        entityDictionary[_ID].FailInteraction();
    }

    private void ChaseActionUpdate(int _ID) // 상호작용 불가능, 해당 개체 이외엔 다 휴식 상태로 전환
    {
        foreach (KeyValuePair<int, BaseEntity> entities in entityDictionary)
        {
            if (entities.Key == _ID)
                continue;
            entities.Value.RestInteraction();     
        }
        GameManager.EntityEvent.CanInteraction = false;
        entityDictionary[_ID].ChaseInteraction();
    }
}
