using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMManager : MonoBehaviour
{
    // �ӽ� �̱���
    public static FSMManager instance;
    public EntityEvent entityEvent;

    [Header("�ӽ� ���� ���� ���� ����")]
    public GameObject instantiateEntity;
    public Transform[] spawnPosition;

    [Header("���� ���� ���� ���� ����")]
    private List<BaseEntity> entityList;
    private Dictionary<int, BaseEntity> entityDictionary;
    public Action RestAction; // �޽İ��� ���� ��
    public Action<int> StartAction; // ��ȣ�ۿ� ���� ��
    public Action<int> SuccessAction; // ��ȣ�ۿ� ���� ��
    public Action<int> FailAction; // ��ȣ�ۿ� ���� ��
    public Action<int> ChaseAction; // �߰� ���� ��
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
