using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public static soundManager Instance { get;  set; }

    private AudioSource ambience;
    private float backgroundVolume = 1f;
    //private float selectedVolume;
    private bool gameplayClip = true;
    //[SerializeField]
    public AudioClip menuClip, gameClip;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //selectedVolume = backgroundVolume;
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

    /*public void ChangeClip()
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
       
    }*/
    /*public void PlayGameSound()
    {
        //menu.Stop();
        ambience.Stop();
        ambience.clip = gameClip;
        ambience.Play();
    }
    public void PlayMenuSound()
    {
        ambience.Stop();
        ambience.clip = menuClip;
        ambience.Play();
        //menu.clip = menuClip;
        //menu.Play();
    }*/
    public void PlayGameSoundx(AudioClip menuclip,AudioClip gameclip)
    {
        menuclip = this.menuClip;
        gameclip = this.gameClip;
        //ambience.Stop();
        //ambience.clip = clip;
        //ambience.Play();
        //this.menuClip = clip;
        //this.gameClip = clip;
        if (gameplayClip)
        {
            ambience.Stop();
            ambience.clip = menuclip;
            ambience.Play();
            //ambience.Stop();
            ///ambience.clip = menuClip;
            //ambience.Play();
            gameplayClip = false;
        }
        else if (!gameplayClip)
        {
            ambience.Stop();
            ambience.clip = gameclip;
            ambience.Play();
            gameplayClip = true;
        }
    }

}
