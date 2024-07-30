using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SitLookPlayer : LookPlayer
{
    public override void GazePlayer(Transform _lookTransform)
    {
        if (lookTransform == null)
            lookTransform = _lookTransform;

        if (IsOverAngle())
            return;
        SetRigWeight(1);
        var sourceObjects = multiAim.data.sourceObjects;
        sourceObjects.Clear();
        multiAim.data.sourceObjects = sourceObjects;
        sourceObjects.Add(new WeightedTransform(lookTransform, weightValue));
        multiAim.data.sourceObjects = sourceObjects;
        rigBuilder.Build();
    }

    public override void GazeFront()
    {
        SetRigWeight(0);
        //var sourceObjects = multiAim.data.sourceObjects;
        //sourceObjects.Clear();
        //multiAim.data.sourceObjects = sourceObjects;
        //rigBuilder.Build();
    }
}
