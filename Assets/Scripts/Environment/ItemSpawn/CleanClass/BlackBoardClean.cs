using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoardClean : CleanSystem
{
    [SerializeField] GameObject[] graffities;

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
            Destroy(graffities[0].transform.parent.gameObject);
        }
    }

    public void Setting()
    {
        int randNum = Random.Range(0, 2);
        graffities[randNum].SetActive(true);
        interactionClean = graffities[randNum].GetComponent<InteractionClean>();
        interactionClean.Clean = this;
        Debug.Log(randNum);
    }
}
