using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHealthTest : AbstractInteraction
{
    [SerializeField] private IdealBodyPart idealBodyPart;
    public override float RequiredTime { get => 2.0f;}

    protected override string GetDetectedString(){
        return $"Press E, Hurt {idealBodyPart.ToString()}!";
    }

    protected override void ActInteraction(){
        HealthPointManager.Instance.Hurt(idealBodyPart, 1);
    }
}
