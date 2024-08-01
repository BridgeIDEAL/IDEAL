using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPillEating : MonoBehaviour
{
    public void EatPill(){
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOver(8);
    }

    public void NotEatPill(){
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.ActivePillUI(false);
    }
}
