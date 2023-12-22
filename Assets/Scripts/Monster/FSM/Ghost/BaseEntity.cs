using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    public string monsterName;
    protected float sightDistance = 10f;
    protected LayerMask playerMask = 1<<3;
    public GameObject playerObject;
    protected Vector3 initPosition;
    protected Vector3 initRotation;
    #endregion

    public virtual void Setup(MonsterData.MonsterStat stat) {}
    public abstract void UpdateBehavior();
    public virtual void RestInteraction() { } 
    public virtual void StartConversationInteraction() { }
    public virtual void EndConversationInteraction() { }
    public virtual void ChaseInteraction() { }
    public virtual void SpeechlessInteraction() { }
    //public bool DetectPlayer()
    //{
    //    Vector3 interV = playerObject.transform.position - transform.position;
    //    float dist = interV.magnitude;
    //    if (dist <= sightDistance)
    //    {
    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, interV, out hit, sightDistance))
    //        {
    //            if (hit.collider.CompareTag("Player"))
    //                return true;
    //        }
    //    }
    //    return false;
    //}

    public bool CheckDistance()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude <= sightDistance)
            return true;
        else
            return false;
    }
    public void LookPlayer()
    {
        Vector3 dir = playerObject.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
    }

    public void LookOriginal()
    {
        Quaternion originalDir = Quaternion.Euler(initRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, originalDir, 5 * Time.deltaTime);
        //transform.rotation = Quaternion.Slerp(transform.rotation, , 5 * Time.deltaTime);
    }
}
