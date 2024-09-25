using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurn : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    bool once = true;

    // 135도 회전 시, 애니메이션 속도를 조절해 더 짧게 재생
    float targetRotation = 135f;
    float currentRotation = 0f;
    float rotationSpeed = 90f;  // 초당 회전 속도
    float minAnimationSpeed = 0.1f;  // 애니메이션의 최소 속도

    private void Awake()
    {
        //StartCoroutine(Rot());
    }

    private void Update()
    {
        if (once)
        {
            //RotateCharacter();
        }
    }

    void RotateCharacter()
    {
        // 남은 회전 각도 계산
        float remainingAngle = targetRotation - currentRotation;

        // 회전할 각도 계산 (rotationSpeed * Time.deltaTime과 남은 각도 중 작은 값)
        float angle = Mathf.Min(rotationSpeed * Time.deltaTime, remainingAngle);

        if (Mathf.Abs(remainingAngle) < 0.01f)  // 거의 회전 완료 시
        {
            once = false;
            anim.speed = 1;  // 애니메이션 속도 정상화
            return;
        }

        currentRotation += angle;

        // 애니메이션 속도 조정 (최소 속도 설정)
        float animSpeed = Mathf.Max(angle / rotationSpeed, minAnimationSpeed);  // 최소 속도 0.1 이상 유지
        anim.speed = animSpeed;

        Debug.Log($"Angle: {angle}, Animation Speed: {anim.speed}");

        // 실제 캐릭터 회전
        transform.Rotate(0, angle, 0);
    }

    IEnumerator Rot()
    {
        float timer = 0f;
        Quaternion startRot = Quaternion.identity;
        Quaternion tartgetRot;
        tartgetRot = Quaternion.Euler(0, 45, 0);
        while (timer < 0.93f)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRot, tartgetRot, timer / 1f);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 135, 0);
        anim.SetTrigger("ReTurn");
    }


    private void OnGUI()
    {
        if(GUI.Button(new Rect(0f, 0f, 100f, 100f), "Rotate"))
        {
            
            anim.SetTrigger("Turn");
            StartCoroutine(Rot());
        }

        if (GUI.Button(new Rect(100f, 0f, 100f, 100f), "Reset"))
        {
            Quaternion tartgetRot;
            tartgetRot = Quaternion.Euler(0, 0, 0);
            transform.rotation = tartgetRot;
        }

        if (GUI.Button(new Rect(0f, 100f, 100f, 100f), "No Anim Rotate"))
        {
            StartCoroutine(Rot());
        }

        if (GUI.Button(new Rect(100f, 100f, 100f, 100f), "Anim Rotate"))
        {
            anim.SetTrigger("Turn");
        }


        if (GUI.Button(new Rect(0f, 200f, 100f, 100f), "Idle Blend"))
        {
            Quaternion tartgetRot;
            tartgetRot = Quaternion.Euler(0, 90, 0);
            transform.rotation = tartgetRot;
            anim.SetBool("Idle",true);
        }

        if (GUI.Button(new Rect(100f, 200f, 100f, 100f), "Rotate Blend"))
        {
            anim.SetBool("Idle", false);
           
        }
    }
}
