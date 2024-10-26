using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandLight : MonoBehaviour
{
    [SerializeField] private Light handLight;
    // max 값들은 현재 handLight에 들어가 있는 값으로 대체
    private float maxSpotInnerAngle = 21.8f;   
    private float minSpotInnerAngle = 16.0f;
    private float maxSpotOuterAngle = 30.0f;
    private float minSpotOuterAngle = 25.5f;
    private float maxIntensity = 1.0f;
    private float minIntensity = 0.0f;

    private float[] blinkTimes = { 0.2f, 0.1f, 0.1f}; // 현재 가장 긴 blinkTime이 처음에 와야 자연스러움
    private float[] blinkStopTimes = { 0.4f, 0.1f, 0.1f};
    private float firstBlinkTime;
    private float combackBlinkTime = 1.0f;

    private Coroutine lightCoroutine;

    private void Awake(){
        maxSpotInnerAngle = handLight.innerSpotAngle;
        maxSpotOuterAngle = handLight.spotAngle;
        maxIntensity = handLight.intensity;

        firstBlinkTime = blinkTimes[0];
    }


    public void EffectOnLight(){
        if(lightCoroutine != null){
            StopCoroutine(lightCoroutine);
        }
        lightCoroutine = StartCoroutine(EffectOnLightCorouine());
    }

    public void EffectOffLight(){
        if(lightCoroutine != null){
            StopCoroutine(lightCoroutine);
        }
        lightCoroutine = StartCoroutine(EffectOffLightCoroutine());
    }

    private IEnumerator EffectOnLightCorouine(){
        float stepTimer = 0.0f;
        for( int i = 0; i < blinkTimes.Length; i++){
            stepTimer = 0.0f;

            float curSpotInnerAngle = handLight.innerSpotAngle;
            float curSpotOuterAngle = handLight.spotAngle;
            float curIntensity = handLight.intensity;
            
            // 손전등 꺼지는 효과
            while(stepTimer <= blinkTimes[i]){
                handLight.innerSpotAngle = Mathf.Lerp(curSpotInnerAngle, minSpotInnerAngle, stepTimer / blinkTimes[i]);
                handLight.spotAngle = Mathf.Lerp(curSpotOuterAngle, minSpotOuterAngle, stepTimer / blinkTimes[i]);
                handLight.intensity = Mathf.Lerp(curIntensity, minIntensity, stepTimer / blinkTimes[i]);

                stepTimer += Time.deltaTime;
                yield return null;
            }

            stepTimer = 0.0f;

            // 깜빡이는 기간이 짧을수록 범위나 밝기가 줄어들 것을 반영
            float curMaxSpotInnerAngle = minSpotInnerAngle + (maxSpotInnerAngle - minSpotInnerAngle) * (blinkTimes[i] / firstBlinkTime);
            float curMaxSpotOuterAngle = minSpotOuterAngle + (maxSpotOuterAngle - minSpotOuterAngle) * (blinkTimes[i] / firstBlinkTime);
            float curMaxIntensity = minIntensity + (maxIntensity - minIntensity) * (blinkTimes[i] / firstBlinkTime);

            // 손전등 다시 켜지는 효과
            while(stepTimer <= blinkTimes[i]){
                handLight.innerSpotAngle = Mathf.Lerp(minSpotInnerAngle, curMaxSpotInnerAngle, stepTimer / blinkTimes[i]);
                handLight.spotAngle = Mathf.Lerp(minSpotOuterAngle, curMaxSpotOuterAngle, stepTimer / blinkTimes[i]);
                handLight.intensity = Mathf.Lerp(minIntensity, curMaxIntensity, stepTimer / blinkTimes[i]);

                stepTimer += Time.deltaTime;
                yield return null;
            }
            
            yield return new WaitForSeconds(blinkStopTimes[i]);
        }
    }

    private IEnumerator EffectOffLightCoroutine(){
        float stepTimer = 0.0f;
        float curSpotInnerAngle = handLight.innerSpotAngle;
        float curSpotOuterAngle = handLight.spotAngle;
        float curIntensity = handLight.intensity;

        while(stepTimer <= combackBlinkTime){
            handLight.innerSpotAngle = Mathf.Lerp(curSpotInnerAngle, maxSpotInnerAngle, stepTimer / combackBlinkTime);
            handLight.spotAngle = Mathf.Lerp(curSpotOuterAngle, maxSpotOuterAngle, stepTimer / combackBlinkTime);
            handLight.intensity = Mathf.Lerp(curIntensity, maxIntensity, stepTimer / firstBlinkTime);

            stepTimer += Time.deltaTime;
            yield return null;
        }

    }
}
