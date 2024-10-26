using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrincipalDeath : MonoBehaviour
{
    Transform camTf;
    [SerializeField, Range(0.1f,3f)] float deathTimer;
    [SerializeField] Transform setTf;
    [SerializeField] Animator anim;

    bool isDeath = false;
    private void Awake()
    {
        if (camTf == null)
            camTf = Camera.main.transform;
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 500, 200), "카메라맨 죽음"))
        {
            Death();
        }
    }

    public void Death()
    {
        if (!isDeath)
        {
            isDeath = true;
            StartCoroutine(DeathCor());
        }
    }

    IEnumerator DeathCor()
    {
        float timer = 0f;
        Quaternion camRot = camTf.rotation;
        Quaternion setRot = setTf.rotation;
        Vector3 camPos = camTf.position;
        Vector3 setPos = setTf.position;

        while (timer < deathTimer)
        {
            timer += Time.deltaTime;
            camTf.rotation = Quaternion.Slerp(camRot, setRot, timer / deathTimer);
            camTf.position = Vector3.Slerp(camPos, setPos, timer / deathTimer);
            yield return null;
        }
        camTf.rotation = setRot;
        camTf.position = setPos;
        anim.SetTrigger("Death");
    }
}
