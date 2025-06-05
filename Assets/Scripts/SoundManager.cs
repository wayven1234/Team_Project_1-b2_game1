using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 싱글톤 패턴(중첩 안되게 막아줌)
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

        //  각 사운드에 대한 AudioSource 컴포넌트 생성
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

    // 타 스크립트에서 소리를 재생하는 코드
    // SoundManager.Instance.Play("사운드명");

    // 타 스크립트에서 소리를 일시정지 하는 코드
    // SoundManager.Instance.Pause("사운드명");

    //Pause는 멈춘 그 자리에서 다시 시작
    //stop으로 바꾸면 처음부터 다시 시작
}
