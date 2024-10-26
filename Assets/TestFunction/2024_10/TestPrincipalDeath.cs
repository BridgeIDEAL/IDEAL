using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrincipalDeath : MonoBehaviour
{
    [SerializeField] Transform groundTF;

    [SerializeField] float seeGroundTime;
    [SerializeField] Transform look;
    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 100, 100), "테스트용"))
        {
            TestGround();
        }

        if (GUI.Button(new Rect(200, 0, 100, 100), "테스트용"))
        {
            Test();
        }

        if (GUI.Button(new Rect(400, 0, 100, 100), "테스트용"))
        {
            Testvec();
        }
    }

    public void Test()
    {
        StartCoroutine(seeGround1());
    }
    public void TestGround()
    {
        StartCoroutine(seeGround());
    }

    IEnumerator seeGround()
    {
        Transform camTf = Camera.main.transform;
        Vector3 stPos = camTf.position;
        Vector3 edPos = groundTF.position;

        Quaternion stRot = camTf.rotation;
        Quaternion edRot = groundTF.rotation;

        float timer = 0f;
        while (timer < seeGroundTime)
        {
            timer += Time.deltaTime;
            Camera.main.transform.position = Vector3.Slerp(stPos, edPos, timer / seeGroundTime);
            Camera.main.transform.rotation= Quaternion.Slerp(stRot, edRot, timer / seeGroundTime);
            
            yield return null;
        }
        Camera.main.transform.position = edPos;
        Camera.main.transform.rotation = edRot;

        stPos = camTf.position;
        edPos = look.position;
        stRot = camTf.rotation;
        edRot = look.rotation;

        timer = 0f;
        while (timer < seeGroundTime)
        {
            timer += Time.deltaTime;
            Camera.main.transform.position = Vector3.Slerp(stPos, edPos, timer / seeGroundTime);
            Camera.main.transform.rotation = Quaternion.Slerp(stRot, edRot, timer / seeGroundTime);
            Camera.main.fieldOfView = Mathf.Lerp(60, 20, timer / seeGroundTime);
            yield return null;
        }
        Camera.main.transform.position = edPos;
        Camera.main.transform.rotation = edRot;
    }

    IEnumerator seeGround1()
    {
        Transform camTf = Camera.main.transform;

        float timer = 0f;
        while (timer < seeGroundTime)
        {
            timer += Time.deltaTime;
            Camera.main.fieldOfView = Mathf.Lerp(60, 20, timer / seeGroundTime);
            yield return null;
        }
    }

    [SerializeField] Transform dirTf;
    public void Testvec()
    {
        float delta = Vector3.Distance(dirTf.position,transform.position);

        Vector3 dir = dirTf.position - transform.position;
        dir.y = 0;
        dir = dir.normalized;
        Debug.Log(dir);
        Debug.Log(delta);
        Debug.Log(transform.position + dir * delta);
    }
}
