using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerRoom3F : MonoBehaviour
{

    public bool IsOnGuardEvent = false;
    public bool IsInEventRoom = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInEventRoom = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && IsInEventRoom)
        {
            if (IsOnGuardEvent)
            {
                Debug.Log("∞Ê∞Ë ¡ﬂ ¿Ã≈ª~~~");
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
