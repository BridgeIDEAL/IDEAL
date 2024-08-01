using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class OnlyImmediatelyDeath : MonoBehaviour
{
    public int deathIndex;
    
    private void Awake()
    {
        // 카메라 블랜드 방식을 컷으로 변경
        Camera mainCam = Camera.main;
        CinemachineBrain cb = mainCam.GetComponent<CinemachineBrain>();
        cb.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
    }

    public void Death()
    {
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(deathIndex);
    }
}
