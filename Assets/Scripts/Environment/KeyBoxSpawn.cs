using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoxSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] classRoomKeys;
    [SerializeField] private Texture[] classRoomKeyTexts;
    [SerializeField] int[] spawnKeyIndexs;
    private List<InteractionGetKeyBundle> classKeyBundle = new List<InteractionGetKeyBundle>();
    private void Awake()
    {
        RandomKeyBundleSpawn();
    }

    public void RandomKeyBundleSpawn()
    {
        int spawnKeyCnt = spawnKeyIndexs.Length;

        for(int idx=0; idx< spawnKeyCnt; idx++)
        {
            int keyIndex = spawnKeyIndexs[idx];
            MeshRenderer meshRenderer = classRoomKeys[keyIndex].GetComponent<MeshRenderer>();
            Material material = new Material(meshRenderer.material);
            material.SetTexture("_BaseMap", classRoomKeyTexts[keyIndex]);
            meshRenderer.material = material;
            InteractionGetKeyBundle keyBundle = classRoomKeys[keyIndex].GetComponent<InteractionGetKeyBundle>();
            classKeyBundle.Add(keyBundle);
            keyBundle.KeyBox_Spawn = this;
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
