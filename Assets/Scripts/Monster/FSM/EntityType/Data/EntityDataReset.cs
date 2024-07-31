using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDataReset : MonoBehaviour
{
    void Start()
    {
        EntityDataManager.Instance.ResetData();       
    }
}
