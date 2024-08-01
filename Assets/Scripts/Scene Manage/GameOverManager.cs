using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private ScriptHub scriptHub;
    private ThirdPersonController thirdPersonController;
    [SerializeField] private GameObject backgroundObject;
    [SerializeField] private GameObject endingMentObject;
    [SerializeField] private GameObject pressKeyTextObject;
    private float pressKeyTime = 1.0f;
    private TextMeshProUGUI endingMentText;
    private bool isEnd = false;
    private float stepTimer = 0.0f;
    

    private void Awake(){
        backgroundObject.SetActive(false);
        endingMentObject.SetActive(false);
        endingMentText = endingMentObject.GetComponent<TextMeshProUGUI>();
        pressKeyTextObject.SetActive(false);
        thirdPersonController = scriptHub.thirdPersonController;
    }

    public void GameOver(string endingMent, int stateNum = 0){
        backgroundObject.SetActive(true);
        endingMent = endingMent.Replace("$attempts", CountAttempts.Instance.GetAttemptCount().ToString());
        endingMentText.text = endingMent;
        endingMentObject.SetActive(true);
        isEnd = true;
        stepTimer = 0.0f;
        CountAttempts.Instance.AddAttemptCount();
        GuideLogManager.Instance.SavePlayerSaveData();
        ArchiveLog archiveLog = new ArchiveLog(CountAttempts.Instance.GetAttemptCount(), ArchiveLogManager.Instance.GetArchiveState(stateNum), endingMent);
        ArchiveLogManager.Instance.AddArchiveLog(archiveLog);
    }

    public void GameOver(int stateNum=0){
        backgroundObject.SetActive(true);
        string endingMent = ArchiveLogManager.Instance.GetArchiveText(stateNum);
        endingMent = endingMent.Replace("$attempts", CountAttempts.Instance.GetAttemptCount().ToString());
        endingMentText.text = endingMent;
        endingMentObject.SetActive(true);
        isEnd = true;
        stepTimer = 0.0f;
        CountAttempts.Instance.AddAttemptCount();
        GuideLogManager.Instance.SavePlayerSaveData();
        ArchiveLog archiveLog = new ArchiveLog(CountAttempts.Instance.GetAttemptCount(), ArchiveLogManager.Instance.GetArchiveState(stateNum), endingMent);
        ArchiveLogManager.Instance.AddArchiveLog(archiveLog);
    }

    private void Update(){
        if(isEnd){
            thirdPersonController.MoveLock = true;
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
