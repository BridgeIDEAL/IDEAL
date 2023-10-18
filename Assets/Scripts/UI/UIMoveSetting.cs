using System.Collections;
using System.Collections.Generic;
using System.Security;
using StarterAssets;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIMoveSetting : MonoBehaviour
{
    enum MoveSettingSlider{
        MoveSpeed = 0,
        SprintSpeed,
        SpeedChangeRate,
        JumpHeight,
        Gravity
    }


    [SerializeField]
    FirstPersonController firstPersonController;

    [SerializeField]
    private Slider[] sliders;

    [SerializeField]
    private TextMeshProUGUI[] sliderTexts;

    private float[] minSize = {0.0f, 0.0f, 0.0f, 0.0f, -30.0f};
    private float[] maxSize = {20.0f, 30.0f, 30.0f, 3.0f, 0.0f};

    

    void Awake(){
        sliders[(int)MoveSettingSlider.MoveSpeed].value = firstPersonController.MoveSpeed / maxSize[(int)MoveSettingSlider.MoveSpeed];
        sliderTexts[(int)MoveSettingSlider.MoveSpeed].text = firstPersonController.MoveSpeed.ToString();
        sliders[(int)MoveSettingSlider.MoveSpeed].onValueChanged.AddListener(ChangeMoveSpeedValue);
        
        sliders[(int)MoveSettingSlider.SprintSpeed].value = firstPersonController.SprintSpeed / maxSize[(int)MoveSettingSlider.SprintSpeed];
        sliderTexts[(int)MoveSettingSlider.SprintSpeed].text = firstPersonController.SprintSpeed.ToString();
        sliders[(int)MoveSettingSlider.SprintSpeed].onValueChanged.AddListener(ChangeSprintSpeedValue);

        sliders[(int)MoveSettingSlider.SpeedChangeRate].value = firstPersonController.SpeedChangeRate / maxSize[(int)MoveSettingSlider.SpeedChangeRate];
        sliderTexts[(int)MoveSettingSlider.SpeedChangeRate].text = firstPersonController.SpeedChangeRate.ToString();
        sliders[(int)MoveSettingSlider.SpeedChangeRate].onValueChanged.AddListener(ChangeSpeedChangeRateValue);

        sliders[(int)MoveSettingSlider.JumpHeight].value = firstPersonController.JumpHeight / maxSize[(int)MoveSettingSlider.JumpHeight];
        sliderTexts[(int)MoveSettingSlider.JumpHeight].text = firstPersonController.JumpHeight.ToString();
        sliders[(int)MoveSettingSlider.JumpHeight].onValueChanged.AddListener(ChangeJumpHeightValue);

        sliders[(int)MoveSettingSlider.Gravity].value = firstPersonController.Gravity / minSize[(int)MoveSettingSlider.Gravity];
        sliderTexts[(int)MoveSettingSlider.Gravity].text = firstPersonController.Gravity.ToString();
        sliders[(int)MoveSettingSlider.Gravity].onValueChanged.AddListener(ChangeGravityValue);

    }

    private void ChangeMoveSpeedValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.MoveSpeed], maxSize[(int)MoveSettingSlider.MoveSpeed], value);
        firstPersonController.MoveSpeed = newSize;
        sliderTexts[(int)MoveSettingSlider.MoveSpeed].text = firstPersonController.MoveSpeed.ToString();
    }

    private void ChangeSprintSpeedValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.SprintSpeed], maxSize[(int)MoveSettingSlider.SprintSpeed], value);
        firstPersonController.SprintSpeed = newSize;
        sliderTexts[(int)MoveSettingSlider.SprintSpeed].text = firstPersonController.SprintSpeed.ToString();
    }

    private void ChangeSpeedChangeRateValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.SpeedChangeRate], maxSize[(int)MoveSettingSlider.SpeedChangeRate], value);
        firstPersonController.SpeedChangeRate = newSize;
        sliderTexts[(int)MoveSettingSlider.SpeedChangeRate].text = firstPersonController.SpeedChangeRate.ToString();
    }

    private void ChangeJumpHeightValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.JumpHeight], maxSize[(int)MoveSettingSlider.JumpHeight], value);
        firstPersonController.JumpHeight = newSize;
        sliderTexts[(int)MoveSettingSlider.JumpHeight].text = firstPersonController.JumpHeight.ToString();
    }

    private void ChangeGravityValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.Gravity], maxSize[(int)MoveSettingSlider.Gravity], value);
        firstPersonController.Gravity = newSize;
        sliderTexts[(int)MoveSettingSlider.Gravity].text = firstPersonController.Gravity.ToString();
    }

}
