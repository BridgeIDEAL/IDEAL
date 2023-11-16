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
        GameObject go = Object.Instantiate<GameObject>(GameManager.Instance.go);
        T bear = go.GetComponentInChildren<T>();
        bear.InitTransform = GameManager.Instance.tf;
        bear.Setup();
        entityList.Add(bear);
        entityDictionary.Add(bear.ID, bear);
        go.transform.position = GameManager.Instance.tf.position;
        go.name = GameManager.Instance.go.name;
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
