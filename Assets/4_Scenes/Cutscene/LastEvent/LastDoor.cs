using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDoor : LastObjects
{
    Vector3 rotateVec = new Vector3(0,180f,0);
    private void Start()
    {
        if (EventDataManager.Instance.RingAfterSchoolBell && isSameScene)
        {
            transform.rotation = Quaternion.Euler(rotateVec);
        }
    }

    public override void EnableObject()
    {
        base.EnableObject();
        transform.rotation = Quaternion.Euler(rotateVec);
    }
}
