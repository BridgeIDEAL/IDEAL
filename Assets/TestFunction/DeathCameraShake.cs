using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCameraShake : MonoBehaviour
{
    public Transform movePos;
    public Transform target;  // 카메라가 따라갈 목표, 즉 플레이어나 허기워기
    public float pullduratio = 0.1f;
    public float duration = 0.5f;  // 흔들리는 지속 시간
    public float magnitude = 0.5f;  // 흔들림의 강도
    public float moveSpeed = 2f;  // 카메라가 이동하는 속도
    private Vector3 originalPos;  // 카메라의 원래 위치
    private bool isShaking = false;

    [SerializeField] Animator anim;
    void Start()
    {
        originalPos = transform.localPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            StartCameraShake();
    }

    public void StartCameraShake()
    {
        if (!isShaking)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        Vector3 originalPos = transform.position;
        Vector3 movePosition = movePos.position;
        float elapsed = 0.0f;
        while (elapsed < pullduratio)
        {
            transform.position= Vector3.Slerp(originalPos, movePosition, elapsed / pullduratio);
            elapsed += Time.deltaTime;

            yield return null;
        }
        anim.SetTrigger("Jump");
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;
        
      

        // 카메라가 타겟을 향해 이동 시작
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion original = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

     
        while (elapsed < duration)
        {

            transform.rotation = Quaternion.Slerp(original, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // 흔들림이 끝난 후 카메라 위치를 원래대로
        transform.rotation = targetRotation;
        isShaking = false;
    }
}
