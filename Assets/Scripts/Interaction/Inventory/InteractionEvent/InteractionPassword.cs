using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPassword : AbstractInteraction
{
    EntityEventData eventData;
    #region Struct Data

    [Header("Check Dialogue State")]
    protected string dialogueName = "";
    public bool canTalk = true;
    public string detectedStr = "";
    public override float RequiredTime { get => 1.0f; }
    #endregion

    /****************************************************************************
                                       Chan Method
   ****************************************************************************/
    #region Chan Method : Detect & Interaction
    protected override string GetDetectedString()
    {
        if (detectedStr == "") return "";
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {
        if (!canTalk) return;

        DialogueManager.Instance.Password_UI.ActivePassword();
    }
    #endregion

    /****************************************************************************
           Jun Method : To Do ~~ Check Condition (Check Item)
    ****************************************************************************/
    #region Jun Method 

    private void Start()
    {
        if (EntityDataManager.Instance.HaveEventData(this.gameObject.name))
        {
            eventData = EntityDataManager.Instance.GetEventData("PasswordInteraction");
        }
        else
        {
            EntityEventData _eventData = new EntityEventData(false, "PasswordInteraction");
            EntityDataManager.Instance.AddData(_eventData);
            eventData = _eventData;
        }

        if (eventData.isDoneEvent)
            canTalk = false;
    }

    public void ClearObject()
    {
        eventData.isDoneEvent = true;
    }

    #endregion
}
