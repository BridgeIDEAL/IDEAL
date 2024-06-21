using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class PlaceTrigger : MonoBehaviour
{
    protected abstract void OnTriggerEnter(Collider other);    
    public virtual void ActiveTrigger() { this.enabled = true; }
    public virtual void InActiveTrigger() { this.enabled = false; }
    public virtual void DestroyTrigger() { Destroy(this.gameObject); }
}
