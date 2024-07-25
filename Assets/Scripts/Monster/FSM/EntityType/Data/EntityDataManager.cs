using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityDataManager : MonoBehaviour
{
    //string dataPath = Application.dataPath+"/EntityData/";
    public static EntityDataManager Instance;

    [SerializeField] TextAsset entityDatas;

    Dictionary<string, Entity> entityDataDic = new Dictionary<string, Entity>();
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        LoadAllEntityData();
    }

    public void LoadAllEntityData()
    {
        if (entityDatas == null)
            entityDatas = Resources.Load<TextAsset>("EntityData/EntityDatas");
        EntityData data = JsonUtility.FromJson<EntityData>(entityDatas.text);
        int cnt = data.entities.Count;
        for(int idx=0; idx<cnt; idx++)
        {
            if(!entityDataDic.ContainsKey(data.entities[idx].speakerName))
                entityDataDic.Add(data.entities[idx].speakerName, data.entities[idx]);
        }
    }

    public Entity GetEntityData(string _name)
    {
        if (entityDataDic.ContainsKey(_name))
            return entityDataDic[_name];
        else
        {
            Debug.LogError("데이터가 없습니다.");
            return null;
        }
    }

    /// <summary>
    /// 죽을 때 한번 호출
    /// </summary>
    public void ResetData()
    {
        entityDataDic.Clear();
        EntityData data = JsonUtility.FromJson<EntityData>(entityDatas.text);
        int cnt = data.entities.Count;
        for (int idx = 0; idx < cnt; idx++)
        {
            if (!entityDataDic.ContainsKey(data.entities[idx].speakerName))
                entityDataDic.Add(data.entities[idx].speakerName, data.entities[idx]);
        }
    }
}