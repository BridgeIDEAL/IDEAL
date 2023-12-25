using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MonsterData
{
    [Serializable]
    public class MonsterStat
    {
        public string monsterName; // =monsterID
        public string monsterType; // A,B,C,D
        public string monsterPrefabName; // =monsterPrefab
        public float monsterSpeed;
        public Vector3 initTransform;
        public Vector3 initRotation;
        public string detectedStr = "";
        public string dialogueName = "";
    }
    public class InitMonsterInformation : ILoader<string, MonsterStat>
    {
        public List<MonsterStat> initMonsterInformation = new List<MonsterStat>();
        public Dictionary<string, MonsterStat> MakeDict()
        {
            Dictionary<string, MonsterStat> dict = new Dictionary<string, MonsterStat>();
            {
                foreach (MonsterStat monsterStat in initMonsterInformation)
                {
                    dict.Add(monsterStat.monsterName, monsterStat);
                }
                return dict;
            }
        }
    }
    public class SpawnMonsterInformation : ILoader<string, MonsterStat>
    {
        public List<MonsterStat> spawnMonsterInformation = new List<MonsterStat>();
        public Dictionary<string, MonsterStat> MakeDict()
        {
            Dictionary<string, MonsterStat> dict = new Dictionary<string, MonsterStat>();
            {
                foreach (MonsterStat monsterStat in spawnMonsterInformation)
                {
                    dict.Add(monsterStat.monsterName, monsterStat);
                }
                return dict;
            }
        }
    }
}
