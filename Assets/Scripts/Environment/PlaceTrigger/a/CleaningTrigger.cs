using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningTrigger : RoomTrigger
{
    public bool IsCleanRoom { get; set; } = false;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!IsCleanRoom)
                Debug.Log("û�� ���� ���Ƽ �߻�!!!!");
            Destroy(this.gameObject);
        }
    }
}
