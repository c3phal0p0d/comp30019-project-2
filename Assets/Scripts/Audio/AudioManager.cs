using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;

    void Awake()
    {
        foreach (Sound sound in sounds){
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    void Start(){
        Play("BackgroundMusic");
    }

    public void Play(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();
    }

    public void Stop(string name){
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Stop();
    }
}
