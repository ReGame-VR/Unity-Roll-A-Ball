using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{

    public AudioSource[] happySounds;

    public AudioSource badSound;

    public AudioSource[] resetSounds;

    public void PlayHappySound()
    {
        AudioSource sound = happySounds[Random.Range(0, happySounds.Length)];
        sound.PlayOneShot(sound.clip);
    }

    public void PlayResetSound()
    {
        AudioSource sound = resetSounds[Random.Range(0, resetSounds.Length)];
        sound.PlayOneShot(sound.clip);
    }

    public void PlayBadSound()
    {
        badSound.PlayOneShot(badSound.clip);
    }
}
