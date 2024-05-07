using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRoomTrigger : MonoBehaviour
{
    [SerializeField] PatrolGuard pg;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity("Guard").IsInRoom(true);
            pg.IsInRoom(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pg.IsInRoom(false);
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity("Guard").IsInRoom(false);
        }
    }
}
