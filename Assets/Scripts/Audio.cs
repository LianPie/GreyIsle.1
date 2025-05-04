using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip LevelTrack;
    public AudioClip Jump;
    public AudioClip Splash;
    public AudioClip Gameover;
    public AudioClip Obtained;
    public AudioClip Crank;
    public AudioClip Box;
    public AudioClip EnemyDeath;

    private void Start()
    {
        MusicSource.clip = LevelTrack;
        MusicSource.Play();
    }
    public void SFXplayer(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
