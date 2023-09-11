using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public List<Sound> sounds;
    private Dictionary<string, AudioSource> audioSources;

    private void Awake()
    {
        audioSources = new Dictionary<string, AudioSource>();
        sounds = Resources.Load<SoundsSO>("Audio/SoundList").SoundList;
        foreach (Sound sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.volume = sound.volume;
            source.pitch = sound.pitch;
            source.loop = sound.loop;

            audioSources.Add(sound.name, source);
        }
    }

    public void Play(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
        {
            audioSources[soundName].Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }

    public void Stop(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
        {
            audioSources[soundName].Stop();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;
}
