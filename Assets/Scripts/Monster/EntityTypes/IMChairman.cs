using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMChairman : IMStandEntity
{
    public override void Setup()
    {
        entity_Data = EntityDataManager.Instance.GetEntityData(gameObject.name);
        if (entity_Data == null)
        {
            Debug.LogError("해당 이형체의 정보를 찾을 수 없습니다!");
            return;
        }
        
        if (CountAttempts.Instance.GetAttemptCount() == 1 && entity_Data.isSpawn)
            controller.ActiveEntity(entity_Data.speakerName);
        else
            SetActiveState(false);

        AdditionalSetup();
    }
}
