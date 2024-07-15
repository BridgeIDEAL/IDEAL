using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    [SerializeField] protected Transform playerTransform;
    protected EntityData data;
    protected EntitiesController controller;
    public EntitiesController Controller { get { return controller; } set { controller = value; } }
    public EntityData Data { get {  return data;  }  set  {  data = value;  } }

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
        if (data == null)
        {
            Debug.LogError("����ü �����Ͱ� �������� �ʽ��ϴ�.");
            return;
        }
        data.isSpawn = _isSpawn;
        gameObject.SetActive(_isSpawn);
    }

    /// <summary>
    /// Trigger�� ���� ȣ��Ǵ� �̺�Ʈ
    /// </summary>
    /// <param name="_isActive"></param>
    public virtual void EntityTriggerEvent(bool _isActive=true) { }
}
