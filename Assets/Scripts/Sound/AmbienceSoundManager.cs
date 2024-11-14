using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IdealArea{
    Outside,
    Inside,
}

public class AmbienceSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource outsideAudioSource;
    [SerializeField] private AudioSource insideAudioSource;
    public GuardCCTVSound guardCCTVSound;
    [SerializeField] private CareerDevelopSound careerDevelopSound;
    [SerializeField] private AudioSource chaseAudioSource;
    [SerializeField] private AudioSource lastRunAudioSource_1;
    [SerializeField] private AudioSource lastRunAudioSource_2;

    [SerializeField] private AudioSource lookOutAudioSource;
    [SerializeField] private AudioClip[] lookOutAudioClip = new AudioClip[2];
    [SerializeField] private AudioSource musicRoomAudioSource;
    [SerializeField] private AudioSource broadCastRoomAudioSource;
    [SerializeField] private AudioSource serverRoomAudioSource;

    private Coroutine audioCoroutine;
    public IdealArea currentArea = IdealArea.Outside;

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


    void Awake(){
        outsideAudioSource.volume = outsideAudioVolume;
        insideAudioSource.volume = insideAudioVolume;
    }

    void Start(){
        if(ProgressManager.Instance.lastRunning){
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
                    audioCoroutine = StartCoroutine(SoundFadeCoroutine(insideAudioSource, insideAudioVolume, soundFadeTime*2 , true));
                }
                else if(currentArea == IdealArea.Inside){
                    currentArea = IdealArea.Outside;
                    if(audioCoroutine != null){
                        StopCoroutine(audioCoroutine);
                    }
                    audioCoroutine = StartCoroutine(SoundFadeCoroutine(outsideAudioSource, outsideAudioVolume, soundFadeTime*2, true));
                }
                else{
                    Debug.Log(" Un expected 1");
                }
            }
        }
    }

    private IEnumerator SoundFadeCoroutine(AudioSource fadeAudioSource, float fadeDestAudioVol, float fadeTime, bool needPlayOn){
        float outsideAudioVol = outsideAudioSource.volume;
        float insideAudioVol = insideAudioSource.volume;
        float chaseAudioVol = chaseAudioSource.volume;
        float lastRunAudioVol_1 = lastRunAudioSource_1.volume;
        float lastRunAudioVol_2 = lastRunAudioSource_2.volume;
        float lookOutAudioVol = lookOutAudioSource.volume;
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
            lookOutAudioSource.volume = Mathf.Lerp(lookOutAudioVol, 0.0f, stepTimer / fadeTime);
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
        float outsideAudioVol = outsideAudioSource.volume;
        float insideAudioVol = insideAudioSource.volume;
        float chaseAudioVol = chaseAudioSource.volume;
        float lastRunAudioVol_1 = lastRunAudioSource_1.volume;
        float lastRunAudioVol_2 = lastRunAudioSource_2.volume;
        float lookOutAudioVol = lookOutAudioSource.volume;
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
            lookOutAudioSource.volume = Mathf.Lerp(lookOutAudioVol, 0.0f, stepTimer / fadeTime);
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
        if(audioCoroutine != null){
            StopCoroutine(audioCoroutine);
        }
        audioCoroutine = StartCoroutine(SoundFadeCoroutine(chaseAudioSource, chaseAudioVolume, soundFadeTime * 2.0f, true));
    }

    public void ChaseEnd(){
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
        lookOutAudioSource.volume = 0.0f;
        lookOutAudioSource.clip = lookOutAudioClip[0];
        lookOutAudioSource.Play();
        Invoke("LookOutSecondAudioClipPlay", lookOutAudioClip[0].length);
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, 0.0f, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, 0.0f, stepTimer / fadeTime);
            lookOutAudioSource.volume = Mathf.Lerp(0.0f, lookOutAudioVolume, stepTimer/ fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
    }

    void LookOutSecondAudioClipPlay(){
        lookOutAudioSource.clip = lookOutAudioClip[1];
        lookOutAudioSource.Play();
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
        float lookOutVol = lookOutAudioSource.volume;
        float outsideDestVol = (currentArea == IdealArea.Outside) ? outsideAudioVolume : 0.0f;
        float insideDestVol = (currentArea == IdealArea.Inside) ? insideAudioVolume : 0.0f;
        float stepTimer = 0.0f;
        float fadeTime = soundFadeTime * 2.0f;
        while(stepTimer <=fadeTime){
            insideAudioSource.volume = Mathf.Lerp(insideVol, insideDestVol, stepTimer / fadeTime);
            outsideAudioSource.volume = Mathf.Lerp(outsideVol, outsideDestVol, stepTimer / fadeTime);
            lookOutAudioSource.volume = Mathf.Lerp(lookOutVol, 0.0f, stepTimer/ fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        lookOutAudioSource.Stop();
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

        if(currentArea == IdealArea.Outside){
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(outsideAudioSource, outsideAudioVolume, soundFadeTime, true));
        }
        else{
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(insideAudioSource, insideAudioVolume, soundFadeTime, true));
        }
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

        if(currentArea == IdealArea.Outside){
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(outsideAudioSource, outsideAudioVolume, soundFadeTime, true));
        }
        else{
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(insideAudioSource, insideAudioVolume, soundFadeTime, true));
        }
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

        if(currentArea == IdealArea.Outside){
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(outsideAudioSource, outsideAudioVolume, soundFadeTime, true));
        }
        else{
            audioCoroutine = StartCoroutine(SoundFadeCoroutine(insideAudioSource, insideAudioVolume, soundFadeTime, true));
        }
    }

    public void CareerDevelopSoundPlay(){
        careerDevelopSound.PlayAudio();
    }
}
