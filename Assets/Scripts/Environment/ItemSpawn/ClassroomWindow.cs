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
            // open state : 안열어도 됨
            OpenWindow();
        }
        else
        {
            // close state : 열어야함
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
    /// 임의로 창문을 돌리는 대신 비활성화 시킴
    /// </summary>
    public void RotateWindowHindge()
    {
        gameObject.SetActive(false);
    }
}
