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

        SceneManager.sceneLoaded += AfterSceneLoaded;
    }

    private void AfterSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "Prototype"){
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if(scene.name == "Lobby"){
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void LoadGameScene(){
        // 현재는 프로토타입 Load
        LoadingImageManager.Instance.ActiveLoadingImage();
        SceneManager.LoadScene("Prototype");
    }

    public void LoadLobbyScene(){
        SceneManager.LoadScene("Lobby");
    }

    public void AddAttemptCount(){
        CountAttempts.Instance.AddAttemptCount();
    }

    public void ExitGame(){
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("isPlaying = false");
#else
        Application.Quit();
        Debug.Log("Application.Quit");
#endif
    }
}
