using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] BaseEntity baseEntity;
    public PlaceTriggerType currentPlace = PlaceTriggerType.StudyRoom_1F;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //baseEntity.EntityTriggerEvent(true);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //baseEntity.EntityTriggerEvent(false);
        }
    }
}
