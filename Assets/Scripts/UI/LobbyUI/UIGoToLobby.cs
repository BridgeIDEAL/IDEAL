using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGoToLobby : MonoBehaviour
{
    public void GoToLobby(){
        CountAttempts.Instance.AddAttemptCount();
        GuideLogManager.Instance.SavePlayerSaveData();
        IdealSceneManager.Instance.LoadLobbyScene();
    }
}
