using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    #region Variable
    [Header("Developer Variable")]
    [SerializeField] private GameObject player;
    [SerializeField] private List<BaseEntity> defaultEntityList = new List<BaseEntity>(); // Already Spawn
    [SerializeField] private List<BaseEntity> spawnEntityList = new List<BaseEntity>(); // After Spawn
    [SerializeField] private GameObject defaultEntityParent; // DefaultEntityParentTransfrom
    [SerializeField] private GameObject spawnEntityParent; // SpawnEntityParentTransfrom
    private Dictionary<string, BaseEntity> wholeEntityDictionary = new Dictionary<string, BaseEntity>(); // For Use Search 
    private int defaultEntityListCount = 0;
    private int spawnEnitityListCount = 0;
    #endregion

    #region UnityLifeCycle
    /// <summary>
    /// Load By GameManager
    /// </summary>
    public void SetUp()
    {
        // Fill Dictionary & SetUp
        GameManager gameManager = IdealSceneManager.Instance.CurrentGameManager;
        player = gameManager.ValueHub.player;
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
            defaultEntityList[idx].Setup(player);
        }
        for (int idx = 0; idx < spawnEnitityListCount; idx++)
        {
            wholeEntityDictionary.Add(spawnEntityList[idx].name, spawnEntityList[idx]);
            if (defaultEntityList[idx].IsSpawn())
                SpawnEntity(defaultEntityList[idx].name);
        }
        // Clear Action
        gameManager.EntityEM.SearchEntity = null;
        gameManager.EntityEM.SpawnEntity = null;
        gameManager.EntityEM.DespawnEntity = null;
        gameManager.EntityEM.BroadCastCalmDown = null;
        gameManager.EntityEM.BroadCastStartConversation = null;
        gameManager.EntityEM.BroadCastEndConversation = null;
        gameManager.EntityEM.BroadCastChase = null;
        // Fill Action
        gameManager.EntityEM.SearchEntity += SearchEntity;
        gameManager.EntityEM.SpawnEntity += SpawnEntity;
        gameManager.EntityEM.DespawnEntity += DespawnEntity;
        gameManager.EntityEM.BroadCastCalmDown += SendCalmDownMessage;
        gameManager.EntityEM.BroadCastStartConversation += SendStartConversationMessage;
        gameManager.EntityEM.BroadCastEndConversation += SendEndConversationMessage;
        gameManager.EntityEM.BroadCastChase += SendChaseMessage;
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
        spawnEntity.Setup(player);
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
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.IsChaseDown = false;
        int count = defaultEntityList.Count;
        for (int idx = 0; idx < count; idx++)
        {
            defaultEntityList[idx].BeCalmDown();
        }
    }
    public void SendSilentMessage(string _nonSilentObjectName)
    {
        int count = defaultEntityList.Count;
        for (int idx = 0; idx < count; idx++)
        {
            if (defaultEntityList[idx].name == _nonSilentObjectName)
                continue;
            else
                defaultEntityList[idx].BeSilent();
        }
    }
    public void SendStartConversationMessage(string _name)
    {
        SendSilentMessage(_name);
    }
    public void SendEndConversationMessage(string _name)
    {
        if (IdealSceneManager.Instance.CurrentGameManager.EntityEM.IsChaseDown)
            return;
        SendCalmDownMessage();
    }
    public void SendChaseMessage(string _name)
    {
        IdealSceneManager.Instance.CurrentGameManager.EntityEM.IsChaseDown = true;
        SendSilentMessage(_name);
    }
    #endregion
}
