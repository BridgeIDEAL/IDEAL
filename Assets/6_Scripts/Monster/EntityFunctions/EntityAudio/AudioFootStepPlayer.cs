using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFootStepPlayer : AudioSFXPlayer
{
    [SerializeField] AudioClip[] footStepClips;
    [SerializeField] AudioClip[] runStepClips;
    int randNum;
    int lastFootIndex = -1;
    int lastRunIndex = -1;
    int footStepCnt = -1;
    int runStepCnt = -1;

    private Coroutine soundCoroutine;
    private float fadeTime = 1.0f;
    private float floorInterval = 1.0f;
    private float footSoundVolume;
    private bool isPlayerFootHear = false;
    private Transform playerTransform = null;

    protected override void Awake()
    {
        base.Awake();
        footStepCnt=footStepClips.Length;
        runStepCnt = runStepClips.Length;
        footSoundVolume = source.volume;
    }


    private void Update() {
        if(playerTransform == null){
            if(IdealSceneManager.Instance.CurrentGameManager != null ){
                playerTransform = IdealSceneManager.Instance.CurrentGameManager.scriptHub.playerArmatureObject.transform;
                return;
            }
        }
        // 앞서 넣더라도 해당 프레임에 position에 접근하면 Null 에러 뜸
        if(playerTransform == null) return;


        if(playerTransform.position.y >= this.transform.position.y - floorInterval && 
        playerTransform.position.y <= this.transform.position.y + floorInterval){
            // 플레이어가 이형체와 층 범위가 같을 때
            if(!isPlayerFootHear){
                // 이형체의 발소리를 듣고 있지 않다면
                FootSoundFade(true);
            }
        }
        else{
            // 플레이어가 이형체와 층 범위가 다를 때
            if(isPlayerFootHear){
                // 이형체의 발소리를 듣고 있다면
                FootSoundFade(false);
            }
        }
    }

    public void FootStep()
    {
        do
        {
            randNum = Random.Range(0, footStepCnt);
        }
        while (randNum == lastFootIndex);
        lastFootIndex = randNum;
        source.PlayOneShot(footStepClips[lastFootIndex]);
    }

    public void RunStep()
    {
        do
        {
            randNum = Random.Range(0, runStepCnt);
        }
        while (randNum == lastRunIndex);
        lastRunIndex = randNum;
        source.PlayOneShot(runStepClips[lastRunIndex]);
    }

    private void FootSoundFade(bool isFadeIn){
        if(soundCoroutine != null){
            StopCoroutine(soundCoroutine);
        }
        soundCoroutine = StartCoroutine(FootSoundFadeCoroutine(isFadeIn));
    }

    private IEnumerator FootSoundFadeCoroutine(bool isFadeIn){
        isPlayerFootHear = isFadeIn;

        float stepTimer = 0.0f;
        float soundVol = source.volume;
        float destVol = isFadeIn ? footSoundVolume : 0.0f;

        while(stepTimer <= fadeTime){
            source.volume = Mathf.Lerp(soundVol, destVol, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }

    }
}
