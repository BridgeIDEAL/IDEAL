using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetKeyBundle : AbstractInteraction
{
    [SerializeField] protected InteractionItemData interactionItemData;
    [SerializeField] protected string detectedStr;
    [SerializeField] protected string afterInteractionStr = "";
    [SerializeField] protected int activationLogNum = -1;
    [SerializeField] protected float requiredTime = 1.0f;
    [SerializeField] protected int availableCount = 1;
    public override float RequiredTime { get => requiredTime; }
    public KeyBoxSpawn KeyBox_Spawn { get; set; } = null;
    protected override string GetDetectedString()
    {
        return $"<sprite=0> {detectedStr}";
    }

    protected override void ActInteraction()
    {
        Inventory.Instance.Add(interactionItemData, 1);
        ProgressManager.Instance.SetItemLog(interactionItemData.ID, 1);
        if (activationLogNum != -1)
        {
            //ActivationLogManager.Instance.AddActivationLog(activationLogNum);
        }
        if (afterInteractionStr != "")
        {
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.interactionManager.uIInteraction.GradientText(afterInteractionStr);
        }
        availableCount--; 
        if (KeyBox_Spawn != null)
            KeyBox_Spawn.GetKeyBundle(this);
        if (availableCount < 1)
        {
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
        if (audioSource != null)
        {
            // Inventory.GetItemSound���� ������ ȹ�� �Ҹ��� ó��
        }
    }

    void Awake(){
        if(ProgressManager.Instance.GetItemLogExist(interactionItemData.ID)){
            this.gameObject.SetActive(false);
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
