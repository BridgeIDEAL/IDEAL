using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region CoreManagers
    // Singleton 을 더이상 사용하지 않음 씬이 2개이기 때문
    public bool canUpdate = false;

    [Header("Refer To Managers")]
    // Refer To Manager & Hub
    [SerializeField] private VariableHub valueHub;
    public VariableHub ValueHub { get { return valueHub; } }
    [SerializeField] private EntityManager entityM;
    public EntityManager EntityM { get { return entityM; } }
    
    // Non Refer To Manager
    private EntityEventManager entityEM = new EntityEventManager();
    public EntityEventManager EntityEM { get { return entityEM; } }
    
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
    // Equipment
    private EquipmentManager equipmentManager;          // Monobehaviour, Destroy(this.gameobject)를 실행하기 위해
    // Conversation
    private ConversationManager conversationManager;        // Monobehaviour


    #endregion
    private void Awake()
    {
        //entityM.SetUp();
    }

    public void Init(){
        AllocateScripts();
        InitScripts();
        canUpdate = true;
    }

    private void InitScripts(){
        Data.Init();
        // UIManager는 inventory보다 앞서야 오류가 발생하지 않음
        uIManager.Init();
        interactionManager.Init(); 
        interactionDetect.Init();
        equipmentManager.Init();
        conversationManager.Init();
    }


    private void Update()
    {
        if(canUpdate){
            //entityM.GameUpdate();
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
        equipmentManager = scriptHub.equipmentManager;
        conversationManager = scriptHub.conversationManager;
    }
}
