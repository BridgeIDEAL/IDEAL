using System.Collections;
using UnityEngine;

public class GuardCCTVSound : MonoBehaviour
{
    [SerializeField]private AudioSource guardCCTVAudioSource;
    [SerializeField]private Transform playerTransform;
    public bool isTurnOn = false;
    private float floorYlimit = 7.5f;
    private float guardCCTVVolume = 0.2f;
    private Coroutine cctvCoroutine;
    private float fadeTime = 1.0f;

    void Update(){
        if(!PenaltyPointManager.Instance.watchIntroEnded){
            return;
        }
        if(isTurnOn && playerTransform.localPosition.y > floorYlimit){
            TurnOffCCTV();
        } 

        if(!isTurnOn && playerTransform.localPosition.y <= floorYlimit && IdealSceneManager.Instance.CurrentGameManager.scriptHub.ambienceSoundManager.currentArea == IdealArea.Inside){
            TurnOnCCTV();
        }
    }

    public void TurnOnCCTV(){
        if(cctvCoroutine != null){
            StopCoroutine(cctvCoroutine);
        }
        cctvCoroutine = StartCoroutine(TurnOnCCTVCoroutine());
    }

    private IEnumerator TurnOnCCTVCoroutine(){
        isTurnOn = true;
        guardCCTVAudioSource.volume = 0.0f;
        float volume = guardCCTVAudioSource.volume;
        float stepTimer = 0.0f;
        guardCCTVAudioSource.Play();
        while(stepTimer <= fadeTime){
            guardCCTVAudioSource.volume = Mathf.Lerp(volume, guardCCTVVolume, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        guardCCTVAudioSource.volume = guardCCTVVolume;
    }

    public void TurnOffCCTV(){
        if(cctvCoroutine != null){
            StopCoroutine(cctvCoroutine);
        }
        cctvCoroutine = StartCoroutine(TurnOffCCTVCoroutine());
    }

    private IEnumerator TurnOffCCTVCoroutine(){
        isTurnOn = false;
        float volume = guardCCTVAudioSource.volume;
        float stepTimer = 0.0f;
        while(stepTimer <= fadeTime){
            guardCCTVAudioSource.volume = Mathf.Lerp(volume, 0.0f, stepTimer / fadeTime);
            stepTimer += Time.deltaTime;
            yield return null;
        }
        guardCCTVAudioSource.volume = 0.0f;
        guardCCTVAudioSource.Stop();
    }
}
