using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoardDeath : MonoBehaviour
{
    [SerializeField] string deathReason;
    public bool SuddenDeath { get; set; } = true;
    private void OnTriggerEnter(Collider other)
    {
        if (SuddenDeath)
        {
            SuddenDeath = false;
            GameOver(deathReason);
        }
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
