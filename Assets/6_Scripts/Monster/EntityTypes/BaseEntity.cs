using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Value 
    // Later Change Layer : Prevent Talk Overlap
    protected LayerMask defaultLayer = 0;

    // Transform : EntitiyHead N Player  
    [SerializeField] protected Transform headTransfrom;
    public Transform HeadTransform { get { return headTransfrom; } }
    protected Transform playerTransform;
    protected Transform playerHeightTransform;
    
    // Entity State 
    protected EntityStateType currentType;
    public EntityStateType CurrentType { get { return currentType; } }

    // Entity Data : Relate Spawn, Dialogue Index
    protected Entity entity_Data =null;
    public Entity Entity_Data { get { if (entity_Data == null) EntityDataManager.Instance.GetEntityData(gameObject.name);  return entity_Data;  }  set  {  entity_Data = value;  } }
    
    // Entity Controller 
    protected EntitiesController controller;
    public EntitiesController Controller { get { return controller; } set { controller = value; } }

    // Entity Dialogue
    protected Dialogue entity_Dialogue =null;
    public Dialogue Entity_Dialogue { get { if (entity_Dialogue == null) entity_Dialogue = DialogueManager.Instance.GetDialogue(Entity_Data.speakerName+Entity_Data.speakIndex); return entity_Dialogue; } set { entity_Dialogue = value; } }
    #endregion

    #region Unity Life Cycle : Call By Entities Controller
    /// <summary>
    /// Awake
    /// </summary>
    public abstract void Init(Transform _playerTransfrom, Transform _playerHeightTransform);
    
    /// <summary>
    /// Start
    /// </summary>
    public abstract void Setup();

    /// <summary>
    /// Update
    /// </summary>
    public abstract void Execute();
    #endregion

    #region Set State Anim, Spawn
    /// <summary>
    /// Set State
    /// </summary>
    /// <param name="_messageType"></param>
    public abstract void ReceiveMessage(EntityStateType _messageType);
    
    /// <summary>
    /// Decide Spawn State
    /// </summary>
    /// <param name="_isSpawn"></param>
    public virtual void SetActiveState(bool _isSpawn)
    {
        if (entity_Data == null)
        {
            Debug.LogError("이형체 데이터가 존재하지 않습니다.");
            return;
        }
        entity_Data.isSpawn = _isSpawn;
        gameObject.SetActive(_isSpawn);
    }

    /// <summary>
    /// Call When you talk with Player : Animation
    /// </summary>
    /// <param name="_triggerName"></param>
    public virtual void AnimationTriggerCallByDialogue(string _triggerName) {  }
    #endregion
}
