using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVManager : MonoBehaviour
{
    [SerializeField] private GameObject silenceButton;
    [SerializeField] private GameObject eyePenaltyButton;

    private bool isClose = true;

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2)){
            silenceButton.SetActive(!silenceButton.activeSelf);
            eyePenaltyButton.SetActive(!eyePenaltyButton.activeSelf);
        }
    }

    public void MakeSilence(){
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.SetSilenceAmbience();
    }

    public void MakeEyePenalty(){
        PenaltyPointManager.Instance.eyePenaltyStepTimer = 50.0f;
    }
}
