using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCabinetSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] cabinets;
    [SerializeField] GameObject textBook;
    
    int cabinetCount = 0;
    private void Awake()
    {
        cabinetCount = cabinets.Length;
        int randomInt = Random.Range(0, cabinetCount);
        RandomCabinetActive rca = cabinets[randomInt].GetComponent<RandomCabinetActive>();
        rca.ActiveInteraction();
        GameObject textBookGo = Instantiate(textBook);
        textBookGo.transform.parent = cabinets[randomInt].transform;
        textBookGo.transform.position = rca.textBookTransform.position;
    }
}
