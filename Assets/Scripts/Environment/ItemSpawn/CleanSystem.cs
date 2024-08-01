using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanSystem : MonoBehaviour
{
    [SerializeField] protected InteractionClean interactionClean;
    protected EntityEventData eventData;
    public virtual void DoneEvent()
    {
        eventData.isDoneEvent = true;
    }
}
