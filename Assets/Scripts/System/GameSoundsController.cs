﻿using UnityEngine;

public class GameSoundsController : MonoBehaviour
{
    [SerializeField]
    private AudioClip backgroundMusic;

    [SerializeField]
    private AudioClip highTideSound;

    [SerializeField]
    private AudioClip gameOverSound;

    private AudioManager AM;

    void Awake()
    {
        AM = AudioManager.Instance;
    }

    public void HighTide()
    {
        AM.PlaySound(highTideSound);
    }    

    public void GameOver()
    {
        AM.PlaySound(gameOverSound);
    }

    public void PlayBackground()
    {
        AM.PlayMusic(backgroundMusic);
    }
}
