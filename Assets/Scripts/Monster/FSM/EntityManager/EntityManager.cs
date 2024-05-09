using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    #region Variable
    [Header("Developer Variable")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<BaseEntity> spawnEntityList = new List<BaseEntity>(); // Already Spawn
    [SerializeField] private List<BaseEntity> deSpawnEntityList = new List<BaseEntity>(); // After Spawn
    private Dictionary<string, BaseEntity> wholeEntityDictionary = new Dictionary<string, BaseEntity>(); // For Use Search 
    private int spawnEntityListCount = 0;
    private int deSpawnEntityListCount = 0;
    private bool isOnceSetUp = false;
    #endregion

    #region UnityLifeCycle
    public void Init()
    {
        // Fill Dictionary & SetUp
        //GameManager gameManager = IdealSceneManager.Instance.CurrentGameManager;
        spawnEntityListCount = spawnEntityList.Count;
        deSpawnEntityListCount = deSpawnEntityList.Count;
        for (int idx = 0; idx < spawnEntityListCount; idx++)
        {
            wholeEntityDictionary.Add(spawnEntityList[idx].name, spawnEntityList[idx]);
            spawnEntityList[idx].Setup(playerTransform);

        }
        for (int idx = 0; idx < deSpawnEntityListCount; idx++)
        {
            wholeEntityDictionary.Add(deSpawnEntityList[idx].name, deSpawnEntityList[idx]);
            deSpawnEntityList[idx].Setup(playerTransform);
            deSpawnEntityList[idx].gameObject.SetActive(false);
        }

        //Clear Action
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SpawnEntity = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.DespawnEntity = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SetSpawnState= null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastCalmDown = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastStartConversation = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastEndConversation = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastChase = null;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastPenalty = null;
        //Fill Action
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity += SearchEntity;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SpawnEntity += SpawnEntity;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.DespawnEntity += DespawnEntity;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.SetSpawnState += ChangeSpawnStateEntity;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastCalmDown += SendCalmDownMessage;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastStartConversation += SendStartConversationMessage;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastEndConversation += SendEndConversationMessage;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastChase += SendChaseMessage;
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.BroadCastPenalty += SendPenaltyMesage;
        isOnceSetUp = true;
    }
    public void Update()
    {
        for (int i = 0; i < spawnEntityListCount; i++) { spawnEntityList[i].UpdateExecute(); }
    }
    public void OnEnable()
    {
        if (isOnceSetUp)
        {
            for(int idx =0; idx<spawnEntityListCount; idx++)
            {
                if (!spawnEntityList[idx].IsSpawn)
                    DespawnEntity(spawnEntityList[idx].name);
            }
            for(int idx=0; idx< deSpawnEntityListCount; idx++)
            {
                if (deSpawnEntityList[idx].IsSpawn)
                    SpawnEntity(spawnEntityList[idx].name);
            }
        }
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
        spawnEntityList.Add(spawnEntity);
        deSpawnEntityList.Remove(spawnEntity);
        spawnEntity.gameObject.SetActive(true);
        spawnEntityListCount = spawnEntityList.Count;
    }
    public void DespawnEntity(string _name)
    {
        BaseEntity despawnEntity = SearchEntity(_name);
        if (despawnEntity == null)
            return;
        spawnEntityList.Remove(despawnEntity);
        deSpawnEntityList.Add(despawnEntity);
        despawnEntity.gameObject.SetActive(false);
        deSpawnEntityListCount = deSpawnEntityList.Count;
    }

    public void ChangeSpawnStateEntity(string _name)
    {
        BaseEntity entity = SearchEntity(_name);
        bool _isSpawn = entity.IsSpawn;
        entity.IsSpawn = !_isSpawn;
    }
    #endregion

    #region Send Message Method 
    public void SendCalmDownMessage()
    {
        for (int idx = 0; idx < spawnEntityListCount; idx++)
        {
            spawnEntityList[idx].BeCalmDown();
        }
    }
    public void SendSilentMessage(string _nonSilentObjectName, EntityEventStateType _entityEventStateType)
    {
        for (int idx = 0; idx < spawnEntityListCount; idx++)
        {
            if (spawnEntityList[idx].name == _nonSilentObjectName)
            {
                switch (_entityEventStateType)
                {
                    case EntityEventStateType.StartConversation:
                        spawnEntityList[idx].StartConversation();
                        break;
                    case EntityEventStateType.BeChasing:
                        spawnEntityList[idx].BeChasing();
                        break;
                    case EntityEventStateType.BePenalty:
                        spawnEntityList[idx].BePenalty();
                        break;
                }
                continue;
            }
            spawnEntityList[idx].BeSilent();
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
