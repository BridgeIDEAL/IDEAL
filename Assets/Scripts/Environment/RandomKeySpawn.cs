using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomKeySpawn : MonoBehaviour
{
    public GameObject[] classRoomKeys;
    private List<int> classKeyListNum= new List<int>();
    private void Awake()
    {
        int cnt = 0;
        while (cnt < 3)
        {
            int randomInt = Random.Range(0, 8);
            if (!classKeyListNum.Contains(randomInt))
            {
                classKeyListNum.Add(randomInt);
                classRoomKeys[randomInt].SetActive(true);
                cnt += 1;
            }
        }
    }
}
