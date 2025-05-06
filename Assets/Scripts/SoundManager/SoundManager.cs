using System;
using UnityEngine;

public enum SoundType
{
    SOLDSOUND,
    BOUGHTSOUND,
}

[Serializable]
public class Sound
{
    public SoundType audioType;
    public AudioClip audioClip;
}


public class SoundManager
{
    private AudioSource sfxSource;
    private Sound[] sounds;

    public SoundManager(AudioSource sfxSource, Sound[] sounds)
    {
        this.sfxSource = sfxSource;
        this.sounds = sounds;
    }

    private Sound GetSound(SoundType soundType)
    {
        return Array.Find(sounds, (s) => s.audioType == soundType);
    }

    public void PlaySfx(SoundType soundType)
    {
        Sound newSound = GetSound(soundType);
        sfxSource.clip = newSound.audioClip;
        sfxSource.Play();
    }
}
