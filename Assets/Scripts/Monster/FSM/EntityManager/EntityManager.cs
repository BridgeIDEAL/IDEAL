using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    #region Variable
    [Header("플레이어/이형체 연결")]
    [SerializeField] private Transform playerTransform; // PlayerTransform
    public Transform PlayerTransform { get { return playerTransform; } }
    [SerializeField] private List<BaseEntity> currentActiveEntityList = new List<BaseEntity>(); // Already Spawn
    [SerializeField] private List<BaseEntity> allEntityList = new List<BaseEntity>(); // For Use Search 

    private bool isEnable = false; // Prevent First OnEnable
    private int allEntityCount = 0;
    private int currentActiveEntityCount = 0;
    private bool isChasePlayer = false; // Use For EndConversation
    public bool IsChasePlayer { get { return isChasePlayer; } set { isChasePlayer = value; } }
    #endregion

    #region UnityLifeCycle
    /// <summary>
    /// Call By GameManager Awake
    /// </summary>
    public void Init()
    {
        allEntityCount = allEntityList.Count;
        currentActiveEntityList.Clear();
        for(int idx=0; idx< allEntityCount; idx++)
        {
            if (allEntityList[idx].IsSpawn)
            {
                currentActiveEntityList.Add(allEntityList[idx]);
                if(!allEntityList[idx].IsSetUp)
                    allEntityList[idx].Setup();
                if(!allEntityList[idx].gameObject.activeSelf)
                    allEntityList[idx].gameObject.SetActive(true);
            }
            else
            {
                if (!allEntityList[idx].IsSetUp)
                    allEntityList[idx].Setup();
                if (allEntityList[idx].gameObject.activeSelf)
                    allEntityList[idx].gameObject.SetActive(false);
            }
        }
        currentActiveEntityCount = currentActiveEntityList.Count;
    }
    /// <summary>
    /// Awake = Enable = Start 순으로 호출 
    /// </summary>
    //public void OnEnable()
    //{
    //    if (isEnable)
    //    {
    //        Init();
    //        return;
    //    }
    //    isEnable = true;
    //}
    //public void Update()
    //{
    //    EntityGroupUpdate();
    //}
    public void EntityGroupUpdate()
    {
        for (int idx = 0; idx < currentActiveEntityCount; idx++) 
        { 
            currentActiveEntityList[idx].UpdateState();
        }
    }
    #endregion

    #region Spawn & Search Method
    public BaseEntity SearchEntity(string _name)
    {
        for(int idx=0; idx< allEntityCount; idx++)
        {
            if (allEntityList[idx].name == _name)
            {
                return allEntityList[idx];
            }
        }
        return null;
    }
    public void SpawnEntity(string _name)
    {
        BaseEntity spawnEntity = SearchEntity(_name);
        if (spawnEntity == null)
            return;
        for(int idx =0; idx<currentActiveEntityCount; idx++)
        {
            if (spawnEntity == currentActiveEntityList[idx])
                return;
        }
        spawnEntity.IsSpawn = true;
        if (!spawnEntity.gameObject.activeSelf)
            spawnEntity.gameObject.SetActive(true);
        currentActiveEntityList.Add(spawnEntity);
        currentActiveEntityCount = currentActiveEntityList.Count;
    }
    public void DespawnEntity(string _name)
    {
        BaseEntity despawnEntity = SearchEntity(_name);
        if (despawnEntity == null)
            return;
        for (int idx = 0; idx < currentActiveEntityCount; idx++)
        {
            if (despawnEntity == currentActiveEntityList[idx])
            {
                despawnEntity.IsSpawn = false;
                currentActiveEntityList.RemoveAt(idx);
                currentActiveEntityCount = currentActiveEntityList.Count;
                if (despawnEntity.gameObject.activeSelf)
                    despawnEntity.gameObject.SetActive(false);
                break;
            }
        }
    }
    #endregion

    #region Send Message Method 
    /// <summary>
    /// Enter SafeZone or End Conversation
    /// </summary>
    public void SendCalmDownMessage()
    {
        for (int idx = 0; idx < currentActiveEntityCount; idx++)
        {
            currentActiveEntityList[idx].IdleState();
        }
    }
    
    public void SendStartConversationMessage(string _name)
    {
        SendSilentMessage(_name, EntityStateType.Talk);
    }
   
    public void SendEndConversationMessage(string _name)
    {
        if (isChasePlayer)
            return;
        SendCalmDownMessage();
    }
    public void SendPenaltyMesage(string _name)
    {
        SendSilentMessage(_name, EntityStateType.Penalty);
    }
    public void SendExtraMessage(string _name, bool _isChase = false)
    {
        if (_isChase)
        {
            SendChaseMessage(_name);
            return;
        }
        SendSilentMessage(_name, EntityStateType.Extra);
    }
    public void SendChaseMessage(string _name)
    {
        isChasePlayer = true;
        SendSilentMessage(_name, EntityStateType.Chase);
    }
    /// <summary>
    /// Use By Send Message
    /// </summary>
    /// <param name="_nonSilentObjectName"></param>
    /// <param name="_entityStateType"></param>
    public void SendSilentMessage(string _nonSilentObjectName, EntityStateType _entityStateType)
    {
        for (int idx = 0; idx < currentActiveEntityCount; idx++)
        {
            if (currentActiveEntityList[idx] == SearchEntity(_nonSilentObjectName))
            {
                switch (_entityStateType)
                {
                    case EntityStateType.Talk:
                        currentActiveEntityList[idx].TalkState();
                        break;
                    case EntityStateType.Quiet:
                        currentActiveEntityList[idx].QuietState();
                        break;
                    case EntityStateType.Extra:
                        currentActiveEntityList[idx].ExtraState();
                        break;
                    case EntityStateType.Chase:
                        currentActiveEntityList[idx].ChaseState();
                        break;
                    case EntityStateType.Penalty:
                        currentActiveEntityList[idx].PenaltyState();
                        break;
                    default:
                        break;
                }
                continue;
            }
            currentActiveEntityList[idx].QuietState();
        }
    }
    #endregion
}
