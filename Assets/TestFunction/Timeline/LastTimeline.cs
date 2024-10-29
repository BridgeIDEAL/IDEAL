using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class LastTimeline : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    [SerializeField, Header("Last PrincipalMove")] Transform lastPrincipal;
    Vector3 destPos = new Vector3(5.3f,0f,5f);
    Vector3 destAngle = new Vector3(0, 200, 0);
    public void CallMoveNRotate() { StartCoroutine(Move()); }

    IEnumerator Move()
    {
        float timer = 0f;
        Vector3 stPos = lastPrincipal.position;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            lastPrincipal.transform.position = Vector3.Lerp(stPos, destPos, timer / 1f);
            yield return null;
        }
        lastPrincipal.transform.position = destPos;
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        float timer = 0f;
        Quaternion stRot = lastPrincipal.transform.rotation;
        Quaternion edRot = Quaternion.Euler(destAngle);
        while (timer < 0.2f)
        {
            timer += Time.deltaTime;
            lastPrincipal.transform.rotation = Quaternion.Slerp(stRot, edRot, timer / 1f);
            yield return null;
        }
        lastPrincipal.transform.rotation = edRot;
    }

    public void CallDecreaseFieldOfView() { StartCoroutine(DecreaseFieldOfView()); }
    [SerializeField, Header("FieldView Time")] float decreaseFieldViewTime; 
    IEnumerator DecreaseFieldOfView()
    {
        float timer = 0f;
        float startView = 60;
        float endView = 25;
        while (timer < decreaseFieldViewTime)
        {
            timer+=Time.deltaTime;
            mainCam.fieldOfView = Mathf.Lerp(startView, endView, timer / decreaseFieldViewTime);
            yield return null;
        }
        mainCam.fieldOfView = endView;
    }
}

