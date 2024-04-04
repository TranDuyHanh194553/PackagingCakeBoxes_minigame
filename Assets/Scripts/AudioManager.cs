using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------AudioSource-------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------AudioClip--------")]
    public AudioClip background;//
    public AudioClip click;//
    public AudioClip timeUp;//
    public AudioClip dunk;//
    public AudioClip slide;//
    public AudioClip win;//

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

