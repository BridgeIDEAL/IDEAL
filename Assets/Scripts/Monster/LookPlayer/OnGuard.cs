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
    [SerializeField, Range(0,1f)] float weightValue =1f;

    public void GazePlayer(Transform _playerTransform)
    {
        if (playerTransform == null)
            playerTransform = _playerTransform;
        var sourceObjects = multiAim.data.sourceObjects;
        sourceObjects.Clear();
        multiAim.data.sourceObjects = sourceObjects;
        sourceObjects.Add(new WeightedTransform(playerTransform, weightValue));
        multiAim.data.sourceObjects = sourceObjects;
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
