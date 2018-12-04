using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script requires the GameObject to have an AudioSource component
//Put all audio files you want to use with this in a folder named "Resources".

[RequireComponent(typeof(AudioSource))]
public class AudioSpeaker : MonoBehaviour
{

    AudioSource AS;
    public float MaxDistance = 60f; //How far can this source be heard?
    [HideInInspector]
    public bool canplay = true;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        AS.maxDistance = MaxDistance;
    }

    public void ResetPlayable()
    { canplay = true; }

    //Play a file with the specified string name using the AudioSource attached
    //Example: AS.AudioPlay("sfx_explosion");
    public void AudioPlay(string audio = null, float volume = 1f)
    {
        if (canplay)
        {
            AS.PlayOneShot((AudioClip)Resources.Load(audio));
            AS.volume = volume;
        }
    }

    //Play random out of a list of audio files, again specified elsewhere
    //Example: AS.AudioPlay(footsteps[random],0.35f);
    public void AudioPlayFromList(string audio = null, int lenght = 1, float volume = 1f)
    {
        if (canplay)
        {
            var random = Random.Range(1, lenght);
            AS.PlayOneShot((AudioClip)Resources.Load(audio + random));
            AS.volume = volume;
        }
    }
}
