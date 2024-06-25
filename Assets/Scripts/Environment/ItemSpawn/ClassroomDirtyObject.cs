using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomDirtyObject : MonoBehaviour
{
    public bool IsCleanTrash { get; set; } = false;
    Classroom classroom;

    public void Init(Classroom _classroom)
    {
        classroom = _classroom;
        IsCleanTrash = false;
    }

    public void CleanTrash()
    {
        IsCleanTrash = true;
        classroom.CleanClassroom();
    }
}
