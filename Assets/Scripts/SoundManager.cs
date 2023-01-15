using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip MainMusic;
    public AudioClip WinMusic;
    public AudioClip LoseMusic;
    public AudioClip JumpClip;
    public AudioClip KnifeThrow;



    private static SoundManager instance;
    public static SoundManager Instance { get => instance; set => instance = value; }
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        audioSource = GameObject.FindGameObjectWithTag("Audio Source").GetComponent<AudioSource>();
        audioSource.clip= MainMusic;
        audioSource.Play();
    }
}
