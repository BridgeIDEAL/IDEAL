using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField, Tooltip("Use for find Entity Data, Dialogue Data Key")]protected string entityName;
    protected Transform playerTransform;

    // Entity Data
    protected Entity entity_Data =null;
    public Entity Entity_Data { get { if (entity_Data == null) EntityDataManager.Instance.GetEntityData(entityName);  return entity_Data;  }  set  {  entity_Data = value;  } }
    
    // Entity Controller
    protected EntitiesController controller;
    public EntitiesController Controller { get { return controller; } set { controller = value; } }

    // Entity Dialogue
    protected Dialogue entity_Dialogue =null;
    public Dialogue Entity_Dialogue { get { if (entity_Dialogue == null) entity_Dialogue = DialogueManager.Instance.GetDialogue(Entity_Data.speakerName+Entity_Data.speakIndex); return entity_Dialogue; } set { entity_Dialogue = value; } }

    #region Unity Life Cycle : Call By Entities Controller
    /// <summary>
    /// Awake
    /// </summary>
    public virtual void Init(Transform _playerTransfrom) { playerTransform = _playerTransfrom; }
    
    /// <summary>
    /// Start
    /// </summary>
    public abstract void Setup();

    /// <summary>
    /// Update
    /// </summary>
    public abstract void Execute();
    #endregion

    /// <summary>
    /// Set State
    /// </summary>
    /// <param name="_messageType"></param>
    public abstract void ReceiveMessage(EntityStateType _messageType);
    
    /// <summary>
    /// True => Call Entity Script , False => Call Controller Script
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
    /// Trigger로 인해 호출되는 이벤트
    /// </summary>
    /// <param name="_isActive"></param>
    public virtual void EntityTriggerEvent(bool _isActive=true) { }
}
