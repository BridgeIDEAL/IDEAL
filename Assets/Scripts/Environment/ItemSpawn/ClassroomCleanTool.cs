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
        // 열리는 효과를 쓰면서 호출
        classroom.InitDirtyState();
        Destroy(interactionObject);
    }
}
