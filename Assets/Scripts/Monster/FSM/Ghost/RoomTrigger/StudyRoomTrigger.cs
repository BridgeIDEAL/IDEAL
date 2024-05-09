using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyRoomTrigger : MonoBehaviour
{
    [SerializeField] Principal principal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity("Principal").IsInRoom(true);
            principal.IsInRoom(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //IdealSceneManager.Instance.CurrentGameManager.EntityEM.SearchEntity("Principal").IsInRoom(false);
            principal.IsInRoom(false);
        }
    }
}
