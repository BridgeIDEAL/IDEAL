using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region CoreManagers
    // Singleton 을 더이상 사용하지 않음 씬이 2개이기 때문
    public bool canUpdate = false;

    [Header("Refer Controller")]
    [SerializeField] private EntitiesController entities_Controller;
    public EntitiesController Entities_Controller 
    { 
        get 
        {   
            if (entities_Controller == null)
                entities_Controller = FindObjectOfType<EntitiesController>();
            return entities_Controller; 
        } 
    }

    [Header("Refer Managers")]
    // Refer To Manager & Hub
    [SerializeField] private GameEventManager gameEvent_Manager;
    public GameEventManager GameEvent_Manager { get { return gameEvent_Manager; } }
    [SerializeField] private FabManager fab_Manager;
    public FabManager Fab_Manager { get { return fab_Manager; } }
    // Not Use Now
    private DataManager data = new DataManager();
    public DataManager Data { get { return data; } }
    private ResourceManager resource = new ResourceManager();
    public ResourceManager Resource { get { return resource; } }
   
    // ScriptHub
    public ScriptHub scriptHub;
    // UIManager
    private UIManager uIManager;
    // UIRayCaster
    private UIEquipment uIEquipment;

    // Interaction
    private InteractionManager interactionManager;  // Monobehaviour, Destroy(this.gameobject)를 실행하기 위해
    private InteractionDetect interactionDetect;    // Monobehaviour, Coroutine을 실행하기 위해
    // Conversation
    private ConversationManager conversationManager;        // Monobehaviour


    #endregion
    private void Awake()
    {
        //
    }

    public void Init(){
        AllocateScripts();
        InitScripts();
        canUpdate = true;
    }

    private void InitScripts(){
        //Data.Init();
        // UIManager는 inventory보다 앞서야 오류가 발생하지 않음
        uIManager.Init();
        interactionManager.Init(); 
        interactionDetect.Init();
        conversationManager.Init();
    }


    private void Update()
    {
        if(canUpdate){
            interactionDetect.GameUpdate();
            uIManager.GameUpdate();
            uIEquipment.GameUpdate();
        }
        
    }

    public void Clear()
    {

    }

    private void AllocateScripts(){
        uIManager = scriptHub.uIManager;
        uIEquipment = scriptHub.uIEquipment;
        interactionManager = scriptHub.interactionManager;
        interactionDetect = scriptHub.interactionDetect;
        conversationManager = scriptHub.conversationManager;
    }
}
