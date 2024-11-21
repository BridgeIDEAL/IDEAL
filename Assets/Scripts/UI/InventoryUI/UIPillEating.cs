using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPillEating : MonoBehaviour
{
    public void EatPill(){
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.ActivePillUI(false);
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOverWithVHSEffect(8);
        if (SteamfeatureController.Instance.FeatureManager.Achievement04.isPillDeath == false)
        {
            SteamfeatureController.Instance.FeatureManager.Achievement04.isPillDeath = true;
            SteamfeatureController.Instance.FeatureManager.Achievement04.CheckAllConidtion();
        }
    }

    public void NotEatPill(){
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.ActivePillUI(false);
    }
}
