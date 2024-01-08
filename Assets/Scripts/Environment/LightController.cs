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

    // ���� �� ȸ�� ����
    private float initialRotation;

    // ȸ�� ���� (1 �Ǵ� -1)
    private int rotationDirection = 1;

    // �ִ� ȸ�� ����
    private float maxRotation = 20f;

    void Start()
    {
        // �ʱ� ȸ�� ���� ����
        initialRotation = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        if (lightSwing)
        {
            // ���� ȸ�� ����
            float currentRotation = transform.rotation.eulerAngles.x;

            // ȸ�� ���� ��ȯ Ȯ��
            if (currentRotation >= maxRotation || currentRotation <= -maxRotation)
            {
                rotationDirection *= -1; // ���� ��ȯ
            }

            // ȸ�� ���� ������Ʈ
            float newRotation = currentRotation + rotationDirection * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(newRotation, 0f, 0f);
        }
       

        if (Input.GetKeyDown(KeyCode.Q)) // Ű�� ����
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
        if (Input.GetKeyDown(KeyCode.W)) // �����̱�
        {
            lightSwing = !lightSwing;
        }
    }
}
