using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoardTrigger : MonoBehaviour
{
    bool once = true;
    [SerializeField] BlackBoardClean blackBoardClean;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && once)
        {
            once = false;
            blackBoardClean.SetCleanEvent(true);
        }
    }
}
