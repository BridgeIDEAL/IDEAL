using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetBruiseItem : AbstractInteraction
{
    [SerializeField] private InteractionItemData interactionItemData;
    [SerializeField] private string detectedStr;
    [SerializeField] private string afterInteractionStr = "";
    [SerializeField] private int activationLogNum = -1;
    [SerializeField] private float requiredTime = 1.0f;
    [SerializeField] private int availableCount = 1;
    [SerializeField] Jump2FGirl jump2FGirl;
    public override float RequiredTime { get => requiredTime; }
    protected override string GetDetectedString()
    {
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {
        Inventory.Instance.Add(interactionItemData, 1);
        if (activationLogNum != -1)
        {
            ActivationLogManager.Instance.AddActivationLog(activationLogNum);
        }
        if (afterInteractionStr != "")
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(afterInteractionStr);
        }
        availableCount--;
        if (jump2FGirl == null)
            jump2FGirl = EntityDataManager.Instance.EventTriggerController.GetJumpSpace(0).gameObject.GetComponent<Jump2FGirl>();
        jump2FGirl.CanActive = true;

        if (availableCount < 1)
        {
            Destroy(this.gameObject);
        }
        if (audioSource != null)
        {
            // Inventory.GetItemSound에서 아이템 획득 소리들 처리
        }
    }

    public void ActInteractionKeyBundle()
    {
        Inventory.Instance.Add(interactionItemData, 1);
        availableCount--;
        if (availableCount < 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        this.transform.SetParent(EntityDataManager.Instance.transform);
    }
}
