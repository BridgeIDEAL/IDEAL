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
    public Dictionary<int, MonsterData.MonsterStat> monsterInfoDict { get; private set; } = new Dictionary<int, MonsterData.MonsterStat>();

    public void Init()
    {
        monsterInfoDict = LoadJson<MonsterData.MonsterInfo, int, MonsterData.MonsterStat>("monsterInfo").MakeDict();
        Debug.Log(monsterInfoDict.Count);
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = GameManager.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
