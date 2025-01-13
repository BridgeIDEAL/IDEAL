using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitSight : EntitySight
{
    [SerializeField] SitDirectionAnim directionAnim;
    public override void BodyToPlayer()
    {
        bool isRight = IsPlayerOnRight();
        switch (directionAnim)
        {
            case SitDirectionAnim.Right:
                anim.SetBool("IsRight", isRight);
                break;
            case SitDirectionAnim.Left:
                anim.SetBool("IsRight", !isRight);
                break;
        }   
    }

    public bool IsPlayerOnRight()
    {
        Vector3 direction = lookTransform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;

        float crossProductY = Vector3.Cross(transform.forward, direction).y;

        if (crossProductY > 0)
            return false;
        return true;
    }

    public void GazeFrontOnGuard()
    {
        StopAllCoroutines();
        SetRigWeight(0, 1);
    }
}

public enum SitDirectionAnim
{
    Right=0,
    Left=1,
}
