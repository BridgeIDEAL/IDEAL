using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    protected float rotateSpeed = 3f;
    protected float marginalAngle = 1f;
    public bool isLookPlayer { get; set; } = true;
    public bool isLookOriginal { get; set; } = false;
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
    public virtual void LookPlayer() { } // �÷��̾�� ��ȭ ��
    public virtual void LookOriginal() { } // �÷��̾�� ��ȭ ���� �� or ��� ���� ��� ��
    #endregion
}
