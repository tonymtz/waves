using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    private AudioSource myMusicPlayer;

    private List<AudioSource> effects;

    private bool isMuted;

    private AudioManager()
    {
    }

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject newInstance = new GameObject("AudioManagerObject");
                DontDestroyOnLoad(newInstance);
                _instance = newInstance.AddComponent<AudioManager>();
            }

            return _instance;
        }
    }

    public bool IsMuted
    {
        get
        {
            return isMuted;
        }
    }

    void Awake()
    {
        if (myMusicPlayer == null)
        {
            myMusicPlayer = gameObject.AddComponent<AudioSource>();
            effects = new List<AudioSource>();
        }
    }

    void LateUpdate()
    {
        effects.ForEach(sfx =>
        {
            if (!sfx.isPlaying)
            {
                Destroy(sfx);
                effects.Remove(sfx);
            }
        });
    }

    public void PlayMusic(AudioClip music)
    {
        if (myMusicPlayer.clip == music)
        {
            return;
        }
        myMusicPlayer.Stop();
        myMusicPlayer.clip = music;
        myMusicPlayer.volume = 0.3f;
        myMusicPlayer.Play();
        myMusicPlayer.loop = true;
    }

    public void PlaySound(AudioClip sfx)
    {
        if (isMuted)
        {
            return;
        }

        AudioSource tmpAudioSource = gameObject.AddComponent<AudioSource>();
        tmpAudioSource.clip = sfx;
        tmpAudioSource.Play();

        effects.Add(tmpAudioSource);
    }

    public void OnApplicationQuit()
    {
        AudioManager._instance = null;
    }

    public void ToggleVolumen()
    {
        isMuted = !isMuted;

        if (isMuted)
        {
            myMusicPlayer.volume = 0f;
        }
        else
        {
            myMusicPlayer.volume = 1f;
        }
    }

    public void LowMusicVolume()
    {
        myMusicPlayer.volume = 0f;
        Invoke("RestoreMusicVolume", 2f);
    }

    void RestoreMusicVolume()
    {
        if (!isMuted)
        {
            myMusicPlayer.volume = 1f;
        }
    }
}