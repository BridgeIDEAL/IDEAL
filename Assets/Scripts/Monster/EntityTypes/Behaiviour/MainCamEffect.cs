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
    //[SerializeField] float xAngle = 60f;

    [Header("FieldOfView"), SerializeField] CinemachineVirtualCamera followCamera;
    float endFieldOfView = 0f;
    float fieldOfViewTime = 0f;

    Camera mainCam = null;

    private void Start()
    {
        mainCam = Camera.main;
        if (followCamera == null)
        {
            CinemachineBrain brain = mainCam.GetComponent<CinemachineBrain>();

            if (brain.ActiveVirtualCamera as CinemachineVirtualCamera != null)
            {
                followCamera = brain.ActiveVirtualCamera as CinemachineVirtualCamera;
            }
        }
    }

    #region Fall Down Effect
    public void FallDownVision(UnityAction fallAction, float waitTime=0f)
    {
        StartCoroutine(DescentFallDown());
        StartCoroutine(RotateFallDown(fallAction, waitTime));
    }

    IEnumerator DescentFallDown()
    {
        float timer = 0f;
        Vector3 stPos = followCamera.transform.position;
        Vector3 edPos = new Vector3(followCamera.transform.position.x, yPos, followCamera.transform.position.z);

        while (timer < fallDownDescentTime)
        {
            timer += Time.deltaTime;
            followCamera.transform.position = Vector3.Lerp(stPos, edPos, timer / fallDownDescentTime);
            yield return null;
        }

        followCamera.transform.position = edPos;
    }

    IEnumerator RotateFallDown(UnityAction fallAction, float waitTime = 0f)
    {
        float timer = 0f;
        Quaternion stCamRot = followCamera.transform.rotation;
        Quaternion edCamRot = Quaternion.Euler(90f, followCamera.transform.eulerAngles.y, followCamera.transform.eulerAngles.z);
        while (timer < fallDownRotateTime)
        {
            timer += Time.deltaTime;
            followCamera.transform.rotation = Quaternion.Slerp(stCamRot, edCamRot, timer / fallDownRotateTime);
            yield return null;
        }

        followCamera.transform.rotation = edCamRot;

        yield return new WaitForSeconds(waitTime);

        if (fallAction != null)
            fallAction.Invoke();
    }
    #endregion

    #region Find Of View Effect
    public void CallSetFieldOfView(float setView) => followCamera.m_Lens.FieldOfView = setView;
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
