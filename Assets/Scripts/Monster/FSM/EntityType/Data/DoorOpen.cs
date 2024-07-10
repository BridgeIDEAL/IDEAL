using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] float doorTime;
    [SerializeField] bool isLocalPosition;
    [SerializeField, Tooltip("Init Close State : 0 = close pos")] Vector3[] doorPosition;
    bool isOpen = false;
    bool canControl = true;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && canControl)
        {
            StartCoroutine(DoorCor());
        }
    }

    IEnumerator DoorCor()
    {
        float timer = 0f;
        canControl = false;
        if (isLocalPosition)
        {
            if (isOpen)
            {
                while (timer <= doorTime)
                {
                    timer += Time.deltaTime;
                    transform.localPosition = Vector3.Lerp(doorPosition[1],doorPosition[0],timer/doorTime);
                    yield return null;
                }
                isOpen = false;
            }
            else
            {
                while (timer <= doorTime)
                {
                    timer += Time.deltaTime;
                    transform.localPosition = Vector3.Lerp(doorPosition[0], doorPosition[1], timer / doorTime);
                    yield return null;
                }
                isOpen = true;
            }
        }
        else
        {
            if (isOpen)
            {
                while (timer <= doorTime)
                {
                    timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(doorPosition[1], doorPosition[0], timer / doorTime);
                    yield return null;
                }
                isOpen = false;
            }
            else
            {
                while (timer <= doorTime)
                {
                    timer += Time.deltaTime;
                    transform.position = Vector3.Lerp(doorPosition[0], doorPosition[1], timer / doorTime);
                    yield return null;
                }
                isOpen = true;
            }
        }
        canControl = true;
    }
}
