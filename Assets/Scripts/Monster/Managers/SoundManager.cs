using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource[] audioSource = new AudioSource[(int)SoundType.MaxSoundCnt];
    Dictionary<string, AudioClip> audioClipDic= new Dictionary<string,AudioClip>();

    public static SoundManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Init();
    }

    public void Init() // Sound ���ӿ�����Ʈ�� �����ϴ� ������� ����� ���� ������Ʈ���� ����
    {
        GameObject root = GameObject.Find("Sound");
        GameObject camera = GameObject.Find("Main Camera");
        if (root == null)
        {
            root = new GameObject { name = "Sound" };
            Object.DontDestroyOnLoad(root);
            string[] soundNames = System.Enum.GetNames(typeof(SoundType));
            for(int i=0; i<soundNames.Length-1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
                audioSource[(int)SoundType.Ambience].loop = true;
            }
        }
        if (camera != null)
            root.transform.parent = camera.transform;
    }
    public void Clear() // ����� Ŭ���� ��� ��ųʸ��� �ʱ�ȭ
    {
        foreach (AudioSource source in audioSource)
        {
            source.clip = null;
            source.Stop();
        }
        audioClipDic.Clear();
    }

    public void PlaySound(string path, SoundType type = SoundType.Effect, float pitch = 1.0f, float volume = 1.0f) // ���� �÷���
    {
        if (path.Contains("Audio/") == false)
            path = $"Audio/{path}";
        if (type == SoundType.Ambience)
        {
            AudioClip audioClip = IdealSceneManager.Instance.CurrentGameManager.Resource.Load<AudioClip>(path);
            if (audioClip == null)
                return;
            AudioSource source = audioSource[(int)SoundType.Ambience];
            if (source.isPlaying)
                source.Stop();
            source.pitch = pitch;
            source.clip = audioClip;
            source.volume = volume;
            source.Play();
        }
        else
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
                return;
            AudioSource source = audioSource[(int)SoundType.Effect];
            source.pitch = pitch;
            source.PlayOneShot(audioClip);
        }
    }
    AudioClip GetOrAddAudioClip(string path) // ���尡 ��� Ŭ���� ��ȯ, ��ųʸ��� ����
    {
        AudioClip audioClip = null;
        if (audioClipDic.TryGetValue(path, out audioClip) == false)
        {
            audioClip = IdealSceneManager.Instance.CurrentGameManager.Resource.Load<AudioClip>(path);
            audioClipDic.Add(path, audioClip);
        }
        return audioClip;
    }
}
