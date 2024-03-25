using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    Animator anim;
    [SerializeField]GameObject player;
    bool head = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("L", true);
            head = true;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            head = false;
            anim.SetBool("L", false);
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (head)
        {
            anim.SetLookAtPosition(player.transform.position + Vector3.up);
            anim.SetLookAtWeight(1, 0, 1);
        }
      
    }
}
