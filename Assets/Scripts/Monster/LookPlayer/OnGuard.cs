using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class OnGuard : MonoBehaviour
{
    Transform playerTransform;
    public Transform PlayerTransform { get { return playerTransform; } set { playerTransform = value; } }
    
    [Header("Rig")]
    [SerializeField] MultiAimConstraint multiAim;
    [SerializeField] RigBuilder rigBuilder;
    [SerializeField, Range(0,1f)] float weightValue;

    public void GazePlayer()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player == null)
                return;
            playerTransform = player.transform;
        }
        var sourceObjects = multiAim.data.sourceObjects;
        sourceObjects.Clear();
        multiAim.data.sourceObjects = sourceObjects;
        sourceObjects.Add(new WeightedTransform(playerTransform, weightValue));
        rigBuilder.Build();
    }

    public void GazeFront()
    {
        var sourceObjects = multiAim.data.sourceObjects;
        sourceObjects.Clear();
        multiAim.data.sourceObjects = sourceObjects;
        rigBuilder.Build();
    }
}
