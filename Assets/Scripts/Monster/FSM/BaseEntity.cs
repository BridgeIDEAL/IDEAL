using UnityEngine;

public abstract class BaseEntity : MonoBehaviour
{
    protected LayerMask playerMask = 1<<3;
    protected GameObject playerObject;
    public virtual void Setup() { playerObject = GameObject.FindGameObjectWithTag("Player"); }
    public abstract void UpdateBehavior(); 
}
