using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}
public class DataManager
{
    public Dictionary<string, MonsterData.MonsterStat> monsterInfoDict { get; private set; } = new Dictionary<string, MonsterData.MonsterStat>();

    public void Init()
    {
        monsterInfoDict = LoadJson<MonsterData.MonsterInformation, string, MonsterData.MonsterStat>("monsterInformation").MakeDict();
        Debug.Log(monsterInfoDict.Count);
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = GameManager.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
