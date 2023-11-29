using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region CoreManagers
    // �̱���
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    // FSM
    private FSMManager fsm = new FSMManager();
    public static FSMManager FSM { get { return Instance.fsm; } }
    // EntityEvent
    private EntityEventManager entityevent = new EntityEventManager();
    public static EntityEventManager EntityEvent { get { return Instance.entityevent; } }
    //Data
    private DataManager data = new DataManager();
    public static DataManager Data { get { return Instance.data; } }
    //Resource
    private ResourceManager resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance.resource; } }

    // ScriptHub
    [SerializeField] private ScriptHub scriptHub;
    // Interaction
    private InteractionManager interactionManager;  // Monobehaviour, Destroy(this.gameobject)를 실행하기 위해
    private InteractionDetect interactionDetect;    // Monobehaviour, Coroutine을 실행하기 위해
    // Inventory
    private Inventory inventory;        // Monobehaviour, Destroy(this.gameobject)를 실행하기 위해
    // ActivationLog
    private ActivationLogManager activationLogManager;  // Monobehaviour, Destroy(this.gameobject)를 실행하기 위해
    private ActivationLogData activationLogData;        // Monobehaviour
    // HealthPoint
    private HealthPointManager healthPointManager;      // Monobehaviour, Destroy(this.gameobject)를 실행하기 위해
    // Equipment
    private EquipmentManager equipmentManager;          // Monobehaviour, Destroy(this.gameobject)를 실행하기 위해
    // Conversation
    private ConversationManager conversationManager;        // Monobehaviour


    #endregion
    private void Awake()
    {
        if (instance==null)
        {
            GameObject gameManagerObject = GameObject.Find("GameManager");
            instance = gameManagerObject.GetComponent<GameManager>();
            DontDestroyOnLoad(gameManagerObject);
        }
        AllocateScripts();
        InitScripts();
    }

    private void InitScripts(){
        Data.Init();
        FSM.Init();
        inventory.Init(); 
        interactionManager.Init(); 
        interactionDetect.Init();
        activationLogManager.Init();
        activationLogData.Init();
        healthPointManager.Init();
        equipmentManager.Init();
        conversationManager.Init();
    }

    private void Start(){
        inventory.GameStart();
    }

    private void Update()
    {
        FSM.Update();
        interactionDetect.GameUpdate();

    }

    public void Clear()
    {

    }

    private void AllocateScripts(){
        interactionManager = scriptHub.interactionManager;
        interactionDetect = scriptHub.interactionDetect;
        inventory = scriptHub.inventory;
        activationLogManager = scriptHub.activationLogManager;
        activationLogData = scriptHub.activationLogData;
        healthPointManager = scriptHub.healthPointManager;
        equipmentManager = scriptHub.equipmentManager;
        conversationManager = scriptHub.conversationManager;
    }
}
