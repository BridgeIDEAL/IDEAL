using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    public Transform Player;
    private bool isLookPlayer = false;
    private float lookWeight = 0f;
    private float headWeight=0.5F;
    private float bodyWeight=0.5F;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        isLookPlayer = false;
       
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            isLookPlayer = true;
        if (Input.GetKeyDown(KeyCode.Z))
            StartCoroutine("HeadRotating");
        if (Input.GetKeyDown(KeyCode.X))
            Cal();    
    }

    public void Cal()
    {
        Vector3 dir1 = transform.forward;
        Vector3 dir2 = Player.transform.position-transform.position;
        float magnitude = Vector3.Dot(dir1, dir2);
        if (magnitude < 0)
            Debug.Log("뒤에 있다.");
        else
            Debug.Log("앞에 있다.");
    }

    public IEnumerator HeadRotating()
    {
        float current = 0;
        float percent = 0;
        float timer = 2f;
        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / timer;
            lookWeight = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (isLookPlayer)
        {
            anim.SetLookAtPosition(Player.transform.position);
            anim.SetLookAtWeight(lookWeight, bodyWeight, headWeight);
        }
    }
}
