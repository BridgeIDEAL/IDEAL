using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    #region Variable Player Place
    public PlaceTriggerType PlayerInPlace { get; set; } = PlaceTriggerType.None;
    #endregion

    public void PlayerInRestPlaceEvent()
    {
        // 휴식 공간 진입 시
    }

    /// <summary>
    ///  Call when you spawn TriggerArea or Item
    /// </summary>
    /// <param name="_fabName"></param>
    /// <param name="_pos"></param>
    /// <param name="_type"></param>
    public void FabSpawn(string _fabName, Vector3 _pos, SpawnObjectType _type)
    {
        switch (_type)
        {
            case SpawnObjectType.Item:
                GameObject item = Instantiate(Resources.Load<GameObject>($"Item/{_fabName}"));
                item.transform.position = _pos;
                break;
            case SpawnObjectType.Area:
                GameObject area = Instantiate(Resources.Load<GameObject>($"Area/{_fabName}"));
                area.transform.position = _pos;
                break;
            default:
                break;
        }
    }
}
