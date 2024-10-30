using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJumpScare : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            TurnSt();
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 100), "교장구현"))
        {
            pri_death();
        }
        if (GUI.Button(new Rect(200, 0, 100, 100), "학생주임구현"))
        {
            head_death();
        }
        if (GUI.Button(new Rect(400, 0, 100, 100), "목꺾인 여학생"))
        {
            reverse_death();
        }
    }

    [Header("교장"), SerializeField] JumpScarePrincipal js;
    [SerializeField] Transform player;
    public void pri_death()
    {
        //js.PlayerTransform = player;
        js.ActiveJumpScare();
    }

    [Header("학생주임"), SerializeField] JumpScareHeadOfStudentTeacher jsHead;
    public void head_death()
    {
        jsHead.ActiveJumpScare();
    }

    [Header("반대 여학생"), SerializeField] JumpScareReverseGirl reverse;
    public void reverse_death()
    {
        reverse.ActiveJumpScare();
    }

    [SerializeField] Transform student;
    [SerializeField] float time;
    public void TurnSt()
    {
        StartCoroutine(TurnSTB());
    }

    IEnumerator TurnSTB()
    {
        float timer = 0f;
        Quaternion st = student.rotation;
        Quaternion ed = Quaternion.LookRotation(transform.position-student.position);
        
        while (timer < time)
        {
            timer += Time.deltaTime;
            student.rotation = Quaternion.Slerp(st,ed,timer/time);   
            yield return null;
        }

        student.rotation = ed;
    }
}
