using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineBlackLineEffect : MonoBehaviour
{
    [SerializeField] GameObject topObject;
    [SerializeField] GameObject botObject;
    [SerializeField] Animator topAnim;
    [SerializeField] Animator botAnim;

    public void StartCutScene()
    {
        topObject.SetActive(true);
        botObject.SetActive(true);
        topAnim.enabled = true;
        botAnim.enabled = true;
    }

    public void EndCutScene()
    {
        topAnim.Play("TimeLineAnim_TopEnd");
        botAnim.Play("TimeLineAnim_BottomEnd");
        Invoke("InActiveAnim", 1f);
    }

    public void InActiveAnim()
    {
        topAnim.enabled = false;
        botAnim.enabled = false;
        topObject.SetActive(false);
        botObject.SetActive(false);
    }
}
