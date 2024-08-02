using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesController : MonoBehaviour
{
    int listCnt = 0;
    public GameObject interactionEntitiesParent;

    Dictionary<string, BaseEntity> allEntityDictionary = new Dictionary<string, BaseEntity>();
    List<BaseEntity> activeEntityList = new List<BaseEntity>();

    Transform playerTransform;
    public Transform PlayerTransform { get { if (playerTransform == null) playerTransform = GameObject.FindWithTag("Player").transform; return playerTransform; }  }

    public Transform lookTransform;

    #region Awake
    private void Awake()
    {
        LinkAllEntity();
    }

    public void LinkAllEntity()
    {
        BaseEntity[] entities = GetComponentsInChildren<BaseEntity>();
        int entityCnt = entities.Length;
        for (int idx = 0; idx < entityCnt; idx++)
        {
            if (allEntityDictionary.ContainsKey(entities[idx].name))
                continue;
            allEntityDictionary.Add(entities[idx].name, entities[idx]);
            entities[idx].Init(PlayerTransform);
            entities[idx].Controller = this;
        }
    }
    #endregion

    #region Start
    private void Start()
    {
        SetupAllEntity();
        EntityDataManager.Instance.Controller = this;
        if (EntityDataManager.Instance.IsLastEvent)
            InActiveInteractionEntities();
    }

    public void SetupAllEntity()
    {
        List<string> keyList = new List<string>(allEntityDictionary.Keys);
        int keyCnt = keyList.Count;
        for(int idx=0; idx<keyCnt; idx++)
        {
            allEntityDictionary[keyList[idx]].Setup();
        }
    }
    #endregion

    #region Update
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


    #region Chase Event

    [SerializeField, Tooltip("  Last1F_Principal, Last1F_Guard, Last3F_GirlStudent, Last3F_StudentOfHeadTeacher, Jump3F_StudentOfHeadTeacher,Jump2F_GirlStudent")] 
    GameObject[] ChaseEntityGroup;

    private bool isChase = false;
    public bool IsChase{ get { return isChase; } set { isChase = value; ChaseSound(value); } } 

    public void ActiveChaseEntity(ChaseEventType _type)
    {
        ChaseEntityGroup[(int)_type].SetActive(true);
    }

    public void InActiveInteractionEntities()
    {
        listCnt = 0;
        activeEntityList.Clear();
        interactionEntitiesParent.SetActive(false);
    }


    public void ChaseSound(bool _value)
    {
        //EntityDataManager.Instance.IsLastEvent
        if (_value)
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.ChaseStart();
            HealthPointManager.Instance.chased = true;
            // To Do ~~ Speed Up
        }
        else
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.ChaseEnd();
            HealthPointManager.Instance.chased = false;
            // To Do ~~ Speed Down
        }
    }
    #endregion
}