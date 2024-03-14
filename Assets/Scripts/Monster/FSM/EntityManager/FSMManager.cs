using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMManager
{
    #region Component 
    private List<BaseEntity> entityList = new List<BaseEntity>(); // ActiveEntity List
    public Dictionary<string, BaseEntity> entityDictionary = new Dictionary<string, BaseEntity>(); // WholeEntity List
    #endregion

    #region Method
    public void Init()
    {
        // Fill List & Dictionary
        BaseEntity[] initEntityArray = GameManager.Instance.variableHub.initMonsterGroup.GetComponentsInChildren<BaseEntity>();
        for (int i = 0; i < initEntityArray.Length; i++) { entityList.Add(initEntityArray[i]); initEntityArray[i].Setup(); }
        BaseEntity[] wholeEntityArray = GameManager.Instance.variableHub.wholeMonsterGroup.GetComponentsInChildren<BaseEntity>(true);
        for (int i = 0; i < wholeEntityArray.Length; i++) { entityDictionary.Add(wholeEntityArray[i].gameObject.name, wholeEntityArray[i]); }

        // Link Entity Event
        GameManager.EntityEvent.StartConversationAction += StartConversationActionUpdate;
        GameManager.EntityEvent.EndConversationAction += EndConversationActionUpdate;
        GameManager.EntityEvent.ChaseAction += ChaseActionUpdate;
        GameManager.EntityEvent.SpawnAction += SpawnMonster;
    }
    
    public void SpawnMonster(string _name)
    {
        if (!entityDictionary[_name].gameObject.activeSelf)
        {
            entityDictionary[_name].gameObject.SetActive(true);
            entityDictionary[_name].Setup();
            entityList.Add(entityDictionary[_name]);
        }
    }

    public void DespawnMonster(string _name)
    {
        if (entityDictionary[_name].gameObject.activeSelf)
        {
            entityDictionary[_name].gameObject.SetActive(false);
            for (int i = 0; i < entityList.Count; i++) { if (entityList[i].gameObject.name == _name) { entityList.RemoveAt(i); return; } }
        }
    }
    
    public BaseEntity SearchEntity(string _name)
    {
        if (entityDictionary[_name].gameObject.activeSelf)
            return entityDictionary[_name];
        else
            return null;
    }

    public void Update() { for (int i = 0; i < entityList.Count; i++) { entityList[i].UpdateBehavior(); }  }
   
    private void StartConversationActionUpdate(string _Key) // 대화 시작 
    {
        for(int i=0; i<entityList.Count; i++)
        {
            if(entityList[i].gameObject.name == _Key)
            {
                entityList[i].StartConversationInteraction();
            }
            else
            {
                entityList[i].SpeechlessInteraction();
            }
        }
    }

    private void EndConversationActionUpdate() // 대화 종료 
    {
        for(int i=0; i<entityList.Count; i++)
        {
            entityList[i].EndConversationInteraction();
        }
    }

    private void ChaseActionUpdate(string _Key) // 상호작용 불가능, 해당 개체 이외엔 다 휴식 상태로 전환
    {

        for (int i = 0; i < entityList.Count; i++)
        {
            if (entityList[i].gameObject.name == _Key)
            {
                entityList[i].ChaseInteraction();
                return;
            }
            //else
            //{
            //    entityList[i].SpeechlessInteraction();
            //}
        }
    }
    #endregion
}
