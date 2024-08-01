using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashClean : CleanSystem
{
    [SerializeField] string trashObjectName;
    [SerializeField] string brokenKeyName;

    [SerializeField] Transform[] trashObjectsTf;
    void Start()
    {
        string _name = gameObject.name;
        if (EntityDataManager.Instance.HaveEventData(_name))
        {
            eventData = EntityDataManager.Instance.GetEventData(_name);
        }
        else
        {
            eventData = new EntityEventData(false, _name);
            EntityDataManager.Instance.AddData(eventData);
        }

        if (!eventData.isDoneEvent)
        {
            Setting();
        }
        else
        {
            Destroy(interactionClean.gameObject);
        }
    }

    public void Setting()
    {
        // To Do ~~
        trashObjectName = "";

        // Current 
        int randNum = Random.Range(0, trashObjectsTf.Length);
        GameObject go = FabManager.Instance.LoadPrefab(brokenKeyName);
        Instantiate(go, trashObjectsTf[randNum]);
        go.GetComponent<InteractionCleanItem>().Clean = this;
        //go.transform.position = trashObjectsTf[randNum].position;
    }
}
