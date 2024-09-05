using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IdealArea{
    Outside,
    Inside,
    GuardRoom,
}

public class AmbienceSoundManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource outsideAudioSource;
    [SerializeField] private AudioSource insideAudioSource;
    [SerializeField] private GuardCCTVSound guardCCTVSound;
    [SerializeField] private AudioSource chaseAudioSource;
    [SerializeField] private AudioSource lastRunAudioSource_1;
    [SerializeField] private AudioSource lastRunAudioSource_2;

    [SerializeField] private AudioSource lookOutAudioSource;

    private Coroutine audioCoroutine;
    public IdealArea currentArea = IdealArea.Outside;

    private Coroutine chaseAudioCoroutine;
    private Coroutine lookoutAudioCoroutine;

    [SerializeField] private float insideAudioVolume;
    [SerializeField] private float outsideAudioVolume;
    [SerializeField] private float chaseAudioVolume;
    [SerializeField] private float lastRunAudioVolume;
    [SerializeField] private float lookOutAudioVolume;
    [SerializeField] private float soundFadeTime = 0.7f;

    private bool isLastRun = false;

    void Awake(){
        outsideAudioSource.volume = outsideAudioVolume;
        insideAudioSource.volume = insideAudioVolume;
    }

    public void SoundFadeIn(bool isOutSide){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        if(isOutSide){
            audioCoroutine = StartCoroutine(OutsideSoundCoroutine(soundFadeTime*2));
        }
        else{
            audioCoroutine = StartCoroutine(InsideSoundCoroutine(soundFadeTime*2));
        }
    }

    
    void Update()
    {

    }

    public void UpdateAreaCondition(bool isColliderEnter, IdealArea areaCondition){
        if(isColliderEnter){
            // 문 개념으로 ColliderExit 에 적용되도록 수정
        }
        else{
            if(areaCondition == IdealArea.Outside){
                if(currentArea == IdealArea.Outside){
                    currentArea = IdealArea.Inside;
                    if(audioCoroutine != null){
                        StopCoroutine(audioCoroutine);
                    }
                    audioCoroutine = StartCoroutine(InsideSoundCoroutine(soundFadeTime));
                }
                else if(currentArea == IdealArea.Inside){
                    currentArea = IdealArea.Outside;
                    if(audioCoroutine != null){
                        StopCoroutine(audioCoroutine);
                    }
                    audioCoroutine = StartCoroutine(OutsideSoundCoroutine(soundFadeTime));
                }
                else{
                    Debug.Log(" Un expected 1");
                }
            }
            if(areaCondition == IdealArea.GuardRoom){
                if(currentArea == IdealArea.Inside){
                    currentArea = IdealArea.GuardRoom;
                    guardCCTVSound.TurnOnCCTV();
                }
                else if(currentArea == IdealArea.GuardRoom){
                    currentArea = IdealArea.Inside;
                    guardCCTVSound.TurnOffCCTV();
                }
            }
        }
    }

    private IEnumerator InsideSoundCoroutine(float fadeTime){
        float outsideVol = outsideAudioSource.volume;
        insideAudioSource.volume = 0.0f;
        insideAudioSource.Play();
        float stepTimer = 0.0f;
        while(stepTimer <= fadeTime){
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, 0.0f, stepTimer / fadeTime);
            insideAudioSource.volume = Mathf.Lerp(0.0f, insideAudioVolume, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        outsideAudioSource.Stop();
    }

    private IEnumerator OutsideSoundCoroutine(float fadeTime){
        float insideVol = insideAudioSource.volume;
        outsideAudioSource.volume = 0.0f;
        outsideAudioSource.Play();
        float stepTimer = 0.0f;
        while(stepTimer <= fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, 0.0f, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(0.0f, outsideAudioVolume, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        insideAudioSource.Stop();
    }


    public void ChaseStart(){
        if(chaseAudioCoroutine != null){
            StopCoroutine(chaseAudioCoroutine);
        }
        chaseAudioCoroutine = StartCoroutine(ChaseStartCoroutine());
    }

    private IEnumerator ChaseEndCoroutine(){
        float insideVol = insideAudioSource.volume;
        float outsideVol = outsideAudioSource.volume;
        float outsideDestVol = (currentArea == IdealArea.Outside) ? outsideAudioVolume : 0.0f;
        float insideDestVol = (currentArea == IdealArea.Inside) ? insideAudioVolume : 0.0f;
        float stepTimer = 0.0f;
        float fadeTime = soundFadeTime * 2.0f;
        chaseAudioSource.volume = 0.0f;
        chaseAudioSource.Play();
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, insideDestVol, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, outsideDestVol, stepTimer / fadeTime);
            chaseAudioSource.volume = Mathf.Lerp(chaseAudioVolume, 0.0f, stepTimer/ fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        chaseAudioSource.Stop();
    }

    public void ChaseEnd(){
        if(chaseAudioCoroutine != null){
            StopCoroutine(chaseAudioCoroutine);
        }
        chaseAudioCoroutine = StartCoroutine(ChaseEndCoroutine());
    }

    private IEnumerator ChaseStartCoroutine(){
        float insideVol = insideAudioSource.volume;
        float outsideVol = outsideAudioSource.volume;
        float stepTimer = 0.0f;
        float fadeTime = soundFadeTime * 2.0f;
        chaseAudioSource.volume = 0.0f;
        chaseAudioSource.Play();
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, 0.0f, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, 0.0f, stepTimer / fadeTime);
            chaseAudioSource.volume = Mathf.Lerp(0.0f, chaseAudioVolume, stepTimer/ fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
    }

    public void LastRunStart(){
        isLastRun = true;
        StartCoroutine(LastRunStartCoroutine());
    }

    private IEnumerator LastRunStartCoroutine(){
        float insideVol = insideAudioSource.volume;
        float outsideVol = outsideAudioSource.volume;
        float stepTimer = 0.0f;
        float fadeTime = soundFadeTime * 2.0f;
        lastRunAudioSource_1.volume = 0.0f;
        lastRunAudioSource_1.Play();
        lastRunAudioSource_2.volume = 0.0f;
        lastRunAudioSource_2.Play();
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, 0.0f, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, 0.0f, stepTimer / fadeTime);
            lastRunAudioSource_1.volume = Mathf.Lerp(0.0f, lastRunAudioVolume, stepTimer/ fadeTime);
            lastRunAudioSource_2.volume = Mathf.Lerp(0.0f, lastRunAudioVolume, stepTimer/ fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
    }

    public void LookOutStart(){
        if(lookoutAudioCoroutine != null){
            StopCoroutine(lookoutAudioCoroutine);
        }
        lookoutAudioCoroutine = StartCoroutine(LookOutStartCoroutine());
    }

    private IEnumerator LookOutStartCoroutine(){
        float insideVol = insideAudioSource.volume;
        float outsideVol = outsideAudioSource.volume;
        float stepTimer = 0.0f;
        float fadeTime = soundFadeTime * 2.0f;
        lookOutAudioSource.volume = 0.0f;
        lookOutAudioSource.Play();
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, 0.0f, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, 0.0f, stepTimer / fadeTime);
            lookOutAudioSource.volume = Mathf.Lerp(0.0f, lookOutAudioVolume, stepTimer/ fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
    }

    public void LookOutEnd(){
        if(lookoutAudioCoroutine != null){
            StopCoroutine(lookoutAudioCoroutine);
        }
        lookoutAudioCoroutine = StartCoroutine(LookOutEndCoroutine());
    }

    private IEnumerator LookOutEndCoroutine(){
        float insideVol = insideAudioSource.volume;
        float outsideVol = outsideAudioSource.volume;
        float outsideDestVol = (currentArea == IdealArea.Outside) ? outsideAudioVolume : 0.0f;
        float insideDestVol = (currentArea == IdealArea.Inside) ? insideAudioVolume : 0.0f;
        float stepTimer = 0.0f;
        float fadeTime = soundFadeTime * 2.0f;
        lookOutAudioSource.volume = 0.0f;
        lookOutAudioSource.Play();
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, insideDestVol, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, outsideDestVol, stepTimer / fadeTime);
            lookOutAudioSource.volume = Mathf.Lerp(lookOutAudioVolume, 0.0f, stepTimer/ fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        chaseAudioSource.Stop();
    }
}
