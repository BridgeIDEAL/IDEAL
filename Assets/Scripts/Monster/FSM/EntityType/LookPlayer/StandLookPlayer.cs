using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class StandLookPlayer : LookPlayer
{
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
    }

    public override void GazePlayer(Transform _lookTransform)
    {
        if (lookTransform == null)
            lookTransform = _lookTransform;

        if (IsOverAngle())
        {
            lookDir = LookPlayerDirection.BodyRotate;
            Vector3 directionToPlayer = _lookTransform.position - transform.position;
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
        else if(lookDir == LookPlayerDirection.BodyRotate)
        {
            StartCoroutine(RotateCor(initRotation));
        }

        lookDir = LookPlayerDirection.None;
    }

    public IEnumerator RotateCor(Quaternion _target)
    {
        float timer = 0f;
        while (timer < rotateTime)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, _target, timer / rotateTime);
            yield return null;
        }
        transform.rotation = _target;
    }
}
