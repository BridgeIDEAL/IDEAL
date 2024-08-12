using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Singleton
    public static DialogueManager Instance;

    public BaseEntity CurrentTalkEntity { get; set; } = null;

    /********************************************************************
                    Dialogue UI & Controller, Event, Data 
      ******************************************************************/
    #region Link Component
    [Header("DialogueController")]
    [SerializeField] DialogueController dialogue_Controller;
    public DialogueController Dialogue_Controller { get { FindDialogueController();  return dialogue_Controller; } }

    [Header("DialogueUI")]
    [SerializeField] DialogueUI dialogue_UI;
    public DialogueUI Dialouge_UI { get { FindDialogueUI(); return dialogue_UI; } }

    [Header("PasswordUI")]
    [SerializeField] PasswordUI password_UI;
    public PasswordUI Password_UI { get { FindPasswordUI(); return password_UI; } }

    [Header("DialogueEvent")]
    [SerializeField] DialogueEvent dialogue_Event;
    public DialogueEvent Dialogue_Event { get { if (dialogue_Event == null) dialogue_Event = GetComponent<DialogueEvent>(); return dialogue_Event; } }

    [Header("DialogueData")]
    [SerializeField] List<TextAsset> dialougeList = new List<TextAsset>();
    Dictionary<string, Dialogue> dialogueDic = new Dictionary<string, Dialogue>();
    #endregion

    // Variable
    [SerializeField] bool isTalking = false;
    public bool IsTalking { get { return isTalking; } set { isTalking = value; } }

    [SerializeField] LayerMask interactionLayer;
    [SerializeField] LayerMask defaultLayer;
    #region Awake Method
    private void Awake()
    {
        Singleton();
        LoadDialogueDatas();
        //Dialouge_UI.Event = Dialogue_Event;
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
    #endregion

    #region Link Component
    public void FindDialogueController()
    {
        if (dialogue_Controller != null)
            return;
        GameObject _controller = GameObject.FindWithTag("DialogueUI");
        dialogue_Controller = _controller.GetComponent<DialogueController>();
    }

    public void FindDialogueUI()
    {
        if(dialogue_UI==null)
            dialogue_UI = Dialogue_Controller.Dialogue;
    }

    public void FindPasswordUI()
    {
        if (password_UI == null)
            password_UI = Dialogue_Controller.Password;
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
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.MoveLock = true;
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.IsDialogueActive = true;
        Dialouge_UI.StartDialogue(_storyKey);
    }

    public void StartDialogue(string _storyKey, BaseEntity _entity)
    {
        if (isTalking)
            return;
        if (Password_UI.IsSolvingPassword)
            return;
        if (_entity.CurrentType == EntityStateType.Quiet)
            return;
        isTalking = true;
        Dialouge_UI.StartDialogue(_storyKey);
        EntityDataManager.Instance.Controller.SendMessage(_entity.gameObject.name, EntityStateType.Talk, EntityStateType.Quiet);
        CurrentTalkEntity = _entity;
        // Lock Player Move & Rotate : Later Delete Annotation
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.MoveLock = true;
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.IsDialogueActive = true;
        // To Do ~~~ : Prevent Active Another UI
        // Prevent Interaction 
        CurrentTalkEntity.gameObject.layer = defaultLayer;
    }

    // Call By DialogueUI
    public void EndDialogue()
    {
        InteractionConditionConversation conversation = DialogueManager.Instance.CurrentTalkEntity.GetComponent<InteractionConditionConversation>();
        if (conversation.canTalk)
            CurrentTalkEntity.gameObject.layer = interactionLayer;
        isTalking = false;
        CurrentTalkEntity = null;
        // Unlock Player Move & Rotate : Later Delete Annotation
        EntityDataManager.Instance.Controller.SendMessage(EntityStateType.Idle);
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.thirdPersonController.MoveLock = false;
        IdealSceneManager.Instance.CurrentGameManager.scriptHub.uIManager.IsDialogueActive = false;
        // To Do ~~~ : Prevent Active Another UI
        // To Do ~~~ : Entity State 
    }
    #endregion
}
