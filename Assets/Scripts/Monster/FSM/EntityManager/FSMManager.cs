using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMManager
{
    private List<BaseEntity> entityList;
    private Dictionary<int, BaseEntity> entityDictionary;
    public void Init()
    {
        entityList = new List<BaseEntity>();
        entityDictionary = new Dictionary<int, BaseEntity>();
        foreach(MonsterData.MonsterStat stat in GameManager.Data.monsterInfoDict.Values){ Spawn<BaseEntity>(stat); }     
        GameManager.EntityEvent.RestAction += RestActionUpdate;
        GameManager.EntityEvent.StartAction += StartActionUpdate;
        GameManager.EntityEvent.SuccessAction += SuccessActionUpdate;
        GameManager.EntityEvent.FailAction += FailActionUpdate;
        GameManager.EntityEvent.ChaseAction += ChaseActionUpdate;
    }

    void Spawn<T>(MonsterData.MonsterStat stat) where T : BaseEntity
    {
        GameObject go = Object.Instantiate<GameObject>(GameManager.Resource.Load<GameObject>($"Prefab/Monster/{stat.name}"));
        T type = go.GetComponentInChildren<T>();
        type.Setup(stat);
        entityList.Add(type);
        type.ID = stat.monsterID;
        entityDictionary.Add(type.ID, type);

        InteractionConversation interactionConversation = go.GetComponent<InteractionConversation>();
        if(interactionConversation != null){
            interactionConversation.dialogueRunner = GameManager.Instance.scriptHub.dialogueRunner;
            interactionConversation.detectedStr = stat.detectedStr;
            interactionConversation.dialogueName = stat.dialogueName;
        }

    }

    public void Update() { for (int i = 0; i < entityList.Count; i++) { entityList[i].UpdateBehavior(); }  }
    
    private void RestActionUpdate() // �޽� ���� ���� ��
    {
        foreach(KeyValuePair<int,BaseEntity> entities in entityDictionary)
        {
            entities.Value.RestInteraction();
            entities.Value.CanInteraction = true; // ��׷� ����
        }
    }
    
    private void StartActionUpdate(int _ID)
    {
        foreach (KeyValuePair<int, BaseEntity> entities in entityDictionary)
        {
            if (entities.Key == _ID) { entities.Value.StartInteraction(); continue; }
            entities.Value.CanInteraction = false;
        }
    }

    private void SuccessActionUpdate(int _ID)
    {
        foreach (KeyValuePair<int, BaseEntity> entities in entityDictionary)
        {
            if (entities.Key == _ID) { entities.Value.SuccessInteraction(); continue; }
            entities.Value.CanInteraction = true;
        }
    }
    private void FailActionUpdate(int _ID)
    {
        foreach (KeyValuePair<int, BaseEntity> entities in entityDictionary)
        {
            if (entities.Key == _ID) { entities.Value.FailInteraction(); continue; }
            entities.Value.CanInteraction = true;
        }
    }

    private void ChaseActionUpdate(int _ID)
    {
        foreach (KeyValuePair<int, BaseEntity> entities in entityDictionary)
        {
            if (entities.Key == _ID) {  entities.Value.ChaseInteraction();continue;}
            entities.Value.CanInteraction = false;
        }
    }
}
