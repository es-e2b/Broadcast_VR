using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusciPlayer : MonoBehaviour
{
    private AudioSource music;
    
    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        music.Play();
    }

    public void PauseAudio()
    {
        music.Pause();
    }

    public void StopAudio()
    {
        music.Stop();
    }
}

