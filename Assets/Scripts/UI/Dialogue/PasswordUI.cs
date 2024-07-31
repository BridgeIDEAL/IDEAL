using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordUI : MonoBehaviour
{
    [Header("Refer Password Component")]
    [SerializeField] GameObject passwordObject;
    [SerializeField] TextMeshProUGUI indicateText;
    [SerializeField] TMP_InputField passwordField;
    [SerializeField, Tooltip("0 : Yes, 1 : No")] Button[] decideButtons;

    [Header("Instructions")]
    [SerializeField, Tooltip("패스워드를 입력하기 전 안내하는 문구")] string beforeEnterPassword;
    [SerializeField, Tooltip("학교종을 울리는지 확인하는 문구")] string afterEnterPassword; 
    [SerializeField, Tooltip("패스워드가 틀린 경우 다시 안내하는 문구")] string failEnterPassword;

    [Header("임시 비밀번호")]
    public string tempPassword;
    [SerializeField] bool isSolvingPassword = false;
    public bool IsSolvingPassword { get { return isSolvingPassword; } }
    public void Init()
    {
        decideButtons[0].onClick.RemoveAllListeners();
        decideButtons[1].onClick.RemoveAllListeners();
        decideButtons[0].onClick.AddListener(() => { ClickPasswordYesBtn(); });
        decideButtons[1].onClick.AddListener(() => { ClickPasswordNoBtn(); });
        decideButtons[0].gameObject.SetActive(false);
        decideButtons[1].gameObject.SetActive(false);
        passwordField.onValueChanged.AddListener(CheckPasswordLength);
        // To Do ~~ Input Password or Init Password Variable
    }

    #region Set Active State
    // 상호작용하는 컴퓨터 정보를 불러옴
    public void ActivePassword()
    {
        if (isSolvingPassword)
            return;
        if (DialogueManager.Instance.IsTalking)
            return;
        isSolvingPassword = true;
        indicateText.text = beforeEnterPassword;
        passwordObject.SetActive(true);
        passwordField.text = "";
    }

    public void InActivePassword()
    {
        indicateText.text = " ";
        passwordObject.SetActive(false);
        passwordField.text = "";
        isSolvingPassword = false;
    }
    #endregion

    #region Success or Fail Password
    public void SuccessPassword()
    {
        indicateText.text = afterEnterPassword;
        decideButtons[0].onClick.RemoveAllListeners();
        decideButtons[1].onClick.RemoveAllListeners();
        decideButtons[0].onClick.AddListener(() => { ClickRingBellYesBtn(); });
        decideButtons[1].onClick.AddListener(() => { ClickRingBellNoBtn(); });
    }

    public void FailPassword()
    {
        passwordField.text = "";
        indicateText.text = failEnterPassword;
    }
    #endregion

    #region 비밀번호 확인 메서드
    public void CheckPasswordLength(string  _password)
    {
        if (_password.Length == 4)
        {
            decideButtons[0].gameObject.SetActive(true);
            decideButtons[1].gameObject.SetActive(true);
        }
        else
        {
            decideButtons[0].gameObject.SetActive(false);
            decideButtons[1].gameObject.SetActive(false);
        }
    }

    public bool CheckSamePassword(string _password) { return (_password == tempPassword) ? true : false; }
    #endregion

    #region 비밀번호 확인 버튼
    public void ClickPasswordYesBtn()
    {
        if(CheckSamePassword(passwordField.text))
            SuccessPassword();
        else
            FailPassword();
    }

    public void ClickPasswordNoBtn()
    {
        InActivePassword();
    }
    #endregion

    #region 하교종 확인 버튼
    public void ClickRingBellYesBtn()
    {
        //decideButtons[0].onClick.RemoveAllListeners();
        //decideButtons[1].onClick.RemoveAllListeners();
        //decideButtons[0].gameObject.SetActive(false);
        //decideButtons[1].gameObject.SetActive(false);
        InActivePassword();
        EntityDataManager.Instance.EventTriggerController.TriggerLastEvent();
        // To Do ~~ Ring Bell
        // To Do ~~ Inactive Computer
    }

    public void ClickRingBellNoBtn()
    {
        decideButtons[0].onClick.RemoveAllListeners();
        decideButtons[1].onClick.RemoveAllListeners();
        decideButtons[0].onClick.AddListener(() => { ClickPasswordYesBtn(); });
        decideButtons[1].onClick.AddListener(() => { ClickPasswordNoBtn(); });
        decideButtons[0].gameObject.SetActive(false);
        decideButtons[1].gameObject.SetActive(false);
        indicateText.text = failEnterPassword;
        passwordField.text = "";
    }
    #endregion
}
