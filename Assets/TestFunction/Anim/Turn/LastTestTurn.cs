using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTestTurn : MonoBehaviour
{
    public Animator animator;
    public float targetRotation;  // 최종 목표 회전 각도 (예: 135도)
    private float initialRotation;  // 애니메이션이 시작할 때의 캐릭터 회전 각도
    private float extraRotation;    // 애니메이션 도중에 추가로 회전해야 할 각도
    private bool isRotating = false; // 회전 중인지 여부

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
        // 현재 캐릭터 회전 각도를 저장
        initialRotation = transform.eulerAngles.y;

        // 목표 회전 각도 설정 (예: 135도)
        targetRotation = finalRotationAngle;

        // 애니메이션이 90도 회전하므로, 나머지 각도는 135 - 90 = 45도
        extraRotation = targetRotation - initialRotation - 90f;
        if (FindClipTime(clipName) == null)
        {
            Debug.Log("없다");
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
