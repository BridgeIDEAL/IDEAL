using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesController : MonoBehaviour
{
    [SerializeField] GameObject entityParent;

    int listCnt = 0;

    Dictionary<string, BaseEntity> allEntityDictionary = new Dictionary<string, BaseEntity>();
    List<BaseEntity> activeEntityList = new List<BaseEntity>();

    #region Unity Life Cycle
    
    // Awake
    private void Awake()
    {
        LinkAllEntity();
    }

    public void LinkAllEntity()
    {
        BaseEntity[] entities = entityParent.GetComponentsInChildren<BaseEntity>();
        int entityCnt = entities.Length;
        for (int idx = 0; idx < entityCnt; idx++)
        {
            if (allEntityDictionary.ContainsKey(entities[idx].name))
                continue;
            allEntityDictionary.Add(entities[idx].name, entities[idx]);
        }
    }

    // Start
    private void Start()
    {
        SetupAllEntity();
    }

    public void SetupAllEntity()
    {
        foreach(KeyValuePair<string,BaseEntity> variable in allEntityDictionary)
        {
            variable.Value.Setup();
        }
    }

    // Update
    private void Update()
    {
        ExecuteActiveEntities();
    }

    public void ExecuteActiveEntities()
    {
        for (int idx = 0; idx < listCnt; idx++)
        {
            activeEntityList[idx].Execute();
        }
    }

    #endregion

    #region FindEntityMethod
    public void ActiveEntity(string _name)
    {
        if (!allEntityDictionary.ContainsKey(_name))
            return;
        listCnt = activeEntityList.Count;
        for (int idx = 0; idx < listCnt; idx++)
        {
            if (activeEntityList[idx] == allEntityDictionary[_name])
                return;
        }
        activeEntityList.Add(allEntityDictionary[_name]);
        allEntityDictionary[_name].SetActiveState(true);
        listCnt = activeEntityList.Count;
    }

    public void InActiveEntity(string _name)
    {
        if (!allEntityDictionary.ContainsKey(_name))
            return;
        listCnt = activeEntityList.Count;
        for(int idx=0; idx<listCnt; idx++)
        {
            if(activeEntityList[idx]== allEntityDictionary[_name])
            {
                BaseEntity entity = activeEntityList[idx];
                entity.SetActiveState(false);
                activeEntityList.RemoveAt(idx);
            }
        }
        listCnt = activeEntityList.Count;
    }

    public BaseEntity GetEntity(string _name)
    {
        if (allEntityDictionary.ContainsKey(_name))
            return allEntityDictionary[_name];
        else
            return null;
    }
    #endregion

    #region Message Method
    // All Entity Same Action
    public void SendMessage(EntityStateType _all)
    {
        int listCnt = activeEntityList.Count;
        for(int idx=0; idx< listCnt; idx++)
        {
            activeEntityList[idx].ReceiveMessage(_all);
        }
    }

    // All Entity Same Action without One
    public void SendMessage(string _name, EntityStateType _one, EntityStateType _allButOne)
    {
        int listCnt = activeEntityList.Count;
        for (int idx = 0; idx < listCnt; idx++)
        {
            if (activeEntityList[idx] == allEntityDictionary[_name])
            {
                activeEntityList[idx].ReceiveMessage(_one);
                continue;
            }
            activeEntityList[idx].ReceiveMessage(_allButOne);
        }
    }
    #endregion
}