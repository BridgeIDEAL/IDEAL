using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    public GameObject playerObject;
    [SerializeField] protected float sightDistance = 10f;
    [SerializeField] protected LayerMask playerMask = 1<<3;
    [SerializeField] protected Vector3 initPosition;
    [SerializeField] protected Vector3 initRotation;
    #endregion

    public virtual void Setup(MonsterData.MonsterStat stat) {}
    public abstract void UpdateBehavior();
    public virtual void StartConversationInteraction() { }
    public virtual void EndConversationInteraction() { }
    public virtual void InjureInteraction() { }
    public virtual void ChaseInteraction() { }
    public virtual void SpeechlessInteraction() { }
    public virtual void LookPlayer() { }
    public virtual void LookOriginal() { }
    public virtual void GazeAtInteraction(MonsterData.MonsterStat stat) { }

    public bool CheckDistance()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude <= sightDistance)
            return true;
        else
            return false;
    }
}
