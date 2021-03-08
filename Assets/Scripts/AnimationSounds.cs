using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSounds : MonoBehaviour
{
    AudioSource AnimSoundSrc;
    [SerializeField]
    private AudioClip walkClip, hitClip;

    private void Start()
    {
        AnimSoundSrc = GetComponent<AudioSource>();
    }

    private void WalkSound()
    {
        AudioSource.PlayClipAtPoint(walkClip, transform.position);
    }

    private void HitSound()
    {
        AudioSource.PlayClipAtPoint(hitClip, transform.position);
    }

    private void ChopSound()
    {

    }

    private void MineSound()
    {

    }
}
