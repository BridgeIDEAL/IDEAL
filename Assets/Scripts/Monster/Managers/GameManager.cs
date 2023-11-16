using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject go;
    public Transform tf;
    #region CoreManagers
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    private FSMManager fsm = new FSMManager();
    public static FSMManager FSM { get { return Instance.fsm; } }
    private EntityEventManager entityevent = new EntityEventManager();
    public static EntityEventManager EntityEvent { get { return Instance.entityevent; } }
    #endregion
    private void Awake()
    {
        if (instance==null)
        {
            GameObject gameManagerObject = GameObject.Find("GameManager");
            instance = gameManagerObject.GetComponent<GameManager>();
            DontDestroyOnLoad(gameManagerObject);
        }
        FSM.Init();
    }

    private void Update()
    {
        FSM.Update();
    }

    public void Clear()
    {

    }
}
