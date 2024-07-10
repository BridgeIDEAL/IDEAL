using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeUI : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField, Tooltip("0:Name, 1:Text, 2:Btn")] TextMeshProUGUI[] dialogueTexts;
    [SerializeField] Button[] choiceBtns;
    [SerializeField] char eventTriggerStr = '$';
    [SerializeField] char fontTriggerStr = '^';
    
    Dialogue dialogue = new Dialogue();

    int curDialogueLineIdx = 0;
    [SerializeField] float typeSpeed = 0.1f;
    bool canSkip = false;
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

    public void StartDialouge(string _key, float _typeSpeed = 0.1f)
    {
        dialogue = DialogueManager.Instance.GetDialogue(_key);
        if (dialogue == null)
            return;
        dialogueBox.SetActive(true);
        DialogueManager.Instance.IsDialogue = true;
        typeSpeed = _typeSpeed;
        curDialogueLineIdx = 0;
        dialogueTexts[0].text = dialogue.storySpeaker;
        StartCoroutine(TypeDialogueCor(dialogue.storyLines[curDialogueLineIdx], _typeSpeed));
    }

    public IEnumerator TypeDialogueCor(string _dialogueLine, float _typeSpeed)
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
            DialogueEvent(eventName, parameterList);
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
                        yield return new WaitForSeconds(_typeSpeed);
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
                        yield return new WaitForSeconds(_typeSpeed);
                    }
                }
            }
        }
        curDialogueLineIdx += 1;
        CanSkip = true;
    }

    public void EndDialouge()
    {
        dialogueBox.SetActive(false);
        if (dialogue.choiceLine.Count == 0)
        {
            DialogueManager.Instance.IsDialogue = false;
            typeSpeed = 0.1f;
            dialogue = null;
            CanSkip = false;
        }
        else
        {
            int choiceCnt = dialogue.choiceLine.Count;

            for (int idx = 0; idx < choiceCnt; idx++)
            {
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
        StartDialouge(_key);
    }

    public void DialogueEvent(string _eventName, List<string> _parameterList)
    {
        switch (_eventName)
        {
            case "Hurt":
                break;
            case "TestLog":
                TestLog(_parameterList);
                break;
            case "Name":
                ChangeSpeakerName(_parameterList);
                break;
        }
    }

    public void ChangeSpeakerName(List<string> _parameterList)
    {
        dialogueTexts[0].text = _parameterList[0];
    }


    public void TestLog(List<string> _parameterList)
    {
        int cnt = _parameterList.Count;
        string testTex = "";
        for(int i=0; i<cnt; i++)
        {
            testTex += _parameterList[i];
        }
        Debug.Log(testTex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canSkip && dialogue != null)
        {
            if (curDialogueLineIdx < dialogue.storyLines.Count)
                StartCoroutine(TypeDialogueCor(dialogue.storyLines[curDialogueLineIdx], typeSpeed));
            else
                EndDialouge();
        }
    }

    public void NextDialogue()
    {
        curDialogueLineIdx += 1;
        if (curDialogueLineIdx < dialogue.storyLines.Count)
            StartCoroutine(TypeDialogueCor(dialogue.storyLines[curDialogueLineIdx], typeSpeed));
        else
            EndDialouge();
    }
}
