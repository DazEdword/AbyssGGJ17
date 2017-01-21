using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    void Awake()
    {
        Instance = this;
    }



    public AudioSource BGMusicSource;
    public AudioListener Listener;
}
