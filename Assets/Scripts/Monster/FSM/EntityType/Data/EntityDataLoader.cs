using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EntityData 
{
    public string speakerName;
    public int speakIndex;
    public bool isSpawn;
}


public class EntityDataLoader : MonoBehaviour
{
    //string dataPath = Application.dataPath+"/EntityData/";
    public static EntityDataLoader Instance;

    [SerializeField] TextAsset[] entityDatas;

    Dictionary<string, EntityData> entityDataDic = new Dictionary<string, EntityData>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        LoadAllEntityData();
    }

    public void LoadAllEntityData()
    {
        int cnt = entityDatas.Length;
        for(int idx=0; idx<cnt; idx++)
        {
            EntityData data = new EntityData();
            data = JsonUtility.FromJson<EntityData>(entityDatas[idx].text);
            entityDataDic.Add(data.speakerName, data);
        }
    }

    public EntityData GetEntityData(string _name)
    {
        if (entityDataDic.ContainsKey(_name))
            return entityDataDic[_name];
        return null;
    }
}