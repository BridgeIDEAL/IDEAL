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

        // 각도를 -180도 ~ 180도 범위로 정규화
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
        //bool turnRight = angleDifference > 0;

        // 애니메이션 속도 조절 (필요한 경우 각도에 따라 속도 비율 조정)
        float animationSpeed = Mathf.Abs(angleDifference) / 90f;  // 90도 기준으로 비율 조정
        anim.speed = animationSpeed;

        // 실제 회전 처리
        float rotationStep = rotateAngleSpeed * Time.deltaTime;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationStep);
        transform.rotation = Quaternion.Euler(0, newAngle, 0);
    }
}
