using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlayerHandLight : MonoBehaviour
{
    [SerializeField] private Light handLight;
    [SerializeField] private ThirdPersonController thirdPersonController;
    // max 값들은 현재 handLight에 들어가 있는 값으로 대체
    [SerializeField] private float maxSpotInnerAngle;   
    private float minSpotInnerAngle = 16.0f;    //  현재는 max랑 동일하게 하여 intensity만 조절
    [SerializeField] private float maxSpotOuterAngle;
    private float minSpotOuterAngle = 25.5f;    //  현재는 max랑 동일하게 하여 intensity만 조절
    [SerializeField] private float maxIntensity;
    private float minIntensity = 0.0f;

    private float resultIntensity;
    private float lightChangeSpeed = 60.0f;
    [SerializeField] private float sphereCastRadius = 0.1f;


    private float[] blinkTimes = { 0.2f, 0.1f, 0.1f}; // 현재 가장 긴 blinkTime이 처음에 와야 자연스러움
    private float[] blinkStopTimes = { 0.4f, 0.1f, 0.1f};
    private float firstBlinkTime;
    private float combackBlinkTime = 1.0f;

    private bool isLightOn = true;
    private bool isTurning = false;
    private Transform lightTransform; // 손전등 위치 및 방향
    [SerializeField] private float fadeStartDistance; // 감쇠 최소 거리, 해당 거리 이하는 distanceMinIntensity
    [SerializeField] private float fadeMiddleDistance; // 감쇠 중간 거리
    [SerializeField] private float fadeEndDistance;  // 감쇠 최대 거리, 해당 거리 이상은 maxIntensity
    [SerializeField] private float distanceMaxIntensity;
    [SerializeField] private float distanceMinIntensity;

    private Coroutine lightCoroutine;

    private Coroutine turnCoroutine;

    private void Awake(){
        lightTransform = handLight.transform;

        minSpotInnerAngle = maxSpotInnerAngle;
        minSpotOuterAngle = maxSpotOuterAngle;

        firstBlinkTime = blinkTimes[0];
    }

    private void Start(){
    }

    private void Update()
    {
        // Camera 회전이 안되는 경우 대부분 UI 조작이므로 손전등 키고 끄는거 막기
        if(thirdPersonController.CameraRotationLock) return;

        // 좌클릭으로 손전등 On/Off
        if (Input.GetMouseButtonDown(0))
        {
            if (isLightOn)
            {
                isLightOn = false;
                if(turnCoroutine != null){
                    StopCoroutine(turnCoroutine);
                }
                turnCoroutine = StartCoroutine(TurnCoroutine(false));
            }
            else
            {
                isLightOn = true;
                if(turnCoroutine != null){
                    StopCoroutine(turnCoroutine);
                }
                turnCoroutine = StartCoroutine(TurnCoroutine(true));
            }
        }

        // 빛의 강도를 거리 기반으로 조정
        AdjustLightIntensity();

        // 너무 깜빡거리는 문제를 해결하기 위해 fade 효과
        if(resultIntensity - handLight.intensity > 0){
            if(resultIntensity - handLight.intensity > lightChangeSpeed * Time.deltaTime){
                handLight.intensity += lightChangeSpeed * Time.deltaTime;
            }
            else{
                handLight.intensity = resultIntensity;
            }
        }
        else{
            if(handLight.intensity - resultIntensity > lightChangeSpeed * Time.deltaTime){
                handLight.intensity -= lightChangeSpeed * Time.deltaTime;
            }
            else{
                handLight.intensity = resultIntensity;
            }
        }
    }

    public float SeeDistance;
    public string SeeHitObject;
    private void AdjustLightIntensity()
    {
        if (!isLightOn) return;
        if(isTurning) return;

        // 손전등 앞의 가장 가까운 오브젝트와 거리 계산
        Ray ray = new Ray(lightTransform.position, lightTransform.forward);
        if (Physics.SphereCast(ray, sphereCastRadius, out RaycastHit hit, fadeEndDistance))
        {
            float distance = hit.distance;
            SeeDistance = distance;
            SeeHitObject = hit.transform.gameObject.name;

            float intensity;
            // 거리 기반으로 강도 조정
            if(distance < fadeMiddleDistance){
                intensity = Mathf.Lerp(distanceMinIntensity, distanceMaxIntensity, (distance - fadeStartDistance) / (fadeMiddleDistance - fadeStartDistance));
                resultIntensity = Mathf.Clamp(intensity, distanceMinIntensity, distanceMaxIntensity);
            }
            else{
                intensity = Mathf.Lerp(distanceMaxIntensity, maxIntensity, (distance - fadeMiddleDistance) / (fadeEndDistance - fadeMiddleDistance));
                resultIntensity = Mathf.Clamp(intensity, distanceMaxIntensity, maxIntensity);
            }
        }
        else
        {
            // 아무것도 맞지 않았을 때 최대 강도 유지
            resultIntensity = maxIntensity;
        }
    }

    private float GetLightIntensity(){
        float intensity;
        // 손전등 앞의 가장 가까운 오브젝트와 거리 계산
        Ray ray = new Ray(lightTransform.position, lightTransform.forward);
        if (Physics.SphereCast(ray, sphereCastRadius, out RaycastHit hit, fadeEndDistance))
        {
            float distance = hit.distance;

            // 거리 기반으로 강도 조정
            intensity = Mathf.Lerp(distanceMinIntensity, distanceMaxIntensity, (distance - fadeStartDistance) / (fadeEndDistance - fadeStartDistance));
            intensity = Mathf.Clamp(intensity, distanceMinIntensity, distanceMaxIntensity);
        }
        else
        {
            // 아무것도 맞지 않았을 때 최대 강도 유지
            intensity = maxIntensity;
        }
        return intensity;
    }

    public void TurnOnLight(bool isTurnOnLight){
        isLightOn = isTurnOnLight;
        if(turnCoroutine != null){
            StopCoroutine(turnCoroutine);
        }
        turnCoroutine = StartCoroutine(TurnCoroutine(isTurnOnLight));
    }

    IEnumerator TurnCoroutine(bool isLightOn){
        isTurning = true;
        ProgressManager.Instance.isTurnOnLight = isLightOn;
        
        float stepTimer = 0.0f;
        float fadeTime = blinkTimes[0];

        float curSpotInnerAngle = handLight.innerSpotAngle;
        float curSpotOuterAngle = handLight.spotAngle;
        float curIntensity = handLight.intensity;

        float destSpotInnerAngle = isLightOn ? maxSpotInnerAngle : minSpotInnerAngle;
        float destSpotOuterAngle = isLightOn ? maxSpotOuterAngle : minSpotOuterAngle;
        float destIntensity = isLightOn ? GetLightIntensity() : minIntensity;

        while(stepTimer <= fadeTime){
            handLight.innerSpotAngle = Mathf.Lerp(curSpotInnerAngle, destSpotInnerAngle, stepTimer / fadeTime);
            handLight.spotAngle = Mathf.Lerp(curSpotOuterAngle, destSpotOuterAngle, stepTimer / fadeTime);
            resultIntensity = Mathf.Lerp(curIntensity, destIntensity, stepTimer/ fadeTime);

            stepTimer += Time.deltaTime;
            yield return null;
        }
        resultIntensity = destIntensity;

        isTurning = false;
    }

    public void OnSceneChanged(){
        if(ProgressManager.Instance.isTurnOnLight){
            isLightOn = true;
            resultIntensity = GetLightIntensity();
            handLight.intensity = resultIntensity;
        }
        else{
            isLightOn = false;
            resultIntensity = minIntensity;
            handLight.intensity = resultIntensity;
        }
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

        // EffectOffLight에서 Coroutine Stop으로 나오게 해줄 것임
        while(true){
            for( int i = 0; i < blinkTimes.Length; i++){
                stepTimer = 0.0f;

                float curSpotInnerAngle = handLight.innerSpotAngle;
                float curSpotOuterAngle = handLight.spotAngle;
                float curIntensity = handLight.intensity;
                
                // 손전등 꺼지는 효과
                while(stepTimer <= blinkTimes[i]){
                    handLight.innerSpotAngle = Mathf.Lerp(curSpotInnerAngle, minSpotInnerAngle, stepTimer / blinkTimes[i]);
                    handLight.spotAngle = Mathf.Lerp(curSpotOuterAngle, minSpotOuterAngle, stepTimer / blinkTimes[i]);
                    resultIntensity = Mathf.Lerp(curIntensity, minIntensity, stepTimer / blinkTimes[i]);

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
                    resultIntensity = Mathf.Lerp(minIntensity, curMaxIntensity, stepTimer / blinkTimes[i]);

                    stepTimer += Time.deltaTime;
                    yield return null;
                }
                
                yield return new WaitForSeconds(blinkStopTimes[i]);
            }
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
            resultIntensity = Mathf.Lerp(curIntensity, maxIntensity, stepTimer / firstBlinkTime);

            stepTimer += Time.deltaTime;
            yield return null;
        }

    }
}