using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    public bool isLookPlayer { get; set; } = true;
    public bool isLookOrigin { get; set; } = false;
    protected float rotateSpeed = 2f;
    protected float marginalAngle = 2f;
    public GameObject playerObject;
    [SerializeField] protected float sightDistance = 10f;
    [SerializeField] protected LayerMask playerMask = 1<<3;
    [SerializeField] protected Vector3 initPosition;
    [SerializeField] protected Vector3 initRotation;
    #endregion

    #region Virtual
    public virtual void Setup(MonsterData.MonsterStat stat) {}
    public virtual void AdditionalSetup() { }
    public abstract void UpdateBehavior();
    public virtual void StartConversationInteraction() { }
    public virtual void EndConversationInteraction() { }
    public virtual void SpeechlessInteraction() { }
    public virtual void ChaseInteraction() { }
    public virtual void LookPlayer() {
        if (isLookPlayer)
        {
            Quaternion targetRotation = Quaternion.LookRotation(playerObject.transform.position - transform.position);
            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            float step = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
            if (angle < marginalAngle)
                isLookPlayer = false;
        }
    }
    public virtual void LookOriginal() {
        if (isLookOrigin)
        {
            Quaternion targetRotation = Quaternion.LookRotation(initPosition - transform.position);
            float angle = Quaternion.Angle(transform.rotation, targetRotation);
            float step = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, step);
            if (angle < marginalAngle)
                isLookOrigin = false;
        }
    }
    #endregion
}
