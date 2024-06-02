using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    #region Variable Player Place
    public EventPlaceType PlayerInPlace { get; set; } = EventPlaceType.None;
    #endregion

    public void PlayerInRestPlaceEvent()
    {
        // 휴식 공간 진입 시
    }
}
