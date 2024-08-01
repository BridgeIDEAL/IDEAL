using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class OnlyImmediatelyDeath : MonoBehaviour
{
    public string deathReason;
    
    private void Awake()
    {
        // 카메라 블랜드 방식을 컷으로 변경
        Camera mainCam = Camera.main;
        CinemachineBrain cb = mainCam.GetComponent<CinemachineBrain>();
        cb.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
    }

    public void Death()
    {
        GameOver(deathReason);
    }

    public void GameOver(string str, int guideLogID = -1)
    {
        int attempts = CountAttempts.Instance.GetAttemptCount();
        if (str.Contains("$attempts"))
        {
            str = str.Replace("$attempts", attempts.ToString());
        }
        if (guideLogID > -1)
        {
            GuideLogManager.Instance.UpdateGuideLogRecord(guideLogID, attempts);
        }
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(str);
    }
}
