using System.Collections.Generic;
using UnityEngine;

public class EntityEvent : MonoBehaviour
{
   
   public void EntityIdle()
   {
      Dictionary<int, BaseEntity> dic = new Dictionary<int, BaseEntity>();
      dic=EntityDataBase.Instance.GetDictionary();
      foreach (KeyValuePair<int,BaseEntity>entity in dic)
      {
         entity.Value.patrolSpeed = 10;
      }
   }
}
