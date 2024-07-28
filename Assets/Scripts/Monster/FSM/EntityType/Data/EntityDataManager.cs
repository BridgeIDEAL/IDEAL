using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EntityDataManager : MonoBehaviour
{
    #region Entity Dialogue Data
    //string dataPath = Application.dataPath+"/EntityData/";
    public static EntityDataManager Instance;

    [SerializeField] TextAsset entityDatas;

    Dictionary<string, Entity> entityDataDic = new Dictionary<string, Entity>();

    private EntitiesController controller = null;
    public EntitiesController Controller { get { LinkEntitiesController(); return controller; } set { controller = value; } }

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

    #endregion

    #region Entity EventData
    public EntityEventTriggerController EventTriggerController { get; set; } = null;
    public bool IsLastEvent { get; set; } = false;
    Dictionary<string, EntityEventData> eventDic = new Dictionary<string, EntityEventData>();

    public EntityEventData GetEventData(string _name)
    {
        if (eventDic.ContainsKey(_name))
            return eventDic[_name];
        else
            return null;
    }

    public void AddData(EntityEventData _eventData)
    {
        if (eventDic.ContainsKey(_eventData.eventName))
            return;
        eventDic.Add(_eventData.eventName, _eventData);
    }

    public bool HaveEventData(string _name)
    {
        if (eventDic.ContainsKey(_name))
            return true;
        return false;
    }

    #endregion

    public void LinkEntitiesController() { if (controller == null) { GameObject go = GameObject.FindWithTag("EntitiesController"); controller = go.GetComponent<EntitiesController>(); } }
}