using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabManager : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject[] prefabs;
    Dictionary<string, GameObject> fabsDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        int fabsCnt = prefabs.Length;
        for(int i=0; i<fabsCnt; i++)
        {
            fabsDictionary.Add(prefabs[i].name, prefabs[i]);
        }
    }

    public GameObject LoadPrefab(string _name)
    {
        if(IsInDictionary(_name))
        {
            GameObject go = Resources.Load<GameObject>($"LoadPrefabs/{_name}");
            if (go == null)
                return null;
            return go;
        }
        return fabsDictionary[_name];
    }

    public bool IsInDictionary(string _name)
    {
        if (fabsDictionary.ContainsKey(_name))
            return false;
        return true;
    }
}
