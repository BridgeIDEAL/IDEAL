using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPlaceTrigger : PlaceTrigger
{
    bool canTrigger = true;
    float timer = 0.0f;
    [SerializeField] float coolTime = 0.0f;
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTrigger)
        {
            IdealSceneManager.Instance.CurrentGameManager.GameEvent_Manager.PlayerInRestPlaceEvent();
            StartCoroutine(CoolTimeTimer());
        }
    }

    private IEnumerator CoolTimeTimer()
    {
        timer = coolTime;
        canTrigger = false;
        while (timer >= 0.0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        canTrigger = true;
    }
}
