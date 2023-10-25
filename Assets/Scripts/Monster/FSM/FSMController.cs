using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController : MonoBehaviour
{
    private List<BaseEntity> entityList;
    public GameObject instantiateEntity;
    public Vector3 spawnPosition;
    private void Awake()
    {
        entityList = new List<BaseEntity>();
        Spawn();    
    }
    void Spawn()
    {
        GameObject go = Instantiate(instantiateEntity);
        Student bear = go.GetComponent<Student>();
        bear.Setup();
        entityList.Add(bear);
        go.transform.position = spawnPosition;
        go.name = instantiateEntity.name;
    }

    private void Update()
    {
        for (int i = 0; i < entityList.Count; i++)
            entityList[i].UpdateBehavior();
    }
}
