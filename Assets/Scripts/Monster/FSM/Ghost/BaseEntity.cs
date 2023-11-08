using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region 
    private static int giveID=0; // �ο��ϴ� ���� ID 
    private int takeID; // �ο��� ���� ID
    public int ID{ set { takeID = value; giveID++; }get { return takeID; }}
    public Transform InitTransform { get; set; } // �ʱ� ��ġ
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
    public virtual void RestInteraction() { } // �޽� ������ ���� ��
    public virtual void StartInteraction() { } // ��ȣ�ۿ��� �������� ��
    public virtual void SuccessInteraction() { } // ��ȣ�ۿ��� �������� ��
    public virtual void FailInteraction() { } // ��ȣ�ۿ��� �������� �� (���ŷ� ����)
    public virtual void ChaseInteraction() { } // ��ȣ�ۿ��� �������� �� (�����ľ� �� ��)
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
        // ������, �븻����(��������), ����, ����, ������
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, sightAngle / 2, sightDistance);
        Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, -sightAngle / 2, sightDistance);
        
    }
}
