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
    ThirdPersonController thirdPersonController;

    [SerializeField]
    private Slider[] sliders;

    [SerializeField]
    private TextMeshProUGUI[] sliderTexts;

    private float[] minSize = {0.0f, 0.0f, 0.0f, 0.0f, -30.0f};
    private float[] maxSize = {20.0f, 30.0f, 30.0f, 3.0f, 0.0f};

    

    void Awake(){
        sliders[(int)MoveSettingSlider.MoveSpeed].value = thirdPersonController.MoveSpeed / maxSize[(int)MoveSettingSlider.MoveSpeed];
        sliderTexts[(int)MoveSettingSlider.MoveSpeed].text = thirdPersonController.MoveSpeed.ToString();
        sliders[(int)MoveSettingSlider.MoveSpeed].onValueChanged.AddListener(ChangeMoveSpeedValue);
        
        sliders[(int)MoveSettingSlider.SprintSpeed].value = thirdPersonController.SprintSpeed / maxSize[(int)MoveSettingSlider.SprintSpeed];
        sliderTexts[(int)MoveSettingSlider.SprintSpeed].text = thirdPersonController.SprintSpeed.ToString();
        sliders[(int)MoveSettingSlider.SprintSpeed].onValueChanged.AddListener(ChangeSprintSpeedValue);

        sliders[(int)MoveSettingSlider.SpeedChangeRate].value = thirdPersonController.SpeedChangeRate / maxSize[(int)MoveSettingSlider.SpeedChangeRate];
        sliderTexts[(int)MoveSettingSlider.SpeedChangeRate].text = thirdPersonController.SpeedChangeRate.ToString();
        sliders[(int)MoveSettingSlider.SpeedChangeRate].onValueChanged.AddListener(ChangeSpeedChangeRateValue);

        sliders[(int)MoveSettingSlider.JumpHeight].value = thirdPersonController.JumpHeight / maxSize[(int)MoveSettingSlider.JumpHeight];
        sliderTexts[(int)MoveSettingSlider.JumpHeight].text = thirdPersonController.JumpHeight.ToString();
        sliders[(int)MoveSettingSlider.JumpHeight].onValueChanged.AddListener(ChangeJumpHeightValue);

        sliders[(int)MoveSettingSlider.Gravity].value = thirdPersonController.Gravity / minSize[(int)MoveSettingSlider.Gravity];
        sliderTexts[(int)MoveSettingSlider.Gravity].text = thirdPersonController.Gravity.ToString();
        sliders[(int)MoveSettingSlider.Gravity].onValueChanged.AddListener(ChangeGravityValue);

    }

    private void ChangeMoveSpeedValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.MoveSpeed], maxSize[(int)MoveSettingSlider.MoveSpeed], value);
        thirdPersonController.MoveSpeed = newSize;
        thirdPersonController.DefaultMoveSpeed = newSize;
        sliderTexts[(int)MoveSettingSlider.MoveSpeed].text = thirdPersonController.MoveSpeed.ToString();
    }

    public void UpdateMoveSpeedValueText(){
        sliderTexts[(int)MoveSettingSlider.MoveSpeed].text = thirdPersonController.MoveSpeed.ToString();
    }

    private void ChangeSprintSpeedValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.SprintSpeed], maxSize[(int)MoveSettingSlider.SprintSpeed], value);
        thirdPersonController.SprintSpeed = newSize;
        thirdPersonController.DefaultMoveSpeed = newSize;
        sliderTexts[(int)MoveSettingSlider.SprintSpeed].text = thirdPersonController.SprintSpeed.ToString();
    }

    public void UpdateSprintSpeedValueText(){
        sliderTexts[(int)MoveSettingSlider.SprintSpeed].text = thirdPersonController.SprintSpeed.ToString();
    }

    private void ChangeSpeedChangeRateValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.SpeedChangeRate], maxSize[(int)MoveSettingSlider.SpeedChangeRate], value);
        thirdPersonController.SpeedChangeRate = newSize;
        sliderTexts[(int)MoveSettingSlider.SpeedChangeRate].text = thirdPersonController.SpeedChangeRate.ToString();
    }

    private void ChangeJumpHeightValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.JumpHeight], maxSize[(int)MoveSettingSlider.JumpHeight], value);
        thirdPersonController.JumpHeight = newSize;
        sliderTexts[(int)MoveSettingSlider.JumpHeight].text = thirdPersonController.JumpHeight.ToString();
    }

    private void ChangeGravityValue(float value){
        float newSize = Mathf.Lerp(minSize[(int)MoveSettingSlider.Gravity], maxSize[(int)MoveSettingSlider.Gravity], value);
        thirdPersonController.Gravity = newSize;
        sliderTexts[(int)MoveSettingSlider.Gravity].text = thirdPersonController.Gravity.ToString();
    }

}
