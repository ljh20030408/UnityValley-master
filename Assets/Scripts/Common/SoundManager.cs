using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource audioSource;
    public AudioClip lightOpen;
    public AudioClip ligthClose;
    public AudioClip explode;
    public AudioClip colorWheel;
    public AudioClip carColorChange;
    void Awake()
    {
        Instance = this;
    }

    void Play()
    {
        audioSource.Play();
    }

    public void PlayLightOpen()
    {
        audioSource.clip = lightOpen;
        Play();
    }

    public void PlayLightClose()
    {
        audioSource.clip = ligthClose;
        Play();
    }

    public void PlayExplode()
    {
        audioSource.clip = explode;
        Play();
    }

    public void PlayColorWheelShow()
    {
        audioSource.clip = colorWheel;
        Play();
    }

    public void PlayCarColorChange()
    {
        audioSource.clip = carColorChange;
        Play();
    }
}
