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
    public Dictionary<string, MonsterData.MonsterStat> initMonsterInfoDict { get; private set; } = new Dictionary<string, MonsterData.MonsterStat>();
    public Dictionary<string, MonsterData.MonsterStat> spawnMonsterInfoDict { get; private set; } = new Dictionary<string, MonsterData.MonsterStat>();

    public void Init()
    {
        initMonsterInfoDict = LoadJson<MonsterData.InitMonsterInformation, string, MonsterData.MonsterStat>("initMonsterInformation").MakeDict();
        spawnMonsterInfoDict = LoadJson<MonsterData.SpawnMonsterInformation, string, MonsterData.MonsterStat>("spawnMonsterInformation").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = GameManager.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
