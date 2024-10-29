using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCollision 
{
    Transform playerTransform = null; // Chased
    Transform entityTransform = null; // Chasing
    float collisionDistance = 0;

    // Caching Value
    int structLayer = 1 << 10;
    float forwardDelta = 1.75f;
    Vector3 structCollisionBox = new Vector3(2f, 1f, 3.5f);
    public ChaseCollision(float collisionDistance, Transform playerTransform, Transform entityTransform)
    {
        this.collisionDistance = collisionDistance;
        this.playerTransform = playerTransform;
        this.entityTransform = entityTransform;
    }

    public bool IsCollidePlayer()
    {
        if (!IsNearPlayer())
            return false;

        return IsBetweenStruct();
    }

    public bool IsNearPlayer()  { return Vector3.Distance(playerTransform.position, entityTransform.position) < collisionDistance ? true : false;  }
    
    public bool IsBetweenStruct()
    {
        if (Physics.CheckBox(entityTransform.position + entityTransform.forward * forwardDelta, structCollisionBox / 2, Quaternion.identity, structLayer))
            return false;
        return true;
    }
}
