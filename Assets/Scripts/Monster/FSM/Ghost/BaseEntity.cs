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
    public virtual void LookPlayer() { } // 플레이어와 대화 시
    public virtual void LookOriginal() { } // 플레이어와 대화 종료 시 or 경계 범위 벗어날 시
    #endregion
}
