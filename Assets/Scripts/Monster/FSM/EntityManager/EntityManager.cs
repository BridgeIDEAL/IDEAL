using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    #region Variable
    [Header("Developer Variable")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<BaseEntity> defaultEntityList = new List<BaseEntity>(); // Already Spawn
    [SerializeField] private List<BaseEntity> spawnEntityList = new List<BaseEntity>(); // After Spawn
    [SerializeField] private GameObject defaultEntityParent; // DefaultEntityParentTransfrom
    [SerializeField] private GameObject spawnEntityParent; // SpawnEntityParentTransfrom
    private Dictionary<string, BaseEntity> wholeEntityDictionary = new Dictionary<string, BaseEntity>(); // For Use Search 
    private int defaultEntityListCount = 0;
    private int spawnEnitityListCount = 0;
    #endregion

    #region UnityLifeCycle

    public void SetUp()
    {
        // Fill Dictionary & SetUp
        //GameManager gameManager = IdealSceneManager.Instance.CurrentGameManager;
        defaultEntityListCount = defaultEntityList.Count;
        spawnEnitityListCount = spawnEntityList.Count;
        for (int idx = 0; idx < defaultEntityListCount; idx++)
        {
            wholeEntityDictionary.Add(defaultEntityList[idx].name, defaultEntityList[idx]);
            if (!defaultEntityList[idx].IsSpawn())
            {
                DespawnEntity(defaultEntityList[idx].name);
                continue;
            }
            defaultEntityList[idx].Setup(playerTransform);
        }
        for (int idx = 0; idx < spawnEnitityListCount; idx++)
        {
            wholeEntityDictionary.Add(spawnEntityList[idx].name, spawnEntityList[idx]);
            if (defaultEntityList[idx].IsSpawn())
                SpawnEntity(defaultEntityList[idx].name);
        }

        //Clear Action
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SpawnEntity = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.DespawnEntity = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastCalmDown = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastStartConversation = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastEndConversation = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastChase = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastPenalty = null;
        //Fill Action
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity += SearchEntity;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SpawnEntity += SpawnEntity;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.DespawnEntity += DespawnEntity;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastCalmDown += SendCalmDownMessage;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastStartConversation += SendStartConversationMessage;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastEndConversation += SendEndConversationMessage;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastChase += SendChaseMessage;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastPenalty += SendPenaltyMesage;
    }
    public void Update()
    {
        for (int i = 0; i < defaultEntityListCount; i++) { defaultEntityList[i].UpdateExecute(); }
    }
    #endregion

    #region Spawn & Search Method
    public BaseEntity SearchEntity(string _name)
    {
        BaseEntity _entity = wholeEntityDictionary[_name];
        if (_entity == null)
            return null;
        else
            return _entity;
    }
    public void SpawnEntity(string _name)
    {
        BaseEntity spawnEntity = SearchEntity(_name);
        if (spawnEntity == null)
            return;
        spawnEntity.gameObject.SetActive(true);
        spawnEntity.Setup(playerTransform);
        defaultEntityList.Add(spawnEntity);
        defaultEntityListCount = defaultEntityList.Count;
        spawnEntity.gameObject.transform.parent = defaultEntityParent.transform;
    }
    public void DespawnEntity(string _name)
    {
        BaseEntity despawnEntity = SearchEntity(_name);
        if (despawnEntity == null)
            return;
        despawnEntity.gameObject.SetActive(false);
        defaultEntityList.Remove(despawnEntity);
        defaultEntityListCount = defaultEntityList.Count;
        despawnEntity.gameObject.transform.parent = spawnEntityParent.transform;
    }
    #endregion

    #region Send Message Method 
    public void SendCalmDownMessage()
    {
        int count = defaultEntityList.Count;
        for (int idx = 0; idx < count; idx++)
        {
            defaultEntityList[idx].BeCalmDown();
        }
    }
    public void SendSilentMessage(string _nonSilentObjectName, EntityEventStateType _entityEventStateType)
    {
        int count = defaultEntityList.Count;
        for (int idx = 0; idx < count; idx++)
        {
            if (defaultEntityList[idx].name == _nonSilentObjectName)
            {
                switch (_entityEventStateType)
                {
                    case EntityEventStateType.StartConversation:
                        defaultEntityList[idx].StartConversation();
                        break;
                    case EntityEventStateType.BeChasing:
                        defaultEntityList[idx].BeChasing();
                        break;
                    case EntityEventStateType.BePenalty:
                        defaultEntityList[idx].BePenalty();
                        break;
                }
                continue;
            }
            defaultEntityList[idx].BeSilent();
        }
    }
    public void SendStartConversationMessage(string _name)
    {
        SendSilentMessage(_name, EntityEventStateType.StartConversation);
    }
    public void SendEndConversationMessage(string _name)
    {
        if (IdealSceneManager.Instance.CurrentGameManager.EntityEM.IsChasePlayer)
            return;
        SendCalmDownMessage();
    }
    public void SendChaseMessage(string _name)
    {
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.IsChasePlayer = true;
        SendSilentMessage(_name, EntityEventStateType.BeChasing);
    }
    public void SendPenaltyMesage(string _name)
    {
        SendSilentMessage(_name, EntityEventStateType.BePenalty);
    }
    #endregion
}
