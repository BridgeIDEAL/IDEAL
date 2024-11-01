using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastFence : LastObjects
{
    private void Start()
    {
        if (isSameScene && EventDataManager.Instance.RingAfterSchoolBell)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
