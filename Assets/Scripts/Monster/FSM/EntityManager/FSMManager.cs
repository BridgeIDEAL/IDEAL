using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMManager
{
    private List<BaseEntity> entityList;
    private Dictionary<int, BaseEntity> entityDictionary;
    private static int spawnID = 101;
    public void Init()
    {
        entityList = new List<BaseEntity>();
        entityDictionary = new Dictionary<int, BaseEntity>();
        Spawn<DType>();
        GameManager.EntityEvent.RestAction += RestActionUpdate;
        GameManager.EntityEvent.StartAction += StartActionUpdate;
        GameManager.EntityEvent.SuccessAction += SuccessActionUpdate;
        GameManager.EntityEvent.FailAction += FailActionUpdate;
        GameManager.EntityEvent.ChaseAction += ChaseActionUpdate;
        Debug.Log(entityList.Count);
    }

    void Spawn<T>() where T : BaseEntity
    {
        MonsterData.MonsterStat stat= GameManager.Data.monsterInfoDict[spawnID];
        GameObject go = Object.Instantiate<GameObject>(GameManager.Resource.Load<GameObject>($"Prefab/Monster/{stat.name}"));
        T type = go.GetComponentInChildren<T>();
        type.Setup(stat);
        entityList.Add(type);
        entityDictionary.Add(type.ID, type);
        spawnID += 1;
    }

    public void Update() { for (int i = 0; i < entityList.Count; i++) { entityList[i].UpdateBehavior(); }  }
    
    private void RestActionUpdate()
    {
        foreach(KeyValuePair<int,BaseEntity> entities in entityDictionary)
        {
            entities.Value.RestInteraction();
            entities.Value.CanInteraction = true;
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
