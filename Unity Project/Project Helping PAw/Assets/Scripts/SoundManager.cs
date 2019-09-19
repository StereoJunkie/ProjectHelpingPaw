using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    [SerializeField] public Sound[] sounds;
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.Name == name);
        if(s != null)
            s.Source.Play();
    }
}
