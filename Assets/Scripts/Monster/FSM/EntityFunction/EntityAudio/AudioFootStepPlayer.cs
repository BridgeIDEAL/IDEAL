using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFootStepPlayer : AudioSFXPlayer
{
    [SerializeField] AudioClip[] footStepClips;
    [SerializeField] AudioClip[] runStepClips;
    int randNum;
    int lastFootIndex = -1;
    int lastRunIndex = -1;
    int footStepCnt = -1;
    int runStepCnt = -1;
    protected override void Awake()
    {
        base.Awake();
        footStepCnt=footStepClips.Length;
        runStepCnt = runStepClips.Length;
    }

    public void FootStep()
    {
        do
        {
            randNum = Random.Range(0, footStepCnt);
        }
        while (randNum == lastFootIndex);
        lastFootIndex = randNum;
        source.PlayOneShot(footStepClips[lastFootIndex]);
    }

    public void RunStep()
    {
        do
        {
            randNum = Random.Range(0, runStepCnt);
        }
        while (randNum == lastRunIndex);
        lastRunIndex = randNum;
        source.PlayOneShot(runStepClips[lastRunIndex]);
    }
}
