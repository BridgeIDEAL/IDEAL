using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HeadOfTeacherDeath : MonoBehaviour
{
    [SerializeField] Animator attackAnim;
    [SerializeField] Animator fallAnim;
    [SerializeField] Transform enemyHead;

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 100, 100), "µ•Ω∫æ¿"))
        {
            DeathScene();
        }

        if (GUI.Button(new Rect(100, 0, 100, 100), "√Îº“"))
        {
            IdleScene();
        }
    }

    public void DeathScene()
    {
        attackAnim.Play("Attack");
        fallAnim.Play("FallDown");
    }

    public void IdleScene()
    {
        attackAnim.Play("Idle");
        fallAnim.Play("Idle");
        Camera.main.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void Look()
    {
        Vector3 dir = enemyHead.position - Camera.main.transform.position;
        dir = dir.normalized;
        StartCoroutine(LookCor(dir));
    }

    IEnumerator LookCor(Vector3 dir)
    {
        float time = 0f;
        Quaternion quaternion = Quaternion.LookRotation(dir);
        Quaternion camQ = Camera.main.transform.rotation;
        while (time < 1f)
        {
            time += Time.deltaTime;
            Camera.main.transform.rotation = Quaternion.Slerp(camQ, quaternion, time/1f);
            yield return null;
        }
    }
}
