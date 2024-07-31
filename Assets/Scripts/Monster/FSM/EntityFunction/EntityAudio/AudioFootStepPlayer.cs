using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFootStepPlayer : AudioSFXPlayer
{
    [SerializeField] AudioClip[] footStepClips;
    int randNum;
    int lastIndex = -1;
    int footStepCnt = -1;

    protected override void Awake()
    {
        base.Awake();
        footStepCnt=footStepClips.Length;
    }

    public void FootStep()
    {
        do
        {
            randNum = Random.Range(0, footStepCnt);
        }
        while (randNum == lastIndex);
        lastIndex = randNum;
        source.PlayOneShot(footStepClips[lastIndex]);
    }
}
