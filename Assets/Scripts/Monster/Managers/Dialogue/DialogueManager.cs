using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] DialougeUI dialouge_UI;
    public DialougeUI Dialouge_UI { get { return dialouge_UI; } set { dialouge_UI = value; } }

    [SerializeField] List<TextAsset> dialougeList = new List<TextAsset>();
    Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    public bool IsDialogue { get; set; } = false;

    [SerializeField] int storyID;
    [SerializeField] float typeSpeed;
    private void Awake()
    {
        Singleton();
        LoadDialogueDatas();
    }

    public void Singleton()
    {
        if (Instance == null)
            Instance = this;
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
                if (dialogueDic.ContainsKey(data.dialogues[i].storyID))
                    return;
                dialogueDic.Add(data.dialogues[i].storyID, data.dialogues[i]);
            }
        }
    }

    public Dialogue GetDialogue(int _storyID)
    {
        if (dialogueDic.ContainsKey(_storyID))
            return dialogueDic[_storyID];
        return null;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if(!IsDialogue)
                Dialouge_UI.StartDialouge(storyID, typeSpeed);
        }
    }
}
