using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILobby : MonoBehaviour
{
    public GameObject BookUpdatedGameObject;

    void Update(){
        BookUpdatedGameObject.SetActive(GuideLogManager.Instance.guideLogUpdated);
    }

    public void StageSelectButtonDown(){
        IdealSceneManager.Instance.LoadGameScene();
    }
}
