using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    #region Component & Variable
    [SerializeField] GameObject dialogueBox;
    [SerializeField, Tooltip("0:Name, 1:Text, 2:Btn")] TextMeshProUGUI[] dialogueTexts;
    [SerializeField] Button[] choiceBtns;
    [SerializeField] char eventTriggerStr = '$';
    [SerializeField] char fontTriggerStr = '^';
    [SerializeField] float defaultTypeSpeed = 0.1f;

    [Header("DialogueBox")]
    [SerializeField] Image textBoxImage;
    [SerializeField, Tooltip("0:Light, 1:Dark")] Sprite[] textBoxSprites;

    Dialogue dialogue = new Dialogue();
    
    DialogueEvent dialogueEvent ;
    public DialogueEvent Event { get { if (dialogueEvent == null) dialogueEvent = DialogueManager.Instance.Dialogue_Event; return dialogueEvent; } set { dialogueEvent = value;  } }

    int curDialogueLineIdx = 0;
    float curTypeSpeed = 0.1f;
    bool canSkip = false;

    bool isChooseState = false;
    #endregion

    #region Dialogue System Method
    public bool CanSkip 
    {   
        get { return canSkip; } 
        set 
        { 
            canSkip = value;
            if (canSkip) dialogueTexts[2].gameObject.SetActive(true);
            else dialogueTexts[2].gameObject.SetActive(false);
        } 
    }

    public void StartDialogue(string _key, float _typeSpeed = 0.1f)
    {
        dialogue = DialogueManager.Instance.GetDialogue(_key);
        if (dialogue == null)
            return;
        dialogueBox.SetActive(true);
        curTypeSpeed = _typeSpeed;
        curDialogueLineIdx = 0;
        dialogueTexts[0].text = dialogue.speakerName;
        StartCoroutine(TypeDialogueCor(dialogue.storyLines[curDialogueLineIdx]));
    }

    public IEnumerator TypeDialogueCor(string _dialogueLine)
    {
        // Init
        CanSkip = false;
        dialogueTexts[1].text = "";
        string curDialogueLine = _dialogueLine;
        int dialogueLineLen = curDialogueLine.Length;

        // Check Event
        if (curDialogueLine[0] == eventTriggerStr)
        {
            // Init Check Event Value
            string eventName = "";
            List<string> parameterList = new List<string>();
            string parameter = "";
            bool haveParameter = false;

            for (int i = 1; i < dialogueLineLen - 1; i++)
            {
                // Receive Parameter && Event
                if (haveParameter)
                {
                    if (curDialogueLine[i] == ',')
                    {
                        parameterList.Add(parameter);
                        parameter = "";
                    }
                    else if (curDialogueLine[i] == ')')
                    {
                        haveParameter = false;
                        parameterList.Add(parameter);
                    }
                    else
                    {
                        parameter += curDialogueLine[i];
                    }
                }
                else
                {
                    if (curDialogueLine[i] == '(')
                        haveParameter = true;
                    else
                        eventName += curDialogueLine[i];
                }
            }
            CallDialogueEvent(eventName, parameterList);
            NextDialogue();
            yield break;
        }
        else // Check Dialogue
        {
            // Init Check Dialogue Value
            string type = "";
            bool haveFontTrigger = false;

            for (int i = 0; i < dialogueLineLen; i++)
            {
                if (haveFontTrigger)
                {
                    if (curDialogueLine[i] == fontTriggerStr)
                    {
                        haveFontTrigger = false;
                        dialogueTexts[1].text += type;
                        type = "";
                        yield return new WaitForSeconds(curTypeSpeed);
                    }
                    else
                    {
                        type += curDialogueLine[i];
                    }
                }
                else
                {
                    if (curDialogueLine[i] == fontTriggerStr)
                    {
                        haveFontTrigger = true;
                    }
                    else
                    {
                        dialogueTexts[1].text += curDialogueLine[i];
                        yield return new WaitForSeconds(curTypeSpeed);
                    }
                }
            }
        }
        curDialogueLineIdx += 1;
        CanSkip = true;
    }

    public void EndDialouge()
    {
        if (dialogue.choiceLine.Count == 0)
        {
            curTypeSpeed = defaultTypeSpeed;
            dialogue = null;
            CanSkip = false;
            DialogueManager.Instance.EndDialogue();
            dialogueBox.SetActive(false);
        }
        else
        {
            int choiceCnt = dialogue.choiceLine.Count;

            for (int idx = 0; idx < choiceCnt; idx++)
            {
                isChooseState = true;
                choiceBtns[idx].gameObject.SetActive(true);
                TextMeshProUGUI text = choiceBtns[idx].GetComponentInChildren<TextMeshProUGUI>();
                text.text = dialogue.choiceLine[idx].choiceText;
                int currentIndex = idx;
                choiceBtns[currentIndex].onClick.RemoveAllListeners();
                string nextKeyName = dialogue.storySpeaker+dialogue.choiceLine[currentIndex].nextID;
                choiceBtns[currentIndex].onClick.AddListener(() => ChooseConversation(nextKeyName));
            }
        }
    }

    public void ChooseConversation(string _key)
    {
        int choiceCnt = dialogue.choiceLine.Count;
        for (int idx = 0; idx < choiceCnt; idx++)
        {
            choiceBtns[idx].gameObject.SetActive(false);
        }
        StartDialogue(_key);
        isChooseState = false;
    }

    public void NextDialogue()
    {
        curDialogueLineIdx += 1;
        if (curDialogueLineIdx < dialogue.storyLines.Count)
            StartCoroutine(TypeDialogueCor(dialogue.storyLines[curDialogueLineIdx]));
        else
            EndDialouge();
    }
    #endregion

    #region Dialogue Event
    public void CallDialogueEvent(string _eventName, List<string> _parameterList)
    {
        switch (_eventName)
        {
            // Call Here : Relate Text Effect
            case "TypeSpeed":
                ChangeTypeSpeed(_parameterList);
                break;
            case "Name":
                ChangeSpeakerName(_parameterList);
                break;
            case "UIBox":
                ChangeUI(_parameterList);
                break;
            // Call DialogueEvent Method
            case "Item":
                Event.GetItem(_parameterList);
                break;
            case "Use":
                Event.UseItem(_parameterList);
                break;
            case "Unable":
                Event.UnableCommunicate(_parameterList);
                break;
            case "Index":
                Event.DialogueIndexChange(_parameterList);
                break;
            case "Hurt":
                Event.Damaged(_parameterList);
                break;
            case "AnimationTrigger":
                Event.EntityAnimationTrigger(_parameterList);
                break;
            case "SpawnItem":
                Event.SpawnItem(_parameterList);
                break;
        }
    }

    public void ChangeSpeakerName(List<string> _parameterList)
    {
        dialogueTexts[0].text = _parameterList[0];
    }

    public void ChangeTypeSpeed (List<string> _parameterList)
    {
        float _typeSpeed = float.Parse(_parameterList[0]);
        curTypeSpeed = _typeSpeed;
    }

    public void ChangeUI(List<string> _parameterList)
    {
        switch (_parameterList[0])
        {
            case "Dark":
                textBoxImage.sprite = textBoxSprites[1];
                break;
            case "Light":
                textBoxImage.sprite = textBoxSprites[0];
                break;
        }
    }
    #endregion

    #region Input Dialogue

    public void Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSkip && dialogue != null)
        {
            if (curDialogueLineIdx < dialogue.storyLines.Count)
                StartCoroutine(TypeDialogueCor(dialogue.storyLines[curDialogueLineIdx]));
            else
                EndDialouge();
        }

        if (isChooseState)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                choiceBtns[0].onClick.Invoke();
            else if (Input.GetKeyDown(KeyCode.Alpha2) && choiceBtns[1].gameObject.activeSelf)
                choiceBtns[1].onClick.Invoke();
        }
    }
    #endregion
}
