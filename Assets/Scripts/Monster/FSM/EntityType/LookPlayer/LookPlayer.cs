using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookPlayer : MonoBehaviour
{
    protected Transform lookTransform;
    public Transform LookTransform { get { return lookTransform; } set { lookTransform = value; } }

    [Header("Rig")]
    [SerializeField] protected MultiAimConstraint multiAim;
    [SerializeField] protected RigBuilder rigBuilder;
    [SerializeField, Range(0, 1f)] protected float weightValue = 1f;

    [Header("Look Condition")]
    [SerializeField] protected float thresholdAngle = 90f;

    protected virtual void Awake()
    {
        if (multiAim == null)
            multiAim = GetComponentInChildren<MultiAimConstraint>();
        if (rigBuilder == null)
            rigBuilder = GetComponent<RigBuilder>();
        rigBuilder.layers[0].rig.weight = 0;
    }

    public virtual void GazePlayer(Transform _lookTransform)
    {
        if (lookTransform == null)
            lookTransform = _lookTransform;
        var sourceObjects = multiAim.data.sourceObjects;
        sourceObjects.Clear();
        multiAim.data.sourceObjects = sourceObjects;
        sourceObjects.Add(new WeightedTransform(lookTransform, weightValue));
        multiAim.data.sourceObjects = sourceObjects;
        rigBuilder.Build();
    }

    public virtual void GazeFront()
    {
        var sourceObjects = multiAim.data.sourceObjects;
        sourceObjects.Clear();
        multiAim.data.sourceObjects = sourceObjects;
        rigBuilder.Build();
    }

    public bool IsOverAngle()
    {
        Vector3 directionToPlayer = lookTransform.position - transform.position;
        directionToPlayer.y = 0;
        Vector3 monsterForward = transform.forward;
        monsterForward.y = 0;

        // Same Direction = 0, Reverse Direction = 180 (Return Only 0~180)
        float angle = Vector3.Angle(monsterForward, directionToPlayer);
        if (angle > thresholdAngle)
            return true;
        return false;
    }

    #region SetWeight
    public void SetRigWeight(float _toWeight) { StartCoroutine(RigWeightCor(_toWeight)); }

    public IEnumerator RigWeightCor(float _toWeight)
    {
        float timer = 0f;
        float currentWeight = rigBuilder.layers[0].rig.weight;
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            rigBuilder.layers[0].rig.weight = Mathf.Lerp(currentWeight, _toWeight, timer / 2f);
            yield return null;
        }
        rigBuilder.layers[0].rig.weight = _toWeight;
    }

    public void SetRigWeight(float _from,float _to) { StartCoroutine(RigWeightCor(_from,_to)); }

    public IEnumerator RigWeightCor(float _from, float _to)
    {
        float timer = 0f;
        float currentWeight = _from;
        rigBuilder.layers[0].rig.weight = _from;
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            rigBuilder.layers[0].rig.weight = Mathf.Lerp(currentWeight, _to, timer / 2f);
            yield return null;
        }
        rigBuilder.layers[0].rig.weight = _to;
    }
    #endregion
}

//bool isRotate = false;
//public void MaintainAngle()
//{
//    Vector3 directionToPlayer = playerTransform.position - transform.position;
//    directionToPlayer.y = 0; 
//    Vector3 monsterForward = transform.forward;
//    monsterForward.y = 0; 

//    float angle = Vector3.Angle(monsterForward, directionToPlayer);
//    if (angle > thresholdAngle && !isRotate)
//    {
//        isRotate = true;
//        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
//        StartCoroutine(RotateCor(targetRotation));
//    }
//}

//public IEnumerator RotateCor(Quaternion _target)
//{
//    float timer = 0f;
//    while (timer < 1f)
//    {
//        timer += Time.deltaTime;
//        transform.rotation = Quaternion.Slerp(transform.rotation, _target, timer/1f);
//        yield return null;
//    }
//    isRotate = false;
//}
