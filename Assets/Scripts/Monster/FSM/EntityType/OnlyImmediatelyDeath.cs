using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyImmediatelyDeath : MonoBehaviour
{
    public string deathReason;

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
