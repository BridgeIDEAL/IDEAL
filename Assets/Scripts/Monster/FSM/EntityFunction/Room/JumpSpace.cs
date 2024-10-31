using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpace : MonoBehaviour
{
    protected EventData entityEventData;
    public EventData EventData { get { LinkData(); return entityEventData; } set { entityEventData = value; } }

    public void LinkData()
    {
        if (entityEventData != null)
            return;
        if (EntityDataManager.Instance.HaveEventData(this.gameObject.name))
        {
            entityEventData = EntityDataManager.Instance.GetEventData(this.gameObject.name);
        }
        else
        {
            EventData _eventData = new EventData(false, this.gameObject.name);
            EntityDataManager.Instance.AddData(_eventData);
            entityEventData = _eventData;
        }
    }
}
