using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region 
    private static int giveID=0; // 부여하는 고유 ID 
    private int takeID; // 부여된 고유 ID
    public int ID{ set { takeID = value; giveID++; }get { return takeID; }}
    public Transform InitTransform { get; set; } // 초기 위치
    public bool CanInteraction { get; set; } = true;
    public float sightDistance;
    public float sightAngle;
    protected bool findPlayer = false;
    protected LayerMask playerMask = 1<<3;
    public GameObject playerObject;
    public float chaseSpeed;
    #endregion

    public virtual void Setup() { playerObject = GameObject.FindGameObjectWithTag("Player"); ID = giveID; }
    public abstract void UpdateBehavior();
    public virtual void RestInteraction() { } // 휴식 공간에 들어갔을 때
    public virtual void StartInteraction() { } // 상호작용을 시작했을 때
    public virtual void SuccessInteraction() { } // 상호작용을 성공했을 때
    public virtual void FailInteraction() { } // 상호작용을 실패했을 때 (정신력 감소)
    public virtual void ChaseInteraction() { } // 상호작용을 실패했을 때 (도망쳐야 할 때)
    public bool DetectPlayer()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude <= sightDistance)
        {
            float dot = Vector3.Dot(interV.normalized, transform.forward);
            float theta = Mathf.Acos(dot);
            float degree = Mathf.Rad2Deg * theta;
            if (degree <= sightAngle / 2f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, interV, out hit, sightDistance))
                {
                    if (hit.collider.CompareTag("Player"))
                        findPlayer = true;
                    else
                        findPlayer = false;
                }
            }
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

    public bool CheckDistance()
    {
        Vector3 interV = playerObject.transform.position - transform.position;
        if (interV.magnitude <= sightDistance)
            return true;
        else
            return false;
    }

    protected virtual void OnDrawGizmos()
    {
        Handles.color = (findPlayer)?Color.red:Color.blue;
        // 시작점, 노말벡터(법선벡터), 방향, 각도, 반지름
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, sightAngle / 2, sightDistance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -sightAngle / 2, sightDistance);
        
    }
}
