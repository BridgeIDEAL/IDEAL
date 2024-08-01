using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoardDeath : MonoBehaviour
{
    [SerializeField] int deathIndex;
    public bool SuddenDeath { get; set; } = true;
    private void OnTriggerEnter(Collider other)
    {
        if (SuddenDeath)
        {
            SuddenDeath = false;
            IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(deathIndex);
        }
    }
}
