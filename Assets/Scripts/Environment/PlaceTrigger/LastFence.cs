using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastFence : MonoBehaviour
{
    private void Awake()
    {
        if (EntityDataManager.Instance.IsLastEvent)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }
}
