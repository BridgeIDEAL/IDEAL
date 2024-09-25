using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurn : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    bool once = true;

    // 135�� ȸ�� ��, �ִϸ��̼� �ӵ��� ������ �� ª�� ���
    float targetRotation = 135f;
    float currentRotation = 0f;
    float rotationSpeed = 90f;  // �ʴ� ȸ�� �ӵ�
    float minAnimationSpeed = 0.1f;  // �ִϸ��̼��� �ּ� �ӵ�

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
        // ���� ȸ�� ���� ���
        float remainingAngle = targetRotation - currentRotation;

        // ȸ���� ���� ��� (rotationSpeed * Time.deltaTime�� ���� ���� �� ���� ��)
        float angle = Mathf.Min(rotationSpeed * Time.deltaTime, remainingAngle);

        if (Mathf.Abs(remainingAngle) < 0.01f)  // ���� ȸ�� �Ϸ� ��
        {
            once = false;
            anim.speed = 1;  // �ִϸ��̼� �ӵ� ����ȭ
            return;
        }

        currentRotation += angle;

        // �ִϸ��̼� �ӵ� ���� (�ּ� �ӵ� ����)
        float animSpeed = Mathf.Max(angle / rotationSpeed, minAnimationSpeed);  // �ּ� �ӵ� 0.1 �̻� ����
        anim.speed = animSpeed;

        Debug.Log($"Angle: {angle}, Animation Speed: {anim.speed}");

        // ���� ĳ���� ȸ��
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
