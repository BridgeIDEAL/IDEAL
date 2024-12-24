using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVManager : MonoBehaviour
{
    private static PVManager instance;
    public static PVManager Instance{
        get{
            if(instance == null){
                return null;
            }
            return instance;
        }
    }
    [SerializeField] private GameObject silenceButton;
    [SerializeField] private GameObject eyePenaltyButton;
    [SerializeField] private GameObject breathandEyePenaltyButton;
    [SerializeField] private GameObject leftLegHurtButton;
    [SerializeField] private GameObject rightLegHurtButton;

    private bool isClose = true;

    public float breathValue = 0.0f;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else if(instance != this){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2)){
            silenceButton.SetActive(!silenceButton.activeSelf);
            eyePenaltyButton.SetActive(!eyePenaltyButton.activeSelf);
            breathandEyePenaltyButton.SetActive(!breathandEyePenaltyButton.activeSelf);
            leftLegHurtButton.SetActive(!leftLegHurtButton.activeSelf);
            rightLegHurtButton.SetActive(!rightLegHurtButton.activeSelf);
        }
    }

    public void MakeSilence(){
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.SetSilenceAmbience();
    }

    public void MakeEyePenalty(){
        PenaltyPointManager.Instance.eyePenaltyStepTimer = 50.0f;
    }

    public void MakeBreathandEyePenalty(){
        StartCoroutine(MakeBreathandEyePenaltyCoroutine());
    }

    IEnumerator MakeBreathandEyePenaltyCoroutine(){
        yield return new WaitForSeconds(1.0f);

        float stepTimer = 0.0f;
        breathValue = 1.0f;
        float breathingTime = 6.0f;
        while(stepTimer < breathingTime){
            breathValue = Mathf.Lerp(1.0f, 0.0f, stepTimer / breathingTime);
            stepTimer += Time.deltaTime;
            if(stepTimer >= (breathingTime - 1.0f)){
                PenaltyPointManager.Instance.eyePenaltyStepTimer = 59.9f;
                break;
            }
            yield return null;
        }

        
    }

    public void MakeLeftLegHurt(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.LeftLeg, 1);
    }
    public void MakeRightLegHurt(){
        HealthPointManager.Instance.Hurt(IdealBodyPart.RightLeg, 1);
    }
}
