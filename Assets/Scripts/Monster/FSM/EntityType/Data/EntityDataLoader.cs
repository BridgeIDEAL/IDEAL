using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EntityData 
{
    // Load File Path != Use File Path
    // Load Json Dialouge File ( Key = speakerName + speakIndex)
    public string speakerName="수위";
    public int speakIndex = 0;
    public bool isSpawn=true;
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

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Z))
    //    {
    //        Debug.Log("출력중!");
    //        Debug.Log(Application.dataPath + "/TestEntity");
    //        EntityData edata = new EntityData();
    //        string newData = JsonUtility.ToJson(edata);
    //        System.IO.File.WriteAllText(Application.dataPath+"/TestEntity",newData);
    //    }
    //}

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