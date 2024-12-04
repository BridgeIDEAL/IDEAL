using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdealSoundChecker : MonoBehaviour
{
    private AudioSource[] audioSources;

    void Start()
    {
        // 씬 내 모든 AudioSource 컴포넌트를 가져옴
        audioSources = FindObjectsOfType<AudioSource>();
    }

    void Update()
    {
        foreach (AudioSource source in audioSources)
        {
            // 오디오가 재생 중인 경우에만 출력
            if (source.isPlaying)
            {
                Debug.Log($"오디오 소스 {source.gameObject.name}에서 {source.clip.name}가 재생 중입니다.");
            }
        }
    }
}
