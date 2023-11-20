using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetPortion_101 : AbstractInteraction
{
    [SerializeField] PortionItemData portionItemData;
    public override float RequiredTime { get => 2.0f;}

    protected override string GetDetectedString(){
        return "Press E, Get Portion!";
    }

    protected override void ActInteraction(){
        Inventory.Instance.Add(portionItemData, 1);
        ActivationLogManager.Instance.AddActivationLog(4002);
    }
}
