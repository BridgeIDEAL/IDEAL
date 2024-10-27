using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareEvent : MonoBehaviour
{
    JumpScare jumpScare = null;
    MainCamEffect mainCamEffect = null;
    private void Awake()
    {
        if (jumpScare == null)
            jumpScare = GetComponentInParent<JumpScare>();
        if (mainCamEffect == null)
            mainCamEffect = Camera.main.GetComponent<MainCamEffect>();
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

    /// <summary>
    /// JumpScare : Reverse Girl
    /// </summary>
    public void SetFieldOfView()
    {
        mainCamEffect.CallGraduallySetFieldOfView(14f,0.1f);
    }
}
