using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSc : MonoBehaviour
{
    public AudioClip[] clips;
    [SerializeField]
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

    public void volumechange(float v)
    {
        
        //float newvolume = Option_Setting_script.settings.volume_audio;
        //AudioSource newaud = GetComponent<AudioSource>();
        aud.volume = v;
    }

    public void stopsound()
    {
        aud.Pause();
    }
    //public void bgmvolumechange()
    //{
    //    aud.volume = Option_Setting_script.settings.volume_bgm;
    //}
}
