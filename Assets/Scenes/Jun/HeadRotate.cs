using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotate : MonoBehaviour
{
    public Transform Player;
    private bool isLookPlayer = false;
    private float lookWeight = 1f;
    private float headWeight=1F;
    private float bodyWeight=0F;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        isLookPlayer = true;
       
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!Cal())
            {
                StartCoroutine("Cor");
                //Vector3 dir = Player.transform.position - transform.forward;
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), 180 * Time.deltaTime);
            }
        }
    }

    IEnumerator Cor()
    {
        float cur = 0f;
        float per = 0f;
        float speed = 3f;
        while(per <1f)
        {
            cur += Time.deltaTime;
            per = cur / speed;
            Quaternion tr = Quaternion.LookRotation(Player.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z),
                tr, per);
            yield return null;
        }
    }

    public bool Cal()
    {
        Vector3 dir1 = transform.forward;
        Vector3 dir2 = Player.transform.position-transform.position;
        float magnitude = Vector3.Dot(dir1, dir2);
        if (magnitude < 0)
            return false;
        else
            return true;
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
