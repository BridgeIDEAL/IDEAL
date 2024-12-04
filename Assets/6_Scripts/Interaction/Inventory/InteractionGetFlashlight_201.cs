using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGetFlashlight_201 : AbstractInteraction
{
    [SerializeField] FlashlightItemData flashlightItemData;
    public override float RequiredTime { get => 3.0f;}

    protected override string GetDetectedString(){
        return "Press E, Get Flashlight!";
    }

    protected override void ActInteraction(){
        Inventory.Instance.Add(flashlightItemData, 1);
        //ActivationLogManager.Instance.AddActivationLog(4004);
    }
}
