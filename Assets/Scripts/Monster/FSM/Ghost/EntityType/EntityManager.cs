using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [Header("������Ʈ : ���α׷���")]
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
        // �̺�Ʈ �ʱ�ȭ
        // TO DO ~~~~~~~~~~~~
        // GameManager.EntityEntityEvent.SearchEntity = null;
        // GameManager.EntityEntityEvent.SpawnEntity = null;
        // GameManager.EntityEntityEvent.DespawnEntity = null;
        // GameManager.EntityEntityEvent.BroadCastCalmDown = null;
        // GameManager.EntityEntityEvent.BroadCastStartConversation = null;
        // GameManager.EntityEntityEvent.BroadCastEndConversation = null;
        // GameManager.EntityEntityEvent.BroadCastChase = null;
        // �̺�Ʈ �߰�
        // TO DO ~~~~~~~~~~~~~~
        // GameManager.EntityEntityEvent.SearchEntity += SearchEntity;
        // GameManager.EntityEntityEvent.SpawnEntity += SpawnEntity;
        // GameManager.EntityEntityEvent.DespawnEntity += DespawnEntity;        
        // GameManager.EntityEntityEvent.BroadCastCalmDown+=SendCalmDownMessage;
        // GameManager.EntityEntityEvent.BroadCastStartConversation += SendStartConversationMessage;
        // GameManager.EntityEntityEvent.BroadCastEndConversation += SendEndConversationMessage;
        // GameManager.EntityEntityEvent.BroadCastChase += SendChaseMessage;
    }

    #region Spawn & Search Method
    /// <summary>
    /// ����ü�� ã�� �Լ� (�Ű������� �̸�, ����Ʈ(default/spawn))
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
    /// ����ü�� Ȱ��ȭ �ϴ� �Լ� (�Ű������� �̸�)
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
    /// ����ü�� ��Ȱ��ȭ �ϴ� �Լ� (�Ű������� �̸�)
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
    /// ���� ������ ���·� (�޽İ��� ���� ��)
    /// </summary>
    public void SendCalmDownMessage()
    {
        // TO DO ~~~~~~~~~~
        // GameManager.EntityEntityEvent.IsChaseDown = false;
        int count = defaultEntityList.Count;
        for (int idx = 0; idx < count; idx++)
        {
            defaultEntityList[idx].BeCalmDown();
        }
    }
    /// <summary>
    /// �ϳ��� ��ü �ܿ��� ��� ħ�����·�
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
    /// ��ȭ ��, ��ȭ�ϴ� ��ü �ܿ��� ��� ħ�����·�
    /// </summary>
    /// <param name="_name"></param>
    public void SendStartConversationMessage(string _name)
    {
        SendSilentMessage(_name);
    }
    /// <summary>
    /// ��ȭ ���� �� (�߰��� ���ϴ� ���¶��, �������� ħ�� / �߰��� �ƴ϶�� ��� �����ɻ��·�)
    /// </summary>
    /// <param name="_name"></param>
    public void SendEndConversationMessage(string _name)
    {
        // TO DO ~~~~~~~~~~~~
        // if (GameManager.EntityEntityEvent.IsChaseDown)
        //     return;
        // else
            SendCalmDownMessage();
    }
    /// <summary>
    /// �߰� �� ���� (�߰��ϴ� ��ü �ܿ��� ��� ħ������)
    /// </summary>
    /// <param name="_name"></param>
    public void SendChaseMessage(string _name)
    {
        //  TO DO ~~~~~~~~~~~~
        // GameManager.EntityEntityEvent.IsChaseDown = true;
        SendSilentMessage(_name);
    }
    #endregion
}
