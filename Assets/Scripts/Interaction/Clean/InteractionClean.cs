using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionClean : AbstractInteraction
{
    [SerializeField] private string detectedStr;
    [SerializeField] private string afterInteractionStr = "";
    [SerializeField] private int activationLogNum = -1;
    [SerializeField] private float requiredTime = 1.0f;
    [SerializeField] private int availableCount = 1;
    public override float RequiredTime { get => requiredTime; }
    public Classroom classRoom { get; set; } = null;
         
    protected override string GetDetectedString()
    {
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        if (activationLogNum != -1)
        {
            ActivationLogManager.Instance.AddActivationLog(activationLogNum);
        }
        if (afterInteractionStr != "")
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(afterInteractionStr);
        }
        availableCount--;
        if (availableCount < 1)
        {
            // Call when you complete clean classroom
            if (classRoom != null)
                classRoom.CompleteClean();
            Destroy(this.gameObject);
        }
        if (audioSource != null)
        {
            // Inventory.GetItemSound에서 아이템 획득 소리들 처리
        }
    }
}
