using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomKeySpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] classRoomKeys;
    [SerializeField] private Texture[] classRoomKeyTexts;
    private List<int> classKeyListNum = new List<int>();
    private List<InteractionGetKeyBundle> classKeyBundle = new List<InteractionGetKeyBundle>();
    private void Awake()
    {
        RandomKeyBundleSpawn();
    }

    public void RandomKeyBundleSpawn()
    {
        int cnt = 0;
        while (cnt < 3)
        {
            int randomInt = Random.Range(0, 8);
            if (!classKeyListNum.Contains(randomInt))
            {
                classKeyListNum.Add(randomInt);
                classRoomKeys[randomInt].SetActive(true);
                // Change Material BaseMap
                MeshRenderer meshRenderer = classRoomKeys[randomInt].GetComponent<MeshRenderer>();
                Material material = new Material(meshRenderer.material);
                material.SetTexture("_BaseMap", classRoomKeyTexts[randomInt]); 
                meshRenderer.material = material;
                InteractionGetKeyBundle keyBundle = classRoomKeys[randomInt].GetComponent<InteractionGetKeyBundle>();
                cnt += 1;
                if (keyBundle == null)
                    continue;
                classKeyBundle.Add(keyBundle);
                keyBundle.RandomKey_Spawn = this;
            }
        }
    }

    public void GetKeyBundle(InteractionGetKeyBundle _currentKeyBundle)
    {
        int cnt = classKeyBundle.Count;
        for(int idx=0; idx<cnt; idx++)
        {
            if (classKeyBundle[idx] == _currentKeyBundle)
                continue;
            classKeyBundle[idx].ActInteractionKeyBundle();
        }
        classKeyBundle.Clear();
    }
}
