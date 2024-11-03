using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ChaseCollisionDetect : MonoBehaviour 
{
    bool isNearPlayer = false;
    Transform playerTransform = null; // Chased
    
    int structLayer = 1 << 10;
    [Header("Collision Detect Value")]
    [SerializeField] float forwardDelta = 3.5f;

    public void Init(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    public bool IsCollidePlayer()
    {
        if (!isNearPlayer)
            return false;

        return IsBetweenStruct();
    }
    
    public bool IsBetweenStruct()
    {
        Vector3 direction = (playerTransform.position+Vector3.up) - transform.position;
        direction = direction.normalized;

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, forwardDelta, structLayer))
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
