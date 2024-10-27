using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareEvent : MonoBehaviour
{
    JumpScare jumpScare = null;
    private void Awake()
    {
        if (jumpScare == null)
            jumpScare = GetComponentInParent<JumpScare>();
    }

    public void GameOver()
    {
        if (jumpScare == null)
        {
            Debug.LogError("찾을 수 없다!");
            return;
        }
        jumpScare.GameOver();
    }
}
