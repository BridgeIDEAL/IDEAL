using UnityEngine;
using UnityEditor;

public abstract class BaseEntity : MonoBehaviour
{
    #region Common Stat
    public int ID { get; set; }
    public bool CanInteraction { get; set; } = true;
    public float sightDistance;
    public float sightAngle;
    protected bool findPlayer = false;
    protected LayerMask playerMask = 1<<3;
    public GameObject playerObject;
    protected float lookSpeed = 2f;
    #endregion

    public virtual void Setup(MonsterData.MonsterStat stat) {}
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
}
