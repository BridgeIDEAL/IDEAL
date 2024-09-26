using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Animator anim;

    [SerializeField]
    float rotateAngleSpeed = 90f;

    [SerializeField]
    bool canTurn = true;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canTurn)
        {
            Turn();
        }
    }

    public void Turn()
    {
        float angle = CalcurateAngle();

        Debug.Log(angle);

    }

    public float CalcurateAngle()
    {
        Vector3 directionVec = playerTransform.position - this.transform.position;
        Vector3 flatDirectionV = new Vector3(directionVec.x, 0, directionVec.z).normalized;
        float angle = Vector3.SignedAngle(this.transform.forward, flatDirectionV, Vector3.up);
        return angle;
    }


    public IEnumerator CoolDownTurn()
    {
        canTurn = false;
        anim.SetTrigger("Turn");
        while (Mathf.Abs(CalcurateAngle()) > 0.5f)
        {
            Turning(CalcurateAngle());
            yield return null;
        }
        canTurn = true;
    }

    public void Turning(float targetAngle)
    {
        float currentAngle = transform.eulerAngles.y;

        // ������ -180�� ~ 180�� ������ ����ȭ
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
        //bool turnRight = angleDifference > 0;

        // �ִϸ��̼� �ӵ� ���� (�ʿ��� ��� ������ ���� �ӵ� ���� ����)
        float animationSpeed = Mathf.Abs(angleDifference) / 90f;  // 90�� �������� ���� ����
        anim.speed = animationSpeed;

        // ���� ȸ�� ó��
        float rotationStep = rotateAngleSpeed * Time.deltaTime;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationStep);
        transform.rotation = Quaternion.Euler(0, newAngle, 0);
    }
}
