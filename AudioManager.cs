using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    AudioClip[] audioClips;
    AudioSource audioSource;
    GameManager gameManager;
    public delegate void AudioFinishDelegate();
    public AudioFinishDelegate audioFinishDelegate;

    public AudioManager(GameManager gameManager_, AudioFinishDelegate audioFinishDelegate_)
    {
        gameManager = gameManager_;
        audioSource = gameManager.gameObject.AddComponent<AudioSource>();
        audioClips = Resources.LoadAll<AudioClip>("Music"); 
        audioFinishDelegate = audioFinishDelegate_;
    }

    public void PlayAudio(string name, float pitch, float volume)
    {
        audioSource.pitch = pitch;

        foreach (AudioClip clip in audioClips)
            if (clip.name == name)
            {
                audioSource.PlayOneShot(clip, volume);
                new QuietTimer(clip.length, 0f, gameManager, () => { audioFinishDelegate(); });
                return;
            }
    }
}
