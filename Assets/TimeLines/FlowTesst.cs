using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowTesst : MonoBehaviour
{
    [SerializeField] GameObject stand;
    public float timer = 0;
    public int idx = 1;

    private void Awake()
    {
        stand.SetActive(false);
    }
    void Update()
    {
        timer += Time.deltaTime;        
        if(timer>idx)
        {
            Debug.Log(idx);
            idx += 1;
        }    
    }
}
