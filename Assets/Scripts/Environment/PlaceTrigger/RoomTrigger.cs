using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    public PlaceTriggerType currentPlace = PlaceTriggerType.StudyRoom_1F;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IdealSceneManager.Instance.CurrentGameManager.GameEvent_Manager.PlayerInPlace = currentPlace;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IdealSceneManager.Instance.CurrentGameManager.GameEvent_Manager.PlayerInPlace = PlaceTriggerType.None;
        }
    }
}
