using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MonsterData
{
    [Serializable]
    public class MonsterStat
    {
        public int monsterID;
        public string name;
        public float speed;
        public Vector3 initTransform;
        public Vector3 initRotation;
    }
    public class MonsterInfo : ILoader<int, MonsterStat>
    {
        public List<MonsterStat> monsterInfo = new List<MonsterStat>();
        public Dictionary<int, MonsterStat> MakeDict()
        {
            Dictionary<int, MonsterStat> dict = new Dictionary<int, MonsterStat>();
            {
                foreach (MonsterStat monsterStat in monsterInfo)
                {
                    dict.Add(monsterStat.monsterID, monsterStat);
                }
                return dict;
            }
        }
    }
}
