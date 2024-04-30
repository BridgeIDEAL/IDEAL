using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [Header("컴포넌트 : 프로그래밍")]
    [SerializeField] private List<BaseEntity> defaultEntityList = new List<BaseEntity>(); // Already Spawn
    [SerializeField] private List<BaseEntity> spawnEntityList = new List<BaseEntity>(); // After Spawn

    private Dictionary<string, BaseEntity> wholeEntityDictionary = new Dictionary<string, BaseEntity>(); // For Use Search 
    private int defaultEntityListCount = 0;
    private int spawnEnitityListCount = 0;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        for (int i = 0; i < defaultEntityListCount; i++) { defaultEntityList[i].UpdateExecute(); }
    }

    public void Init()
    {
        defaultEntityListCount = defaultEntityList.Count;
        spawnEnitityListCount = spawnEntityList.Count;
        for (int i = 0; i < defaultEntityListCount; i++) { wholeEntityDictionary.Add(defaultEntityList[i].name, defaultEntityList[i]); }
        for (int i = 0; i < spawnEnitityListCount; i++) { wholeEntityDictionary.Add(spawnEntityList[i].name, spawnEntityList[i]); }
        // 이벤트 초기화
        GameManager.EntityEntityEvent.SearchEntity = null;
        GameManager.EntityEntityEvent.SpawnEntity = null;
        GameManager.EntityEntityEvent.DespawnEntity = null;
        GameManager.EntityEntityEvent.BroadCastCalmDown = null;
        GameManager.EntityEntityEvent.BroadCastStartConversation = null;
        GameManager.EntityEntityEvent.BroadCastEndConversation = null;
        GameManager.EntityEntityEvent.BroadCastChase = null;
        // 이벤트 추가
        GameManager.EntityEntityEvent.SearchEntity += SearchEntity;
        GameManager.EntityEntityEvent.SpawnEntity += SpawnEntity;
        GameManager.EntityEntityEvent.DespawnEntity += DespawnEntity;        
        GameManager.EntityEntityEvent.BroadCastCalmDown+=SendCalmDownMessage;
        GameManager.EntityEntityEvent.BroadCastStartConversation += SendStartConversationMessage;
        GameManager.EntityEntityEvent.BroadCastEndConversation += SendEndConversationMessage;
        GameManager.EntityEntityEvent.BroadCastChase += SendChaseMessage;
    }

    #region Spawn & Search Method
    /// <summary>
    /// 이형체를 찾는 함수 (매개변수는 이름, 리스트(default/spawn))
    /// </summary>
    /// <param name="_name"></param>
    /// <param name="_searchList"></param>
    /// <returns></returns>
    public BaseEntity SearchEntity(string _name)
    {
        BaseEntity _entity = wholeEntityDictionary[_name];
        if (_entity == null)
            return null;
        else
            return _entity;
    }
    /// <summary>
    /// 이형체를 활성화 하는 함수 (매개변수는 이름)
    /// </summary>
    /// <param name="_name"></param>
    public void SpawnEntity(string _name)
    {
        BaseEntity spawnEntity = SearchEntity(_name);
        if (spawnEntity == null)
            return;
        spawnEntity.gameObject.SetActive(true);
        defaultEntityList.Add(spawnEntity);
        defaultEntityListCount = defaultEntityList.Count;
    }
    /// <summary>
    /// 이형체를 비활성화 하는 함수 (매개변수는 이름)
    /// </summary>
    /// <param name="_name"></param>
    public void DespawnEntity(string _name)
    {
        BaseEntity despawnEntity = SearchEntity(_name);
        if (despawnEntity == null)
            return;
        despawnEntity.gameObject.SetActive(false);
        defaultEntityList.Remove(despawnEntity);
        defaultEntityListCount = defaultEntityList.Count;
    }
    #endregion

    #region Send Message Method 
    /// <summary>
    /// 전부 무관심 상태로 (휴식공간 진입 시)
    /// </summary>
    public void SendCalmDownMessage()
    {
        GameManager.EntityEntityEvent.IsChaseDown = false;
        int count = defaultEntityList.Count;
        for (int idx = 0; idx < count; idx++)
        {
            defaultEntityList[idx].BeCalmDown();
        }
    }
    /// <summary>
    /// 하나의 개체 외에는 모두 침묵상태로
    /// </summary>
    /// <param name="_nonSilentObjectName"></param>
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
    /// <summary>
    /// 대화 시, 대화하는 개체 외에는 모두 침묵상태로
    /// </summary>
    /// <param name="_name"></param>
    public void SendStartConversationMessage(string _name)
    {
        SendSilentMessage(_name);
    }
    /// <summary>
    /// 대화 종료 시 (추격을 당하는 상태라면, 나머지는 침묵 / 추격이 아니라면 모두 무관심상태로)
    /// </summary>
    /// <param name="_name"></param>
    public void SendEndConversationMessage(string _name)
    {
        if (GameManager.EntityEntityEvent.IsChaseDown)
            return;
        else
            SendCalmDownMessage();
    }
    /// <summary>
    /// 추격 시 실행 (추격하는 개체 외에는 모두 침묵상태)
    /// </summary>
    /// <param name="_name"></param>
    public void SendChaseMessage(string _name)
    {
        GameManager.EntityEntityEvent.IsChaseDown = true;
        SendSilentMessage(_name);
    }
    #endregion
}
