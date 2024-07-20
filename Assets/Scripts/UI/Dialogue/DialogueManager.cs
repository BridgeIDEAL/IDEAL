using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Singleton
    public static DialogueManager Instance;

    [Header("DialogueUI")]
    [SerializeField] DialogueUI dialogue_UI;
    public DialogueUI Dialouge_UI { get { if (dialogue_UI == null) FindDialogueUI(); return dialogue_UI; } }

    [Header("DialogueEvent")]
    [SerializeField] DialogueEvent dialogue_Event;
    public DialogueEvent Dialogue_Event { get { if (dialogue_Event == null) dialogue_Event = GetComponent<DialogueEvent>(); return dialogue_Event; } }

    [Header("DialogueData")]
    [SerializeField] List<TextAsset> dialougeList = new List<TextAsset>();
    Dictionary<string, Dialogue> dialogueDic = new Dictionary<string, Dialogue>();

    // Variable
    bool isTalking = false;
    public bool IsTalking { get { return IsTalking; } set { isTalking = value; } }

    #region Awake Method
    private void Awake()
    {
        Singleton();
        LoadDialogueDatas();
        Dialouge_UI.Event = Dialogue_Event;
    }

    public void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void LoadDialogueDatas()
    {
        if (dialougeList == null)
            return;
        int dialogueListCnt = dialougeList.Count;
        for(int idx= 0; idx<dialogueListCnt; idx++)
        {
            DialogueData data = JsonUtility.FromJson<DialogueData>(dialougeList[idx].text);
            int dataCnt = data.dialogues.Count;
            for(int i =0; i<dataCnt; i++)
            {
                string key = data.dialogues[i].storySpeaker + data.dialogues[i].storyID;
                if (dialogueDic.ContainsKey(key))
                    return;
                dialogueDic.Add(key, data.dialogues[i]);
            }
        }
    }

    public void FindDialogueUI()
    {
        GameObject _dialogueUI = GameObject.FindWithTag("DialogueUI");
        dialogue_UI = _dialogueUI.GetComponentInChildren<DialogueUI>();
    }

    #endregion

    #region Relate Dialogue Function
    public Dialogue GetDialogue(string _key)
    {
        if (dialogueDic.ContainsKey(_key))
            return dialogueDic[_key];
        return null;
    }

    // Call when you start Dialogue
    public void StartDialogue(string _storyKey)
    {
        if (isTalking)
            return;
        isTalking = true;
        Dialouge_UI.StartDialogue(_storyKey);
    }

    // Call By DialogueUI
    public void EndDialogue()
    {
        isTalking = false;
        // Set DialogueIndex
    }
    #endregion
}
