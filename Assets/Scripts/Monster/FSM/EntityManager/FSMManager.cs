using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMManager : MonoBehaviour
{
    // 임시 싱글톤
    public static FSMManager instance;
    public EntityEvent entityEvent;

    [Header("임시 몬스터 스폰 관련 변수")]
    public GameObject instantiateEntity;
    public Transform[] spawnPosition;

    [Header("몬스터 정보 저장 관련 변수")]
    private List<BaseEntity> entityList;
    private Dictionary<int, BaseEntity> entityDictionary;
    public Action RestAction; // 휴식공간 진입 시
    public Action<int> StartAction; // 상호작용 시작 시
    public Action<int> SuccessAction; // 상호작용 성공 시
    public Action<int> FailAction; // 상호작용 실패 시
    public Action<int> ChaseAction; // 추격 진행 시
    private void Awake()
    {
        entityEvent = new EntityEvent();
        if (instance == null)
            instance = this;
        entityList = new List<BaseEntity>();
        entityDictionary = new Dictionary<int, BaseEntity>();
        Spawn<DType>();
        RestAction += RestActionUpdate;
        StartAction += StartActionUpdate;
        SuccessAction += SuccessActionUpdate;
        FailAction += FailActionUpdate;
        ChaseAction += ChaseActionUpdate;
    }

    void Spawn<T>() where T : BaseEntity
    {
        GameObject go = Instantiate(instantiateEntity);
        T bear = go.GetComponentInChildren<T>();
        bear.Setup();
        entityList.Add(bear);
        entityDictionary.Add(bear.ID, bear);
        go.transform.position = spawnPosition[0].position;
        go.name = instantiateEntity.name;
    }

    private void Update() { for (int i = 0; i < entityList.Count; i++) { entityList[i].UpdateBehavior(); }  }
    
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
