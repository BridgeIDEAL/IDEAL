using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMController : MonoBehaviour
{
    private List<BaseEntity> entityList;
    public GameObject instantiateEntity;
    public Vector3 entityPosition;
    private void Awake()
    {
        Spawn();    
    }
    void Spawn()
    {
        GameObject go = Instantiate(instantiateEntity);
        go.transform.position = entityPosition;
        go.name = instantiateEntity.name;
        BaseEntity be = go.GetComponent<BaseEntity>();
        entityList.Add(be);
    }

    private void Update()
    {
        for (int i = 0; i < entityList.Count; i++)
            entityList[i].UpdateBehavior();
    }

    
}
