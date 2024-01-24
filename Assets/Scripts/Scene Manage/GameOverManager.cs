using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    private static GameOverManager instance = null;
    public static GameOverManager Instance{
        get{
            if(instance == null) return null;
            return instance;
        }
    }

    [SerializeField] private GameObject backgroundObject;
    [SerializeField] private GameObject endingMentObject;
    [SerializeField] private GameObject pressKeyTextObject;
    private float pressKeyTime = 1.0f;
    private TextMeshProUGUI endingMentText;
    private bool isEnd = false;
    private float stepTimer = 0.0f;
    

    private void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
        backgroundObject.SetActive(false);
        endingMentObject.SetActive(false);
        endingMentText = endingMentObject.GetComponent<TextMeshProUGUI>();
        pressKeyTextObject.SetActive(false);
    }

    public void GameOver(string endingMent){
        backgroundObject.SetActive(true);
        endingMentText.text = endingMent;
        endingMentObject.SetActive(true);
        isEnd = true;
        stepTimer = 0.0f;
        GuideLogManager.Instance.SaveGuideLogRecordList();
    }

    private void Update(){
        if(isEnd){
            if(stepTimer >= pressKeyTime){
                pressKeyTextObject.SetActive(true);
                if(Input.anyKeyDown){
                    IdealSceneManager.Instance.LoadLobbyScene();
                }
            }
            stepTimer += Time.deltaTime;
        }
    }
}
