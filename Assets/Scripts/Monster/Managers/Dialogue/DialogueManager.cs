using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] DialougeUI dialouge_UI;
    public DialougeUI Dialouge_UI { get { return dialouge_UI; } set { dialouge_UI = value; } }

    [SerializeField] List<TextAsset> dialougeList = new List<TextAsset>();
    Dictionary<string, Dialogue> dialogueDic = new Dictionary<string, Dialogue>();

    public bool IsDialogue { get; set; } = false;

    [SerializeField] string keyName;
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

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        if(!IsDialogue)
    //            Dialouge_UI.StartDialouge(keyName, typeSpeed);
    //    }
    //}
}
