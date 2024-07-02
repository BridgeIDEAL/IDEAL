using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseEntity : MonoBehaviour
{
    public abstract void Init();
    public abstract void Execute();
    public abstract void ReceiveMessage(EntityStateType _messageType);
}
