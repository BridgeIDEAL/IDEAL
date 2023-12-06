using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    public int ID { get; set; }
    //public bool CanInteraction { get; set; } = true;
    public float sightDistance;
    public float sightAngle;
    protected LayerMask playerMask = 1<<3;
    public GameObject playerObject;
    protected float lookSpeed = 2f;
    #endregion

    public virtual void Setup(MonsterData.MonsterStat stat) {}
    public abstract void UpdateBehavior();
    public virtual void RestInteraction() { } // 휴식 공간에 들어갔을 때
    public virtual void ConversationInteraction() { } // 상호작용을 시작했을 때
    public virtual void SuccessInteraction() { } // 상호작용을 성공했을 때
    public virtual void FailInteraction() { } // 상호작용을 실패했을 때 (정신력 감소)
    public virtual void ChaseInteraction() { } // 상호작용을 실패했을 때 (도망쳐야 할 때)
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
