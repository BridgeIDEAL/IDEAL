using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPassword : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            DialogueManager.Instance.Password_UI.ActivePassword();
    }
}
