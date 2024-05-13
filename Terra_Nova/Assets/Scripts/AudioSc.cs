using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSc : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource aud;
    public void AudioPlay(int i)
    {
        AudioClip audio = clips[i];
        aud = GetComponent<AudioSource>();
        aud.clip = audio;
        aud.Play();

    }
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
