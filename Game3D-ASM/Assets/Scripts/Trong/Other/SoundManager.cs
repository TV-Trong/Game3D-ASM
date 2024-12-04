using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public static SoundManager instance;

    public AudioClip backgroundMusic;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
