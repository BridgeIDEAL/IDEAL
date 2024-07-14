using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookPlayer : MonoBehaviour
{
    [SerializeField] Rig rig;
    [SerializeField] MultiAimConstraint mc;
    [SerializeField] Transform ptf;
    [SerializeField] RigBuilder rb;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            var so = mc.data.sourceObjects;
            so.Clear();
            mc.data.sourceObjects = so;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            var so = mc.data.sourceObjects;
            so.Add(new WeightedTransform(ptf,1f));
            mc.data.sourceObjects = so;
            rb.Build();
        }
    }
}
