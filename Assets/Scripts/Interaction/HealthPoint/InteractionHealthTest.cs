using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class InteractionHealthTest : AbstractInteraction
{
    [SerializeField] private IdealBodyPart idealBodyPart;

    protected override string GetDetectedString(){
        return $"Press E, Hurt {idealBodyPart.ToString()}!";
    }

    protected override void ActInteraction(){
        HealthPointManager.Instance.Hurt(idealBodyPart, 1);
    }
}
