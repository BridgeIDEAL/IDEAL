using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTestTurn : MonoBehaviour
{
    public Animator animator;
    public float targetRotation;  // ���� ��ǥ ȸ�� ���� (��: 135��)
    private float initialRotation;  // �ִϸ��̼��� ������ ���� ĳ���� ȸ�� ����
    private float extraRotation;    // �ִϸ��̼� ���߿� �߰��� ȸ���ؾ� �� ����
    private bool isRotating = false; // ȸ�� ������ ����

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0f, 0f, 100f, 100f), "Rotate"))
        {
            RotateCharacter(135f, "Rotate");
        }

        if (GUI.Button(new Rect(100f, 0f, 100f, 100f), "Reset"))
        {
            Quaternion tartgetRot;
            tartgetRot = Quaternion.Euler(0, 0, 0);
            transform.rotation = tartgetRot;
        }
    }

    void Update()
    {
        //if (isRotating)
        //{
            
        //    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rotate"))
        //    {
        //        Debug.Log("1421421");
        //        float animProgress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //        if (animProgress >= 1f)
        //        {
        //            transform.rotation = Quaternion.Euler(0, targetRotation, 0);
        //            isRotating = false;
        //            return;
        //        }
        //        float currentRotation = Mathf.Lerp(initialRotation, initialRotation + extraRotation, animProgress);
        //        transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        //    }
        //    else if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Rotate"))
        //    {
        //        transform.rotation = Quaternion.Euler(0, targetRotation, 0);
        //        isRotating = false;
        //    }
        //}
    }

    public void RotateCharacter(float finalRotationAngle, string clipName)
    {
        // ���� ĳ���� ȸ�� ������ ����
        initialRotation = transform.eulerAngles.y;

        // ��ǥ ȸ�� ���� ���� (��: 135��)
        targetRotation = finalRotationAngle;

        // �ִϸ��̼��� 90�� ȸ���ϹǷ�, ������ ������ 135 - 90 = 45��
        extraRotation = targetRotation - initialRotation - 90f;
        if (FindClipTime(clipName) == null)
        {
            Debug.Log("����");
            return;
        }
        StartCoroutine(RotateCor(FindClipTime(clipName).length, extraRotation+ initialRotation));
    }

    IEnumerator RotateCor(float clipLength, float extraAngle)
    {
        animator.SetTrigger("Turn");
        float timer = 0f;
        float animTimer = clipLength;
        Quaternion startRotate = this.transform.rotation;
        Quaternion endRotate = Quaternion.Euler(0, extraAngle, 0);
        while (timer <= animTimer)
        {
            timer += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(startRotate, endRotate, timer / animTimer);
            yield return null;
        }
        animator.CrossFade("Idle", 0.1f);
        //this.transform.rotation = Quaternion.Euler(0, targetRotation, 0);
    }


    public AnimationClip FindClipTime(string clipName)
    {
        RuntimeAnimatorController rac = animator.runtimeAnimatorController;
        int cnt = rac.animationClips.Length;
        for(int i=0; i<cnt; i++)
        {
            if (rac.animationClips[i].name == clipName)
            {
                return rac.animationClips[i];
            }
        }
        return null;
    }
}
