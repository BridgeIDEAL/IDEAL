using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPickupItem : AbstractInteraction
{
    public InteractionItemData interactionItemData;
    [SerializeField] private string detectedStr;
    [SerializeField] private string afterInteractionStr = "";
    [SerializeField] private int activationLogNum = -1;
    [SerializeField] private float requiredTime = 1.0f;
    [SerializeField] private int availableCount = 1;
    public override float RequiredTime { get => requiredTime; }

    [SerializeField] EventItemNames pickupEventName;

    /// <summary>
    /// Must Call By ItemController
    /// </summary>
    public void Init()
    {
        EventData eventData = EventDataManager.Instance.GetEventData(Enums.GetString(pickupEventName));
        if (eventData != null)
        {
            if(eventData.isDoneEvent)
                this.gameObject.SetActive(false);
            else
            {
                transform.position = eventData.position;
                if (eventData.isOccurEvent)
                    this.gameObject.SetActive(false);
                else
                    this.gameObject.SetActive(true);
            }
        }
        this.gameObject.SetActive(false);
    }

    protected override string GetDetectedString()
    {
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {
        Inventory.Instance.Add(interactionItemData, 1);
        if (audioSource != null)
        {
            audioSource.Play();
        }
        if (activationLogNum != -1)
        {
            //ActivationLogManager.Instance.AddActivationLog(activationLogNum);
        }
        if (afterInteractionStr != "")
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(afterInteractionStr);
        }
        availableCount--;

        if (availableCount < 1)
        {
            if (pickupEventName == EventItemNames.GetMedicine)
                BruiseItemGetEvent();
            EventDataManager.Instance.GetEventData(Enums.GetString(pickupEventName)).isDoneEvent = true;
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        if (audioSource != null)
        {
            // Inventory.GetItemSound에서 아이템 획득 소리들 처리
        }
    }

    public void BruiseItemGetEvent()
    {
        Jump2FGirl jump2FGirl = null;
        jump2FGirl = EntityDataManager.Instance.EventTriggerController.GetJumpSpace(0).gameObject.GetComponent<Jump2FGirl>();
        if (jump2FGirl == null)
            return;
        jump2FGirl.CanActive = true;
    }
}
