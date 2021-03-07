using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance { get;  set; }

    private AudioSource ambience;
    private float backgroundVolume = 1f;
    private bool gameplayClip = true;
    [SerializeField]
    private AudioClip menuClip, gameClip;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ambience = GetComponent<AudioSource>();
    }
    private void Update()
    {
        ambience.volume = backgroundVolume;
    }

    public void SetAmbienceVolume(float vol)
    {
        backgroundVolume = vol;
    }

    public void ChangeClip()
    {
        if (gameplayClip)
        {
            ambience.Stop();
            ambience.clip = menuClip;
            ambience.Play();
            gameplayClip = false;
        }
        else if(!gameplayClip)
        {
            ambience.Stop();
            ambience.clip = gameClip;
            ambience.Play();
            gameplayClip = true;
        }
       
    }
}
