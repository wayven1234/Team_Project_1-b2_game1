using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // �̱��� ����(��ø �ȵǰ� ������)
    public static SoundManager Instance { get; private set; }

    [System.Serializable]

    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume;
        public bool loop = false;

        [HideInInspector]
        public AudioSource source;
    }

    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //  �� ���忡 ���� AudioSource ������Ʈ ����
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }

    // Ÿ ��ũ��Ʈ���� �Ҹ��� ����ϴ� �ڵ�
    // SoundManager.Instance.Play("�����");

    // Ÿ ��ũ��Ʈ���� �Ҹ��� �Ͻ����� �ϴ� �ڵ�
    // SoundManager.Instance.Pause("�����");

    //Pause�� ���� �� �ڸ����� �ٽ� ����
    //stop���� �ٲٸ� ó������ �ٽ� ����
}
