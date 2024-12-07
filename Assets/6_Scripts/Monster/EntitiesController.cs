using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesController : MonoBehaviour
{
    int listCnt = 0;
    [Header("Interaction Group Parent : Use for inactive all interaction entities")] public GameObject interactionEntitiesParent;

    Dictionary<string, BaseEntity> allEntityDictionary = new Dictionary<string, BaseEntity>();
    List<BaseEntity> activeEntityList = new List<BaseEntity>();

    [SerializeField, Header("Foot Pos")] Transform playerTransform; // Foot Pos
    public Transform PlayerTransform { get { if (playerTransform == null) playerTransform = GameObject.FindWithTag("Player").transform; return playerTransform; } }

    [SerializeField, Header("Eye Pos")] Transform playerHeightTransform; // Eye Pos
    public Transform PlayerHeightTransform { get { return playerHeightTransform; } }

    List<OnlyChase> chaseGroup = new List<OnlyChase>();

    #region Awake : Link Entity & Call Init Entity Information
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
            entities[idx].Init(playerTransform, playerHeightTransform);
            entities[idx].Controller = this;
        }
    }
    #endregion

    #region Start : Call Entity Information Setup
    private void Start()
    {
        SetupAllEntity();
        EntityDataManager.Instance.Controller = this;
        if (EventDataManager.Instance.RingAfterSchoolBell)
            InActiveInteractionEntities();
    }

    public void SetupAllEntity()
    {
        List<string> keyList = new List<string>(allEntityDictionary.Keys);
        int keyCnt = keyList.Count;
        for (int idx = 0; idx < keyCnt; idx++)
        {
            allEntityDictionary[keyList[idx]].Setup();
        }
    }
    #endregion

    #region Update : Call Entity Behaviour State

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

    #region FindEntityMethod : Get Entity Information & Control
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
        for (int idx = 0; idx < listCnt; idx++)
        {
            if (activeEntityList[idx] == allEntityDictionary[_name])
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

    #region Message Method : Change Entity State Methods
    // All Entity Same Action
    public void SendMessage(EntityStateType _all)
    {
        int listCnt = activeEntityList.Count;
        for (int idx = 0; idx < listCnt; idx++)
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
    [Header("Last & Jump : Only Chase")]
    [Tooltip("Last1F_APrincipal, Last1F_BPrincipal, Last1F_Guard, Last3F_GirlStudent, Last3F_StudentOfHeadTeacher, Jump3F_StudentOfHeadTeacher,Jump2F_GirlStudent")]
    [SerializeField]
    GameObject[] ChaseEntityGroup;

    private bool isChase = false;
    public bool IsChase
    {
        get
        {
            return isChase;
        }
        set
        {
            if (isChase && value)
            {
                isChase = value;
            }
            else
            {
                isChase = value;
                ChaseSound(value);
            }
        }
    }
    public bool PrincipalChase { get; set; } = false;

    public void ActiveChaseEntity(ChaseEventType _type)
    {
        ChaseEntityGroup[(int)_type].SetActive(true);
    }

    public void InActiveInteractionEntities()
    {
        listCnt = 0;
        if (activeEntityList.Count != 0)
            activeEntityList.Clear();
        interactionEntitiesParent.SetActive(false);
    }

    public void InActiveExceptOne(GameObject go)
    {
        listCnt = 0;
        activeEntityList.Clear();
        go.transform.SetParent(null);
        interactionEntitiesParent.SetActive(false);
    }


    public void ChaseSound(bool _value)
    {
        //EntityDataManager.Instance.IsLastEvent
        if (EventDataManager.Instance.RingAfterSchoolBell)
            return;

        if (_value)
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.ChaseStart();
            HealthPointManager.Instance.chased = true;
            PenaltyPointManager.Instance.SetChase(true);
            // To Do ~~ Speed Up
        }
        else
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.ChaseEnd();
            HealthPointManager.Instance.chased = false;
            PenaltyPointManager.Instance.SetChase(false);
            // To Do ~~ Speed Down
        }
    }
    #endregion

    #region Manage OnlyChase
    public void AddChaseGroup(OnlyChase onlyChase)
    {
        int cnt = chaseGroup.Count;
        for (int i = 0; i < cnt; i++)
        {
            if (chaseGroup[i] == onlyChase)
                return;
        }
        chaseGroup.Add(onlyChase);
    }

    public void DisableChaseGroupExceptOne(OnlyChase exceptOne)
    {
        int cnt = chaseGroup.Count;
        for (int i = 0; i < cnt; i++)
        {
            if (chaseGroup[i] == exceptOne)
                continue;
            chaseGroup[i].gameObject.SetActive(false);
        }
    }

    public void DisableChaseGroup()
    {
        int cnt = chaseGroup.Count;
        for (int i = 0; i < cnt; i++)
        {
            chaseGroup[i].gameObject.SetActive(false);
        }
    }
    #endregion
    public void InActivePrincipal()
    {
        if (!allEntityDictionary.ContainsKey("PatrolPrincipal"))
            return;
        listCnt = activeEntityList.Count;
        for (int idx = 0; idx < listCnt; idx++)
        {
            if (activeEntityList[idx] == allEntityDictionary["PatrolPrincipal"])
            {
                PrincipalPatrol entity = activeEntityList[idx].gameObject.GetComponent<PrincipalPatrol>();
                entity?.ChangeState(EntityStateType.Quiet);
            }
        }
    }

    public void ActivePrincipal()
    {
        if (!allEntityDictionary.ContainsKey("PatrolPrincipal"))
            return;
        listCnt = activeEntityList.Count;
        for (int idx = 0; idx < listCnt; idx++)
        {
            if (activeEntityList[idx] == allEntityDictionary["PatrolPrincipal"])
            {
                PrincipalPatrol entity = activeEntityList[idx].gameObject.GetComponent<PrincipalPatrol>();
                entity?.ChangeState(EntityStateType.Idle);
            }
        }
    }
}