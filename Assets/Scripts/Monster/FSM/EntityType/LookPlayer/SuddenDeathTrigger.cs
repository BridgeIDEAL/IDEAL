using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuddenDeathTrigger : MonoBehaviour
{
    OnGuardEntity guardEntity =null;
    public void ActiveThis(bool _isActive, OnGuardEntity _guardEntity = null)
    {
        this.gameObject.SetActive(_isActive);
        if (_isActive)
            this.guardEntity = _guardEntity;
        else
            this.guardEntity = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (guardEntity == null)
            return;

        if (other.CompareTag("Player"))
        {
            guardEntity.SpawnFrontPlayer();
        }
    }
}
