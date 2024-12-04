using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisibleDetectPlayer : MonoBehaviour
{
    protected bool isDetectPlayer = false;
    public bool IsDetectPlayer { get { return isDetectPlayer; } set { isDetectPlayer = value; } }
    public abstract bool DetectExecute();
}
