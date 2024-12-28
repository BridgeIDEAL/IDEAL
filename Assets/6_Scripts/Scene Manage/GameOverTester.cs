using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTester : MonoBehaviour
{
    #if UNITY_EDITOR
    

    // Update is called once per frame
    void Update()
    {
        // Check numeric keys 0-9 for deathIndex
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                TriggerGameOver(i);
                break;
            }
        }
        if(Input.GetKeyDown(KeyCode.P)){
            TriggerGameOver(10);
        }
    }

    // Function to trigger game over with the specified deathIndex
    private void TriggerGameOver(int deathIndex)
    {
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.gameOverManager.GameOverWithVHSEffect(deathIndex);
        Debug.Log($"GameOver triggered with deathIndex: {deathIndex}");
    }

    #endif
}
