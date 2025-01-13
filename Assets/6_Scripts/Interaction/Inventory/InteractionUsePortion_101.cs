using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUsePortion_101 : AbstractInteraction
{
    [SerializeField] GameObject cubeObject;
    public override float RequiredTime { get => 2.0f;}

    protected override string GetDetectedString(){
        return "Press E, Use Portion!";
    }

    protected override void ActInteraction(){
        if(Inventory.Instance.UseItemWithItemCode(101)){
            cubeObject.SetActive(true);
        }
    }
}
