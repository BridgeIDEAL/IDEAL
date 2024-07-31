using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetClassCabinet : MonoBehaviour
{
    [SerializeField] List<ClassroomCabinet> cabinetList = new List<ClassroomCabinet>();
    [SerializeField] ClassCabinetSpawnItem cabinetItemType;
    EntityEventData eventData;

    private void Start()
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

        if (eventData.isDoneEvent)
        {
            int cabinetCnt = cabinetList.Count;
            for (int i = 0; i <cabinetCnt;  i++)
            {
                cabinetList[i].RemoveThis();
            }
        }
        else
        {
            SetOpenCabbinet();
        }
    }

    public void SetOpenCabbinet()
    {
        int cabinetCnt = cabinetList.Count;
        int randomNum = Random.Range(0, cabinetCnt);
        cabinetList[randomNum].SpawnNameTag(this, cabinetItemType);
        for (int i = 0; i<cabinetCnt; i++)
        {
            if (i == randomNum)
                continue;
            cabinetList[i].RemoveThis();
        }
        cabinetList.Clear();
    }


    public void DoneEvent()
    {
        eventData.isDoneEvent = true;
    }
}
