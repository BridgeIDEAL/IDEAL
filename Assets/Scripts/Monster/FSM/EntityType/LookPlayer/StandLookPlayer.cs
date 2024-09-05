using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Events;

public class StandLookPlayer : LookPlayer
{
    UnityAction<Transform> lookAction = null;

    Transform playerTransform;
    Quaternion initRotation;
    LookPlayerDirection lookDir = LookPlayerDirection.None;
    [Header("Body Rotate Time")]
    float rotateTime =1;

    public enum LookPlayerDirection
    {
        None,
        HeadRotate,
        BodyRotate
    }

    protected override void Awake()
    {
        base.Awake();
        initRotation = transform.rotation;

        lookAction = null;
        lookAction += GazeTarget;
    }

    public override void GazePlayer(Transform _lookTransform)
    {
        if (lookTransform == null)
            lookTransform = _lookTransform;
        if (playerTransform == null)
            playerTransform = EntityDataManager.Instance.Controller.PlayerTransform;

        if (IsOverAngle())
        {
            lookDir = LookPlayerDirection.BodyRotate;
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            StartCoroutine(RotateCor(targetRotation));
        }
        else
        {
            lookDir = LookPlayerDirection.HeadRotate;
            SetRigWeight(1);
            var sourceObjects = multiAim.data.sourceObjects;
            sourceObjects.Clear();
            multiAim.data.sourceObjects = sourceObjects;
            sourceObjects.Add(new WeightedTransform(lookTransform, weightValue));
            multiAim.data.sourceObjects = sourceObjects;
            rigBuilder.Build();
        }
    }

    public override void GazeFront()
    {
        if (lookDir == LookPlayerDirection.HeadRotate)
        {
            SetRigWeight(0);
            //var sourceObjects = multiAim.data.sourceObjects;
            //sourceObjects.Clear();
            //multiAim.data.sourceObjects = sourceObjects;
            //rigBuilder.Build();
        }
        else if (lookDir == LookPlayerDirection.BodyRotate)
        {
            StartCoroutine(RotateCor(initRotation));
        }

        lookDir = LookPlayerDirection.None;
    }

    public IEnumerator RotateCor(Quaternion _target, UnityAction<Transform> _action= null)
    {
        float timer = 0f;
        while (timer < rotateTime)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, _target, timer / rotateTime);
            yield return null;
        }
        transform.rotation = _target;

        if (_action != null)
            _action(Target);
    }

    public Transform Target { get; set; } = null;

    public void GazeTarget(Transform _target)
    {
        lookTransform = _target;
        lookDir = LookPlayerDirection.HeadRotate;
        SetRigWeight(1);
        var sourceObjects = multiAim.data.sourceObjects;
        sourceObjects.Clear();
        multiAim.data.sourceObjects = sourceObjects;
        sourceObjects.Add(new WeightedTransform(lookTransform, weightValue));
        multiAim.data.sourceObjects = sourceObjects;
        rigBuilder.Build();
    }

    public void GazeDefaultTarget(Quaternion _targetRotate, Transform _target)
    {
        Target = _target;
        StartCoroutine(RotateCor(_targetRotate, lookAction));
    }
}
