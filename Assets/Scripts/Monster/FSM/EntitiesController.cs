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
        // 추후에 entity들의 데이터를 통해 스폰 여부를 확인하고 리스트에 삽입
        entityListCnt = entityList.Count;
        for (int i = 0; i < entityListCnt; i++)
        {
            activeEntityList.Add(entityList[i]);
        }

        // 추후에 BaseEntity 스크립트 작성 후, Init 호출해주기
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
        // 추후에 BaseEntity 스크립트 작성 후, Execute 호출해주기
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
            //if () 아이디와 같으면 추가
            //{
            //    activeEntityList.Add(entityList[i]);
            //}
        }
    }

    public void InactiveEntity(string _entityIndex)
    {
        for (int i = 0; i < activeEntityListCnt; i++)
        {
            //if () 아이디와 같으면 삭제
            //{
            //    activeEntityList.RemoveAll(i);
            //}
        }
    }

    // 모두 같은 행동을 시킬 때
    public void SendMessage(EntityStateType _all)
    {
        for(int i=0; i<activeEntityListCnt; i++)
        {
            activeEntityList[i].ReceiveMessage(_all);
        }
    }

    // 특정 한 개체만 다른 행동을 시킬 때
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