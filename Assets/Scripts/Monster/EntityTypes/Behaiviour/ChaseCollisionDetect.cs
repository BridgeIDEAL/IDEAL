using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCollisionDetect : MonoBehaviour 
{
    bool isNearPlayer = false;
    Transform playerTransform = null; // Chased
    Transform entityTransform = null; // Chasing

    int structLayer = 1 << 10;
    [Header("Collision Detect Value")]
    [SerializeField] float forwardDelta = 3.5f;
    [SerializeField] Vector3 structCollisionBox = new Vector3(2f, 1f, 3.5f);

    public void Init(Transform playerTransform, Transform entityTransform)
    {
        this.playerTransform = playerTransform;
        this.entityTransform = entityTransform;
    }

    public bool IsCollidePlayer()
    {
        if (!isNearPlayer)
            return false;

        return IsBetweenStruct();
    }
    
    public bool IsBetweenStruct()
    {
        if (Physics.CheckBox(entityTransform.position + entityTransform.forward * forwardDelta/2, structCollisionBox / 2, Quaternion.identity, structLayer))    
            return false;
        return true;
    }

    #region Trigger Detect
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
    #endregion
}
