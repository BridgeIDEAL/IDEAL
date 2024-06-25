using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomCleanTool : MonoBehaviour
{
    Classroom classroom;
    [SerializeField] GameObject interactionObject;
    public void Active(Classroom _classroom)
    {
        classroom = _classroom;
        interactionObject.SetActive(true);
    }

    public void HoldCleanTool()
    {
        // ������ ȿ���� ���鼭 ȣ��
        classroom.InitDirtyState();
        Destroy(interactionObject);
    }
}
