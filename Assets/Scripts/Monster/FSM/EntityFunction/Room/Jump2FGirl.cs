using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump2FGirl : JumpSpace
{
    bool once = true;
    public bool CanActive { get; set; } = false;
    private void Start()
    {
        if (EventData.isDoneEvent)
            gameObject.SetActive(false);
    }

    public void ActiveEvent()
    {
        EventData.isDoneEvent = true;
        EntityDataManager.Instance.Controller.ActiveChaseEntity(ChaseEventType.Jump2F_GirlStudent);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && once && CanActive)
        {
            once = false;
            ActiveEvent();
            gameObject.SetActive(false);
        }
    }
}
