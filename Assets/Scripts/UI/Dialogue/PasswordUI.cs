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
    [SerializeField, Tooltip("�н����带 �Է��ϱ� �� �ȳ��ϴ� ����")] string beforeEnterPassword;
    [SerializeField, Tooltip("�б����� �︮���� Ȯ���ϴ� ����")] string afterEnterPassword; 
    [SerializeField, Tooltip("�н����尡 Ʋ�� ��� �ٽ� �ȳ��ϴ� ����")] string failEnterPassword;

    [Header("�ӽ� ��й�ȣ")]
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
    // ��ȣ�ۿ��ϴ� ��ǻ�� ������ �ҷ���
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

    #region ��й�ȣ Ȯ�� �޼���
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

    #region ��й�ȣ Ȯ�� ��ư
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

    #region �ϱ��� Ȯ�� ��ư
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
