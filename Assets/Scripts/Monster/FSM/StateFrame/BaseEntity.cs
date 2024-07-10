using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    protected EntityData data;
    public EntityData Data { get {  return data;  }  set  {  data = value;  } }

    /// <summary>
    /// Awake
    /// </summary>
    public abstract void Init();
    
    /// <summary>
    /// Start
    /// </summary>
    public abstract void Setup();

    /// <summary>
    /// Update
    /// </summary>
    public abstract void Execute();

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
            Debug.LogError("이형체 데이터가 존재하지 않습니다.");
            return;
        }
        data.isSpawn = _isSpawn;
        gameObject.SetActive(_isSpawn);
    }
}
