using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        Init();
    }
    public void Init()
    {
        SoundManager.instance.PlaySound("ClassRoomWav", SoundType.Ambience);
        //GameManager.Sound.PlaySound("ClassRoomWav",SoundType.Ambience);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D))
            SoundManager.instance.PlaySound("WoodFootWav");
    }
}
