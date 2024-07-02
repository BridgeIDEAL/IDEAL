using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesController : MonoBehaviour
{
    int entityListCnt = 0;
    int activeEntityListCnt = 0;
    [SerializeField] List<BaseEntity> entityList = new List<BaseEntity>();
    List<BaseEntity> activeEntityList = new List<BaseEntity>();

    #region Unity Life Cycle
    // Start
    void Start()
    {
        InitActiveEntityList();
    }

    public void InitActiveEntityList()
    {
        // ���Ŀ� entity���� �����͸� ���� ���� ���θ� Ȯ���ϰ� ����Ʈ�� ����
        entityListCnt = entityList.Count;
        for (int i = 0; i < entityListCnt; i++)
        {
            activeEntityList.Add(entityList[i]);
        }

        // ���Ŀ� BaseEntity ��ũ��Ʈ �ۼ� ��, Init ȣ�����ֱ�
        activeEntityListCnt = activeEntityList.Count;
        for (int i = 0; i < activeEntityListCnt; i++)
        {
            //activeEntityList[i].Init();
        }
    }

    // Update
    void Update()
    {
        ExecuteActiveEntity();
    }

    public void ExecuteActiveEntity()
    {
        // ���Ŀ� BaseEntity ��ũ��Ʈ �ۼ� ��, Execute ȣ�����ֱ�
        for (int i = 0; i < activeEntityListCnt; i++)
        {
            //activeEntityList[i].Execute();
        }
    }
    #endregion

    public void ActiveEntity(string _entityIndex)
    {
        for (int i = 0; i < entityListCnt; i++)
        {
            //if () ���̵�� ������ �߰�
            //{
            //    activeEntityList.Add(entityList[i]);
            //}
        }
    }

    public void InactiveEntity(string _entityIndex)
    {
        for (int i = 0; i < activeEntityListCnt; i++)
        {
            //if () ���̵�� ������ ����
            //{
            //    activeEntityList.RemoveAll(i);
            //}
        }
    }

    // ��� ���� �ൿ�� ��ų ��
    public void SendMessage(EntityStateType _all)
    {
        for(int i=0; i<activeEntityListCnt; i++)
        {
            activeEntityList[i].ReceiveMessage(_all);
        }
    }

    // Ư�� �� ��ü�� �ٸ� �ൿ�� ��ų ��
    public void SendMessage(string _entityIndex, EntityStateType _one, EntityStateType _allButOne)
    {
        for (int i = 0; i < activeEntityListCnt; i++)
        {
            if(activeEntityList[i].name==_entityIndex)
                activeEntityList[i].ReceiveMessage(_one);
            else
                activeEntityList[i].ReceiveMessage(_allButOne);
        }
    }
}