using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MainCamEffect : MonoBehaviour
{
    [Header("FallDown Value"), SerializeField] float fallDownDescentTime = 0.1f;
    [SerializeField] float fallDownRotateTime = 0.1f;
    [SerializeField] float yPos = 1.1f;
    [SerializeField] float xAngle = 60f;

    [Header("FieldOfView"), SerializeField] CinemachineVirtualCamera followCamera;
    [SerializeField] float endFieldOfView;
    [SerializeField] float fieldOfViewTime;

    Camera mainCam = null;
    private void Awake()
    {
        mainCam = Camera.main;
    }

    #region Fall Down Effect
    public void FallDownVision(UnityAction fallAction)
    {
        StartCoroutine(DescentFallDown());
        StartCoroutine(RotateFallDown(fallAction));
    }

    IEnumerator DescentFallDown()
    {
        float timer = 0f;
        Vector3 stPos = mainCam.transform.position;
        Vector3 edPos = new Vector3(mainCam.transform.position.x, yPos, mainCam.transform.position.z);

        while (timer < fallDownDescentTime)
        {
            timer += Time.deltaTime;
            mainCam.transform.position = Vector3.Lerp(stPos, edPos, timer / fallDownDescentTime);
            yield return null;
        }

        mainCam.transform.position = edPos;
    }

    IEnumerator RotateFallDown(UnityAction fallAction)
    {
        float timer = 0f;
        Quaternion stCamRot = mainCam.transform.rotation;
        Quaternion edCamRot = Quaternion.Euler(60f, mainCam.transform.eulerAngles.y, mainCam.transform.eulerAngles.z);
        while (timer < fallDownRotateTime)
        {
            timer += Time.deltaTime;
            mainCam.transform.rotation = Quaternion.Slerp(stCamRot, edCamRot, timer / fallDownRotateTime);
            yield return null;
        }

        mainCam.transform.rotation = edCamRot;

        if (fallAction != null)
            fallAction.Invoke();
    }
    #endregion

    #region Find Of View Effect
    public void CallGraduallySetFieldOfView(float end=-2, float time=-2)
    {
        if (time <= -1)
            time = fieldOfViewTime;

        if (end <= -1)
            end = endFieldOfView;
        StartCoroutine(GraduallySetFieldOfView(end, time));
    }

    IEnumerator GraduallySetFieldOfView(float end, float time)
    {
        float timer = 0f;
        if (followCamera.Follow != null)
            followCamera.Follow = null;

        float start = followCamera.m_Lens.FieldOfView;
        while (timer < time) 
        {
            timer += Time.deltaTime;
            followCamera.m_Lens.FieldOfView = Mathf.Lerp(start, end, timer / time); 
            yield return null;
        }
        followCamera.m_Lens.FieldOfView = end;
    }
    #endregion
}
