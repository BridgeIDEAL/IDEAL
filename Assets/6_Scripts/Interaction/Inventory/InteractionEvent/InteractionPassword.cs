using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPassword : AbstractInteraction
{
    EventNames eventName = EventNames.Password_4F;
    EventData eventData = null;
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
        return $"     {detectedStr}";
    }

    protected override void ActInteraction()
    {
        if (!canTalk) return;

        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIInteraction.MakeRedEmoticon(false);

        DialogueManager.Instance.Password_UI.PasswordInteraction = this;
        DialogueManager.Instance.Password_UI.ActivePassword();
    }
    #endregion

    /****************************************************************************
           Jun Method : To Do ~~ Check Condition (Check Item)
    ****************************************************************************/
    #region Jun Method 

    private void Start()
    {
        isRedEmoticon = true;
        string _name = Enums.GetString(eventName);
        EventData _data = EventDataManager.Instance.GetEventData(_name);
        if (_data!=null)
        {
            eventData = _data;
        }
        else
        {
            EventData _eventData = new EventData(_name, false, true);
            EventDataManager.Instance.AddEventData(_name,_eventData);
            eventData = _eventData;
        }

        if (eventData.isDoneEvent)
        {
            this.gameObject.layer = 0;
            canTalk = false;
        }
    }

    public void DisablePassword()
    {
        eventData.isDoneEvent = true;
        canTalk = false;
        this.gameObject.layer = 0;
    }

    #endregion
}
