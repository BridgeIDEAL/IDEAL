using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGoToLobby : MonoBehaviour
{
    public void GoToLobby(){
        IdealSceneManager.Instance.LoadLobbyScene();
    }
}
