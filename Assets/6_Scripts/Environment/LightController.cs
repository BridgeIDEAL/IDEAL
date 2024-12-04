using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject testObjectLight;
    public Light testLight;

    bool lightOn = false;
    bool lightSwing = false;

    public float rotationSpeed = 5f;

    // 시작 시 회전 각도
    private float initialRotation;

    // 회전 방향 (1 또는 -1)
    private int rotationDirection = 1;

    // 최대 회전 각도
    private float maxRotation = 20f;

    void Start()
    {
        // 초기 회전 각도 설정
        initialRotation = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        if (lightSwing)
        {
            // 현재 회전 각도
            float currentRotation = transform.rotation.eulerAngles.x;

            // 회전 방향 전환 확인
            if (currentRotation >= maxRotation || currentRotation <= -maxRotation)
            {
                rotationDirection *= -1; // 방향 전환
            }

            // 회전 각도 업데이트
            float newRotation = currentRotation + rotationDirection * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(newRotation, 0f, 0f);
        }
       

        if (Input.GetKeyDown(KeyCode.Q)) // 키고 끄기
        {
            if (lightOn)
            {
                testLight.intensity = 0f ;
            }
            else
            {
                testLight.intensity = 5f;
            }
            lightOn = !lightOn;
        }
        if (Input.GetKeyDown(KeyCode.W)) // 움직이기
        {
            lightSwing = !lightSwing;
        }
    }
}
