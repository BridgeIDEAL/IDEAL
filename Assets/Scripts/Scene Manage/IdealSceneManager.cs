using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdealSceneManager : MonoBehaviour
{
    private static IdealSceneManager instance = null;
    public static IdealSceneManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    private void Awake() {
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }

    public void LoadGameScene(){
        // 현재는 프로토타입 Load
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Prototype");
    }

    public void LoadLobbyScene(){
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Lobby");
    }

    public void AddAttemptCount(){
        CountAttempts.Instance.AddAttemptCount();
    }
}
