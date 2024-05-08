using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRoomTrigger : MonoBehaviour
{
    [SerializeField] PatrolGuard patrolGuard;

    private void Awake()
    {
        if (!patrolGuard.IsSpawn())
            this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity("Guard").IsInRoom(true);
            patrolGuard.IsInRoom(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            patrolGuard.IsInRoom(false);
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity("Guard").IsInRoom(false);
        }
    }
}
