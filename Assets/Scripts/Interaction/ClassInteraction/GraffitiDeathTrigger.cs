using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraffitiDeathTrigger : MonoBehaviour
{
    [SerializeField] int deathIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SteamfeatureController.Instance.FeatureManager.Achievement03.isBoardDeath == false)
            {
                SteamfeatureController.Instance.FeatureManager.Achievement03.isBoardDeath = true;
                SteamfeatureController.Instance.FeatureManager.Achievement03.CheckAllConidtion();
            }
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOverWithVHSEffect(deathIndex);
        }
    }
}
