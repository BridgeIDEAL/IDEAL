using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EntitySight : MonoBehaviour
{
    protected Transform lookTransform;
    [Header("Anim")]
    [SerializeField] protected Animator anim;

    [Header("Rig")]
    [SerializeField] protected MultiAimConstraint multiAim;
    [SerializeField] protected RigBuilder rigBuilder;
    [SerializeField, Range(0, 1f)] protected float weightValue = 1f;
    [SerializeField, Range(0,5f)] protected float weightTime;

    [Header("Look Condition")]
    [SerializeField] protected float thresholdAngle = 90f;
    [SerializeField, Range(0f,5f)] protected float bodyTime;
    //[SerializeField] protected Transform frontTransform;

    // Init Rig Component
    public virtual void Init(Transform lookTransform)
    {
        this.lookTransform = lookTransform;
        if(anim==null)
            anim = GetComponent<Animator>();
        if (multiAim == null)
            multiAim = GetComponentInChildren<MultiAimConstraint>();
        if (rigBuilder == null)
            rigBuilder = GetComponent<RigBuilder>();
        rigBuilder.layers[0].rig.weight = 0;
    }

    #region Gaze Method : Front, Player, Angle
    public virtual void HeadToPlayer()
    {
        var sourceObjects = multiAim.data.sourceObjects;
        sourceObjects.Clear();
        multiAim.data.sourceObjects = sourceObjects;
        sourceObjects.Add(new WeightedTransform(lookTransform, weightValue));
        multiAim.data.sourceObjects = sourceObjects;
        rigBuilder.Build();
        StopAllCoroutines();
        SetRigWeight(1, 0);
    }

    // Must Override Sit Entity
    public virtual void BodyToPlayer() { StopAllCoroutines(); StartCoroutine(BodyRotate()); }
    IEnumerator BodyRotate()
    {
        float timer = 0f;
        Quaternion currentRot = transform.rotation;
        Vector3 direction = lookTransform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;

        Quaternion toRot = Quaternion.LookRotation(direction);
        while (timer < bodyTime)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(currentRot, toRot, timer / bodyTime) ;
            yield return null;
        }
        transform.rotation = toRot;
    }

    public virtual void GazeFront()
    {
        if (IsOverAngle())
            return;

        StopAllCoroutines();
        SetRigWeight(0, 1);
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

    public virtual void SetRotate(bool isTalk)
    {
        if (isTalk)
        {
            if (IsOverAngle())
            {
                // 각도가 넘으면 몸이 돌아감
                BodyToPlayer();
            }
            else
            {
                // 각도가 넘지 않으면 머리가 돌아감
                HeadToPlayer();
            }
        }
        else
        {
            // 대화가 끝나면 앞을 보면서 끝남
            GazeFront();
        }
    }
    #endregion

    #region SetWeight : Set Must To Weight (from, time : Not Must)
    public void SetRigWeight(float to, float from=-1f, float time = -1f) { StartCoroutine(RigWeightCor(to, from, time)); }
    IEnumerator RigWeightCor(float to, float from, float time)
    {
        float timer = 0f;
        if (time < 0f)
            time = weightTime; 
        if(from <0f)
            from = rigBuilder.layers[0].rig.weight;
      
        while (timer < time)
        {
            timer += Time.deltaTime;
            rigBuilder.layers[0].rig.weight = Mathf.Lerp(from, to, timer / time);
            yield return null;
        }
        rigBuilder.layers[0].rig.weight = to;
    }
    #endregion
}
