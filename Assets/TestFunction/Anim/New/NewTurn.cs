using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTurn : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 100, 100), "회전"))
        {
            Rotate();
        }
        if (GUI.Button(new Rect(100, 0, 100, 100), "평상시"))
        {
            anim.SetBool("Idle", true);
        }
    }

    public void Rotate()
    {
        StartCoroutine(RoateCor());
    }

    IEnumerator RoateCor()
    {
        Quaternion stQ = Quaternion.identity;
        Quaternion enQ =  Quaternion.Euler(0,135,0);
        anim.SetBool("Idle", false);
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(stQ, enQ, timer);
            yield return null;
        }
    }
}
