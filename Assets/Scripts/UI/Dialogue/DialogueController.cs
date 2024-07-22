using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    #region UI
    [Header("Dialogue UI")]
    [SerializeField] DialogueUI dialogueUI;
    public DialogueUI Dialogue { get { LinkDialogue(); return dialogueUI; } set { dialogueUI = value; } }

    [Header("Password UI")]
    [SerializeField] PasswordUI passwordUI;
    public PasswordUI Password { get { LinkPassword(); return passwordUI; } set { passwordUI = value; } }
    #endregion

    #region Life Cycle
    private void Awake()
    {
        Password.Init();
    }

    private void Update()
    {
        Dialogue.Execute();
    }
    #endregion

    #region Link UI
    public void LinkDialogue()
    {
        if (dialogueUI == null)
            dialogueUI = GetComponentInChildren<DialogueUI>();
    }

    public void LinkPassword()
    {
        if (passwordUI == null)
            passwordUI = GetComponentInChildren<PasswordUI>();
    }
    #endregion
}
