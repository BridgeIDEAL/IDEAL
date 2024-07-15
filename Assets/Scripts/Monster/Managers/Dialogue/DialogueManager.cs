using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Singleton
    public static DialogueManager Instance;

    [Header("DialogueData & UI")]
    [SerializeField] DialogueUI dialouge_UI;
    public DialogueUI Dialouge_UI { get { if (dialouge_UI == null) dialouge_UI = GetComponentInChildren<DialogueUI>(); return dialouge_UI; } }

    [SerializeField] List<TextAsset> dialougeList = new List<TextAsset>();
    Dictionary<string, Dialogue> dialogueDic = new Dictionary<string, Dialogue>();

    bool isTalking = false;
    public bool IsTalking { get { return IsTalking; } set { isTalking = value; } }

    private void Awake()
    {
        Singleton();
        LoadDialogueDatas();
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
            DialougeData data = JsonUtility.FromJson<DialougeData>(dialougeList[idx].text);
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
        // 
    }
}
