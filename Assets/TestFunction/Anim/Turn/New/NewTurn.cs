using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTurn : MonoBehaviour
{
    Animator anim = null;
    AnimationClip turnAnim = null;

    [SerializeField] float yAngle = 0f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        int animCnt = anim.runtimeAnimatorController.animationClips.Length;
        for(int i=0; i<animCnt; i++)
        {
            if (anim.runtimeAnimatorController.animationClips[i].name == "RT")
            {
                turnAnim = anim.runtimeAnimatorController.animationClips[i];
                break;
            }
        }
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, 100, 100), "회전"))
        {
            Rotate();
        }
        if (GUI.Button(new Rect(100, 0, 100, 100), "평상시"))
        {
            Quaternion stQ = Quaternion.Euler(0,0,0);
            this.transform.rotation = stQ;
        }
    }

    public void Rotate()
    {
        StartCoroutine(RoateCor());
    }

    IEnumerator RoateCor()
    {
        Quaternion stQ = Quaternion.identity;
        Quaternion enQ =  Quaternion.Euler(0,yAngle, 0);

        if (yAngle < 0)
            anim.SetBool("Right", false);
        else
            anim.SetBool("Right", true);

        anim.SetTrigger("Turn");
        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            this.transform.rotation = Quaternion.Lerp(stQ, enQ, timer);
            yield return null;
        }
    }
}
