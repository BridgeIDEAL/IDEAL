using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    public float sightDistance;
    public float sightAngle;
    protected bool findPlayer = false;
    protected LayerMask playerMask = 1<<3;
    protected GameObject playerObject;
    public virtual void Setup() { playerObject = GameObject.FindGameObjectWithTag("Player"); }
    public abstract void UpdateBehavior();
    public bool DetectPlayer()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude <= sightDistance)
        {
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            float theta = Mathf.Acos(dot);
            float degree = Mathf.Rad2Deg * theta;
            if (degree <= sightAngle / 2f)
                findPlayer = true;
            else
                findPlayer = false;
        }
        else
            findPlayer = false;
        if (findPlayer)
            return true;
        else
            return false;
    }

    protected void OnDrawGizmos()
    {
        Handles.color = (findPlayer)?Color.red:Color.blue;
        // 시작점, 노말벡터(법선벡터), 방향, 각도, 반지름
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, sightAngle / 2, sightDistance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -sightAngle / 2, sightDistance);
    }
}
