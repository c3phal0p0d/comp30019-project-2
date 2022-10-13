using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    public string name;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

