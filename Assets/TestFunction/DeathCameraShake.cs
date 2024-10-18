using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCameraShake : MonoBehaviour
{
    public Transform movePos;
    public Transform target;  // ī�޶� ���� ��ǥ, �� �÷��̾ ������
    public float pullduratio = 0.1f;
    public float duration = 0.5f;  // ��鸮�� ���� �ð�
    public float magnitude = 0.5f;  // ��鸲�� ����
    public float moveSpeed = 2f;  // ī�޶� �̵��ϴ� �ӵ�
    private Vector3 originalPos;  // ī�޶��� ���� ��ġ
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
        
      

        // ī�޶� Ÿ���� ���� �̵� ����
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion original = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

     
        while (elapsed < duration)
        {

            transform.rotation = Quaternion.Slerp(original, targetRotation, elapsed / duration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // ��鸲�� ���� �� ī�޶� ��ġ�� �������
        transform.rotation = targetRotation;
        isShaking = false;
    }
}
