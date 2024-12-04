using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILobby : MonoBehaviour
{
    public GameObject BookUpdatedGameObject;

    private bool isSelectedButtonPressed = false;

    void Update(){
        BookUpdatedGameObject.SetActive(GuideLogManager.Instance.guideLogUpdated);
    }

    public void StageSelectButtonDown(){
        if(isSelectedButtonPressed) return;
        isSelectedButtonPressed = true;
        IdealSceneManager.Instance.LoadGameScene();
    }
}
