using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetKeyBundle : AbstractInteraction
{
    [SerializeField] private InteractionItemData interactionItemData;
    [SerializeField] private string detectedStr;
    [SerializeField] private string afterInteractionStr = "";
    [SerializeField] private int activationLogNum = -1;
    [SerializeField] private float requiredTime = 1.0f;
    [SerializeField] private int availableCount = 1;
    public override float RequiredTime { get => requiredTime; }
    public RandomKeySpawn RandomKey_Spawn { get; set; } = null;
    protected override string GetDetectedString()
    {
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {
        Inventory.Instance.Add(interactionItemData, 1);
        if (activationLogNum != -1)
        {
            //ActivationLogManager.Instance.AddActivationLog(activationLogNum);
        }
        if (afterInteractionStr != "")
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(afterInteractionStr);
        }
        availableCount--; 
        if (RandomKey_Spawn != null)
            RandomKey_Spawn.GetKeyBundle(this);
        if (availableCount < 1)
        {
            Destroy(this.gameObject);
        }
        if (audioSource != null)
        {
            // Inventory.GetItemSound���� ������ ȹ�� �Ҹ��� ó��
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
}
