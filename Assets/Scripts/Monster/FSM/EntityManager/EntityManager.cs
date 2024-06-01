using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    #region Variable
    private bool isEnable = false; // Prevent First OnEnable
    private int currentActiveEntityCount = 0; 
    private bool isChasePlayer = false;
    public bool IsChasePlayer { get { return isChasePlayer; } }
    [Header("Developer Variable")]
    [SerializeField] private Transform playerTransform; // PlayerTransform
    [SerializeField] private List<BaseEntity> currentActiveEntityList = new List<BaseEntity>(); // Already Spawn
    [SerializeField] private Dictionary<string, BaseEntity> allEntityDictionary = new Dictionary<string, BaseEntity>(); // For Use Search 
    #endregion

    #region UnityLifeCycle
    /// <summary>
    /// Call By GameManager Awake
    /// </summary>
    public void Init()
    {
        currentActiveEntityList.Clear();
        int dictionaryCount = allEntityDictionary.Count;
        List<string> dictionaryKeys = new List<string>(allEntityDictionary.Keys);
        for(int idx=0; idx<dictionaryCount; idx++)
        {
            if (allEntityDictionary[dictionaryKeys[idx]].IsSpawn)
            {
                currentActiveEntityList.Add(allEntityDictionary[dictionaryKeys[idx]]);
                if(!allEntityDictionary[dictionaryKeys[idx]].IsSetUp)
                    allEntityDictionary[dictionaryKeys[idx]].Setup(playerTransform);
                if(!allEntityDictionary[dictionaryKeys[idx]].gameObject.activeSelf)
                    allEntityDictionary[dictionaryKeys[idx]].gameObject.SetActive(true);
            }
            else
            {
                if (!allEntityDictionary[dictionaryKeys[idx]].IsSetUp)
                    allEntityDictionary[dictionaryKeys[idx]].Setup(playerTransform);
                if (!allEntityDictionary[dictionaryKeys[idx]].gameObject.activeSelf)
                    allEntityDictionary[dictionaryKeys[idx]].gameObject.SetActive(false);
            }
        }
        currentActiveEntityCount = currentActiveEntityList.Count;
    }
    /// <summary>
    /// Awake = Enable = Start 순으로 호출 
    /// </summary>
    public void OnEnable()
    {
        if (isEnable)
        {
            Init();
            return;
        }
        isEnable = true;
    }

    public void Update()
    {
        EntityGroupUpdate();
    }
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
        if (allEntityDictionary[_name] == null)
            return null;
        else
            return allEntityDictionary[_name];
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
        currentActiveEntityList.Add(spawnEntity);
        currentActiveEntityCount = currentActiveEntityList.Count;
        if (!spawnEntity.gameObject.activeSelf)
            spawnEntity.gameObject.SetActive(true);
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
    /// 모든 이형체를 무관심 상태로 변화
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
