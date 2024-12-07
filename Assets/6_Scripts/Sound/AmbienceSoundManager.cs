using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IdealArea{
    Outside,
    Inside,
}

public class AmbienceSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip silenceAudioClip;
    [SerializeField] private AudioSource outsideAudioSource;
    [SerializeField] private AudioSource insideAudioSource;
    public GuardCCTVSound guardCCTVSound;
    [SerializeField] private CareerDevelopSound careerDevelopSound;
    [SerializeField] private AudioSource chaseAudioSource;
    [SerializeField] private AudioSource lastRunAudioSource_1;
    [SerializeField] private AudioSource lastRunAudioSource_2;

    [SerializeField] private AudioSource lookOutAudioSource_1;
    [SerializeField] private AudioSource lookOutAudioSource_2;

    [SerializeField] private AudioSource musicRoomAudioSource;
    [SerializeField] private AudioSource broadCastRoomAudioSource;
    [SerializeField] private AudioSource serverRoomAudioSource;

    [SerializeField] private AudioSource tinnitusAudioSource;

    private Coroutine audioCoroutine;
    public IdealArea currentArea = IdealArea.Outside;
    private bool isChased = false;

    private Coroutine lookoutAudioCoroutine;

    [SerializeField] private float insideAudioVolume;
    [SerializeField] private float outsideAudioVolume;
    [SerializeField] private float chaseAudioVolume;
    [SerializeField] private float lastRunAudioVolume;
    [SerializeField] private float lookOutAudioVolume;
    [SerializeField] private float musicRoomAudioVolume;
    [SerializeField] private float broadCastRoomAudioVolume;
    [SerializeField] private float serverRoomAudioVolume;
    [SerializeField] private float soundFadeTime = 0.7f;

    public void SetSilenceAmbience(){
        chaseAudioSource.clip = silenceAudioClip;
        lastRunAudioSource_1.clip = silenceAudioClip;
        lastRunAudioSource_2.clip = silenceAudioClip;
    }


    void Awake(){
        outsideAudioSource.volume = outsideAudioVolume;
        insideAudioSource.volume = insideAudioVolume;
    }

    void Start(){
        if(ProgressManager.Instance.lastRunning){
            lastRunAudioSource_1.time = ProgressManager.Instance.lastRunningTime % lastRunAudioSource_1.clip.length;
            lastRunAudioSource_2.time = ProgressManager.Instance.lastRunningTime % lastRunAudioSource_2.clip.length;
            LastRunStart();
        }
    }

    public void SoundFadeIn(bool isOutSide){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        if(isOutSide){
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(outsideAudioSource, outsideAudioVolume, soundFadeTime*2, true));
        }
        else{
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(insideAudioSource, insideAudioVolume, soundFadeTime*2 , true));
        }
    }

    
    public void SetTinnitusSound(float volume){
        if(!tinnitusAudioSource.isPlaying){
            tinnitusAudioSource.Play();
        }
        tinnitusAudioSource.volume = volume;
    }

    public void OffTinnitusSound(){
        tinnitusAudioSource.volume = 0.0f;
        tinnitusAudioSource.Stop();
    }
    
    void Update()
    {

    }

    public void UpdateAreaCondition(bool isColliderEnter, IdealArea areaCondition){
        if(isColliderEnter){
            // Outside에 들어갔으므로  Ouside 앰비언스로 변경
            currentArea = IdealArea.Outside;
            if(audioCoroutine != null){
                StopCoroutine(audioCoroutine);
            }
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(outsideAudioSource, outsideAudioVolume, soundFadeTime*2, true));
        }
        else{
            // Outside에서 나왔으므로 Inside 앰비언스로 변경
            currentArea = IdealArea.Inside;
            if(audioCoroutine != null){
                StopCoroutine(audioCoroutine);
            }
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(insideAudioSource, insideAudioVolume, soundFadeTime*2 , true));
        }
    }

    private IEnumerator SoundFadeCoroutine(AudioSource fadeAudioSource, float fadeDestAudioVol, float fadeTime, bool needPlayOn){
        if(ProgressManager.Instance.lastRunning){
            // LastRun 중이면 다른 사운드가 켜지지 않도록 함
            yield break;
        }
        
        float outsideAudioVol = outsideAudioSource.volume;
        float insideAudioVol = insideAudioSource.volume;
        float chaseAudioVol = chaseAudioSource.volume;
        float lastRunAudioVol_1 = lastRunAudioSource_1.volume;
        float lastRunAudioVol_2 = lastRunAudioSource_2.volume;
        float lookOutAudioVol_1 = lookOutAudioSource_1.volume;
        float lookOutAudioVol_2 = lookOutAudioSource_2.volume;
        float musicRoomAudioVol = musicRoomAudioSource.volume;
        float broadCastRoomAudioVol = broadCastRoomAudioSource.volume;
        float serverRoomAudioVol = serverRoomAudioSource.volume;

        float fadeAudioVol = fadeAudioSource.volume;

        if(needPlayOn) fadeAudioSource.Play();

        float stepTimer = 0.0f;

        while(stepTimer <= fadeTime){
            // 모든 Audio 소리를 FadeOut
            outsideAudioSource.volume = Mathf.Lerp(outsideAudioVol, 0.0f, stepTimer / fadeTime);
            insideAudioSource.volume = Mathf.Lerp(insideAudioVol, 0.0f, stepTimer / fadeTime);
            chaseAudioSource.volume = Mathf.Lerp(chaseAudioVol, 0.0f, stepTimer / fadeTime);
            lastRunAudioSource_1.volume = Mathf.Lerp(lastRunAudioVol_1, 0.0f, stepTimer / fadeTime);
            lastRunAudioSource_2.volume = Mathf.Lerp(lastRunAudioVol_2, 0.0f, stepTimer / fadeTime);
            lookOutAudioSource_1.volume = Mathf.Lerp(lookOutAudioVol_1, 0.0f, stepTimer / fadeTime);
            lookOutAudioSource_2.volume = Mathf.Lerp(lookOutAudioVol_2, 0.0f, stepTimer / fadeTime);
            musicRoomAudioSource.volume = Mathf.Lerp(musicRoomAudioVol, 0.0f, stepTimer / fadeTime);
            broadCastRoomAudioSource.volume = Mathf.Lerp(broadCastRoomAudioVol, 0.0f, stepTimer / fadeTime);
            serverRoomAudioSource.volume = Mathf.Lerp(serverRoomAudioVol, 0.0f, stepTimer / fadeTime);

            // 선택한 Audio의 소리를 FadeIn
            fadeAudioSource.volume = Mathf.Lerp(fadeAudioVol, fadeDestAudioVol, stepTimer / fadeTime);

            stepTimer += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator SoundFadeCoroutine(List<AudioSource> fadeAudioSourceList, List<float> fadeDestAudioVolList, float fadeTime, bool needPlayOn){
        if(ProgressManager.Instance.lastRunning){
            // 현재 Lists는 LastRun만 사용하므로 추가 처리 하지 않음
            // 만약 이 코루틴을 다른 오디오가 사용하면 추가 처리 필요
        }
        
        float outsideAudioVol = outsideAudioSource.volume;
        float insideAudioVol = insideAudioSource.volume;
        float chaseAudioVol = chaseAudioSource.volume;
        float lastRunAudioVol_1 = lastRunAudioSource_1.volume;
        float lastRunAudioVol_2 = lastRunAudioSource_2.volume;
        float lookOutAudioVol_1 = lookOutAudioSource_1.volume;
        float lookOutAudioVol_2 = lookOutAudioSource_2.volume;
        float musicRoomAudioVol = musicRoomAudioSource.volume;
        float broadCastRoomAudioVol = broadCastRoomAudioSource.volume;
        float serverRoomAudioVol = serverRoomAudioSource.volume;

        List<float> fadeAudioVolList = new List<float>();
        for(int i = 0; i < fadeAudioSourceList.Count; i++){
            fadeAudioVolList.Add(fadeAudioSourceList[i].volume);
        }

        if(needPlayOn){
            for(int i = 0; i < fadeAudioSourceList.Count; i++){
                fadeAudioSourceList[i].Play();
            }
        }

        float stepTimer = 0.0f;

        while(stepTimer <= fadeTime){
            // 모든 Audio 소리를 FadeOut
            outsideAudioSource.volume = Mathf.Lerp(outsideAudioVol, 0.0f, stepTimer / fadeTime);
            insideAudioSource.volume = Mathf.Lerp(insideAudioVol, 0.0f, stepTimer / fadeTime);
            chaseAudioSource.volume = Mathf.Lerp(chaseAudioVol, 0.0f, stepTimer / fadeTime);
            lastRunAudioSource_1.volume = Mathf.Lerp(lastRunAudioVol_1, 0.0f, stepTimer / fadeTime);
            lastRunAudioSource_2.volume = Mathf.Lerp(lastRunAudioVol_2, 0.0f, stepTimer / fadeTime);
            lookOutAudioSource_1.volume = Mathf.Lerp(lookOutAudioVol_1, 0.0f, stepTimer / fadeTime);
            lookOutAudioSource_2.volume = Mathf.Lerp(lookOutAudioVol_2, 0.0f, stepTimer / fadeTime);
            musicRoomAudioSource.volume = Mathf.Lerp(musicRoomAudioVol, 0.0f, stepTimer / fadeTime);
            broadCastRoomAudioSource.volume = Mathf.Lerp(broadCastRoomAudioVol, 0.0f, stepTimer / fadeTime);
            serverRoomAudioSource.volume = Mathf.Lerp(serverRoomAudioVol, 0.0f, stepTimer / fadeTime);

            // 선택한 Audio들의 소리를 FadeIn
            for(int i = 0; i < fadeAudioSourceList.Count; i++){
                fadeAudioSourceList[i].volume = Mathf.Lerp(fadeAudioVolList[i], fadeDestAudioVolList[i], stepTimer / fadeTime);
            }

            stepTimer += Time.deltaTime;
            yield return null;
        }
    }


    


    public void ChaseStart(){
        isChased = true;
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        audioCoroutine = StartCoroutine(SoundFadeCoroutine(chaseAudioSource, chaseAudioVolume, soundFadeTime * 2.0f, true));
    }

    public void ChaseEnd(){
        isChased = false;
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        if(currentArea == IdealArea.Outside){
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(outsideAudioSource, outsideAudioVolume, soundFadeTime * 2.0f, true));
        }
        else{
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(insideAudioSource, insideAudioVolume, soundFadeTime * 2.0f, true));
        }
    }

    private void CheckNextAudio(){
        if(isChased){
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(chaseAudioSource, chaseAudioVolume, soundFadeTime, true));
            return;
        }
        
        if(currentArea == IdealArea.Outside){
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(outsideAudioSource, outsideAudioVolume, soundFadeTime, true));
            return;
        }
        else{
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(insideAudioSource, insideAudioVolume, soundFadeTime, true));
            return;
        }
    }


    public void LastRunStart(){
        ProgressManager.Instance.lastRunning = true;
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        audioCoroutine = StartCoroutine(SoundFadeCoroutine(new List<AudioSource>{lastRunAudioSource_1, lastRunAudioSource_2}, 
        new List<float>{lastRunAudioVolume, lastRunAudioVolume}, soundFadeTime * 2.0f, true));
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
        lookOutAudioSource_1.volume = 0.0f;
        lookOutAudioSource_1.Play();
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, 0.0f, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, 0.0f, stepTimer / fadeTime);
            lookOutAudioSource_1.volume = Mathf.Lerp(0.0f, lookOutAudioVolume, stepTimer/ fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }

        while(stepTimer <= lookOutAudioSource_1.clip.length * 0.7f){
            stepTimer += Time.deltaTime;
            yield return null;
        }

        stepTimer = 0.0f;
        float lookOutVol_1 = lookOutAudioSource_1.volume;
        float lookOutVol_2 = lookOutAudioSource_2.volume;
        lookOutAudioSource_2.Play();
        
        while(stepTimer <= fadeTime){
            lookOutAudioSource_1.volume = Mathf.Lerp(lookOutVol_1, 0.0f, stepTimer / fadeTime);
            lookOutAudioSource_2.volume = Mathf.Lerp(lookOutVol_2, lookOutAudioVolume, stepTimer / fadeTime);
            
            stepTimer +=Time.deltaTime;
            yield return null;
        }
        lookOutAudioSource_1.Stop();

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
        float lookOutVol_1 = lookOutAudioSource_1.volume;
        float lookOutVol_2 = lookOutAudioSource_2.volume;
        float outsideDestVol = (currentArea == IdealArea.Outside) ? outsideAudioVolume : 0.0f;
        float insideDestVol = (currentArea == IdealArea.Inside) ? insideAudioVolume : 0.0f;

        float chaseVol = chaseAudioSource.volume;
        float chaseDestVol = 0.0f;
        if(isChased){
            outsideDestVol = 0.0f;
            insideDestVol = 0.0f;
            chaseDestVol = chaseAudioVolume;
        }

        float stepTimer = 0.0f;
        float fadeTime = soundFadeTime * 2.0f;
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, insideDestVol, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, outsideDestVol, stepTimer / fadeTime);
            lookOutAudioSource_1.volume = Mathf.Lerp(lookOutVol_1, 0.0f, stepTimer/ fadeTime);
            lookOutAudioSource_2.volume = Mathf.Lerp(lookOutVol_2, 0.0f, stepTimer/ fadeTime);
            chaseAudioSource.volume = Mathf.Lerp(chaseVol, chaseDestVol, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        lookOutAudioSource_1.Stop();
        lookOutAudioSource_2.Stop();
    }

    public void MusicRoomStart(){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        audioCoroutine = StartCoroutine(SoundFadeCoroutine(musicRoomAudioSource, musicRoomAudioVolume, soundFadeTime, false));
    }


    public void MusicRoomEnd(){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }

        CheckNextAudio();
    }

    public void BroadCastRoomStart(){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        audioCoroutine = StartCoroutine(SoundFadeCoroutine(broadCastRoomAudioSource, broadCastRoomAudioVolume, soundFadeTime, true));
    }

    public void BroadCastRoomEnd(){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }

        CheckNextAudio();
    }

    public void ServerRoomStart(){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        audioCoroutine = StartCoroutine(SoundFadeCoroutine(serverRoomAudioSource, serverRoomAudioVolume, soundFadeTime, true));
    }

    public void ServerRoomEnd(){
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }

        CheckNextAudio();
    }

    public void CareerDevelopSoundPlay(){
        careerDevelopSound.PlayAudio();
    }
}
