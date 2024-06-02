using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPlace : RoomTrigger
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IdealSceneManager.Instance.CurrentGameManager.GameEvent_Manager.PlayerInPlace = currentPlace;
            IdealSceneManager.Instance.CurrentGameManager.GameEvent_Manager.PlayerInRestPlaceEvent();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IdealSceneManager.Instance.CurrentGameManager.GameEvent_Manager.PlayerInPlace = EventPlaceType.None;
        }
    }
}
