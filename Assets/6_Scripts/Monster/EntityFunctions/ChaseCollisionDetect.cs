using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ChaseCollisionDetect : MonoBehaviour 
{
    Transform playerTransform = null; // Chased
    
    int structLayer = 1 << 10;
    int playerLayer = 1 <<3;
    [Header("Collision Detect Value")]
    float catchDistance = 1.5f;
    [SerializeField] Transform bodyTransform;
    public void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public bool IsCollidePlayer()
    {
        if (!CheckNearPlayer())
            return false;

        return IsBetweenStruct();
    }
    
    public bool IsBetweenStruct()
    {
        Vector3 direction = (playerTransform.position+Vector3.up*1.5f) - bodyTransform.position;
        if (Physics.Raycast(bodyTransform.position, direction.normalized, catchDistance, structLayer))
            return false;
        return true;
    }

    public bool CheckNearPlayer()
    {
        if(Physics.SphereCast(bodyTransform.position, catchDistance, transform.forward, out RaycastHit hit, catchDistance , playerLayer))
            return true;
        return false;
    }
}
