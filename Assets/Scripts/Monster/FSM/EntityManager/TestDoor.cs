using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDoor : MonoBehaviour
{
    bool isOpne = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isOpne)
            {
                transform.position -= Vector3.right * 5;

            }
            else
            {
                transform.position += Vector3.right * 5;
            }
            isOpne = !isOpne;
        }
    }

    public void OpenDoor()
    {
        if (!isOpne)
        {
            transform.position -= Vector3.right * 5;

        }
        isOpne = !isOpne;
    }
}
