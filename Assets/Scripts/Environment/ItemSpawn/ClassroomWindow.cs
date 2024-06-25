using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomWindow : MonoBehaviour
{
    Classroom classroom;
    public bool IsOpenWindow { get; set; } = false;
    public void InitWindowState(Classroom _classroom)
    {
        int randomNum = Random.Range(0, 20);
        classroom = _classroom;
        if (randomNum < 10) // 50%
        {
            // open state : �ȿ�� ��
            OpenWindow();
        }
        else
        {
            // close state : �������
            IsOpenWindow = false;
        }
    }

    public void OpenWindow()
    {
        IsOpenWindow = true;
        classroom.OpenWindow();
        RotateWindowHindge();
    }

    /// <summary>
    /// ���Ƿ� â���� ������ ��� ��Ȱ��ȭ ��Ŵ
    /// </summary>
    public void RotateWindowHindge()
    {
        gameObject.SetActive(false);
    }
}
