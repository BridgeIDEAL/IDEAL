using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use TimeLine or EntityEvent or Camera
/// </summary>
public class GameEventManager
{
    #region Variable Player Place
    public PlaceTriggerType PlayerInPlace { get; set; } = PlaceTriggerType.None;
    #endregion

    public void PlayerInRestPlaceEvent()
    {
        // 휴식 공간 진입 시
    }
}
