using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FSMManager : MonoBehaviour
{
    // 임시 싱글톤
    public static FSMManager instance;

    [Header("임시 몬스터 스폰 관련 변수")]
    public GameObject instantiateEntity;
    public Transform[] spawnPosition;

    private List<BaseEntity> entityList;
    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        Spawn<AType>();
        entityList = new List<BaseEntity>();
    }

    void Spawn<T>() where T : BaseEntity
    {
        GameObject go = Instantiate(instantiateEntity);
        T bear = go.GetComponentInChildren<T>();
        bear.Setup();
        entityList.Add(bear);
        go.transform.position = spawnPosition[0].position;
        go.name = instantiateEntity.name;
    }

    private void Update()
    {
        for (int i = 0; i < entityList.Count; i++)
            entityList[i].UpdateBehavior();
    }
}
