using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableHub : MonoBehaviour
{

   [Header("EntityManager Variable")]
   public GameObject player;
   public GameObject defaultEntityParent;
   public GameObject spawnEntityParent;
   public List<BaseEntity> defaultEntityList;
   public List<BaseEntity> spawnEntityList;
}
