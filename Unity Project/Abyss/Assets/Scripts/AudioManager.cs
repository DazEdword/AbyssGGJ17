using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    void Awake()
    {
        Instance = this;
    }

    public AudioListener Listener;
    public AudioSource Source_Music1;
    public AudioSource Source_Music2;
    public AudioSource Source_Sound1;
    public AudioSource Source_Sound2;
    public AudioSource Source_Sound3;


    public List<AudioClip> Clips = new List<AudioClip>();


    List<int> SourceUse = new List<int>() { 1, 2, 3 };

    public void PlayMusic(string Name)
    {
        Source_Music1.clip = GetClipFromName(Name);

        if (Source_Music1.clip != null)
        {
            Source_Music1.Play();
        }
    }

    public void PlayMusicBg(string Name)
    {
        Source_Music2.clip = GetClipFromName(Name);

        if (Source_Music2.clip != null)
        {
            Source_Music2.Play();
        }
    }

    public void PlaySound(string Name)
    {
        AudioClip clip = GetClipFromName(Name);
        if (clip == null)
        {
            Debug.LogError("Not found " + Name);
            return;
        }

        AudioSource source = GetFreeAudioSource();
        source.clip = clip;
        source.Play();

    }

    public void StopMusic()
    {
        Source_Music1.Stop();
        Source_Music2.Stop();
    }

    public void StopAllSound()
    {
        StopMusic();
        Source_Sound1.Stop();
        Source_Sound2.Stop();
        Source_Sound3.Stop();
    }


    AudioClip GetClipFromName(string filename)
    {
        return Clips.FirstOrDefault(x => x.name == filename);
    }

    AudioSource GetFreeAudioSource()
    {
        int f = 0;
        int pos = 0;

        if (!Source_Sound1.isPlaying)
        {
            f = 1;
        }
        else if (!Source_Sound2.isPlaying)
        {
            f = 2;
        }
        if (!Source_Sound3.isPlaying)
        {
            f = 3;
        }
        else
        {
            f = SourceUse.First();
        }

        pos = SourceUse.FindIndex(x => x == f);
        SourceUse.RemoveAt(pos);
        SourceUse.Add(f);

        switch (f)
        {
            case 1:
                return Source_Sound1;
            case 2:
                return Source_Sound2;
            case 3:
                return Source_Sound3;
        }
        return Source_Sound1;
    }
}
