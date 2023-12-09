using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    public int ID { get; set; }
    protected float lookSpeed = 2f;
    protected float sightDistance = 10f;
    protected LayerMask playerMask = 1<<3;
    public GameObject playerObject;
    protected Vector3 initPosition;
    protected Vector3 initRotation;
    #endregion

    public virtual void Setup(MonsterData.MonsterStat stat) {}
    public abstract void UpdateBehavior();
    public virtual void RestInteraction() { } 
    public virtual void ConversationInteraction() { } 
    public virtual void SuccessInteraction() { } 
    public virtual void FailInteraction() { } 
    public virtual void ChaseInteraction() { }
    public bool DetectPlayer()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        float dist = interV.magnitude;
        if (dist <= sightDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, interV, out hit, sightDistance))
            {
                if (hit.collider.CompareTag("Player"))
                    return true;
            }
        }
        return false;
    }

    public bool CheckDistance()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude <= sightDistance)
            return true;
        else
            return false;
    }
}
