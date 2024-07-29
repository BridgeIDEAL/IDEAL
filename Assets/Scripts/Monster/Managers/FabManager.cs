using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabManager : MonoBehaviour
{
    public static FabManager Instance;

    [SerializeField] GameObject[] prefabs;
    Dictionary<string, GameObject> fabsDictionary = new Dictionary<string, GameObject>();

    [SerializeField] InteractionItemData[] interactionItemDatas;
    Dictionary<string, InteractionItemData> interactionItemDatasDic = new Dictionary<string, InteractionItemData>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        #region Link Data To Dictionary
        int fabsCnt = prefabs.Length;
        for(int i=0; i<fabsCnt; i++)
        {
            fabsDictionary.Add(prefabs[i].name, prefabs[i]);
        }

        int itemCnt = interactionItemDatas.Length;
        for(int i=0; i<itemCnt; i++)
        {
            interactionItemDatasDic.Add(interactionItemDatas[i].name, interactionItemDatas[i]);
        }
        #endregion
    }

    #region Load GameObject
    public GameObject LoadPrefab(string _name)
    {
        if(IsInDictionary(_name))
            return fabsDictionary[_name];
        GameObject go = Resources.Load<GameObject>($"LoadPrefabs/{_name}");
        if (go == null)
            return null;
        else
            return go;
    }

    public bool IsInDictionary(string _name)
    {
        if (fabsDictionary.ContainsKey(_name))
            return true;
        return false;
    }
    #endregion

    #region Load Interaction Item Data
    public InteractionItemData LoadInteractionItemData(string _name)
    {
        if (IsInItemDataDictionary(_name))
            return interactionItemDatasDic[_name];
    
        InteractionItemData _interactionData = Resources.Load<InteractionItemData>($"DialogueItemData/{_name}");
        if (_interactionData == null)
            return null;
        else
            return _interactionData;
    }

    public bool IsInItemDataDictionary(string _name)
    {
        if (interactionItemDatasDic.ContainsKey(_name))
            return true;
        return false;
    }
    #endregion
}
