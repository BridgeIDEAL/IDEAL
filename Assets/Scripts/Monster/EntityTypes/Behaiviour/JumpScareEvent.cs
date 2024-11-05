using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareEvent : MonoBehaviour
{
    [SerializeField, Header("Death Scene Index")] int deathIndex;
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
        jumpScare.GameOver(deathIndex);
    }

    /// <summary>
    /// JumpScare : Reverse Girl
    /// </summary>
    public void SetFieldOfView()
    {
        mainCamEffect.CallGraduallySetFieldOfView(14f,0.1f);
    }
}
