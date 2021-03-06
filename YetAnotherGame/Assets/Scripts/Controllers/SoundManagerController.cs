﻿using System.Collections.Generic;
using UnityEngine;

public class SoundManagerController : MonoBehaviour
{
    private static AudioSource _audioSource;
    private static Dictionary<string,AudioClip> _audioCache = new Dictionary<string, AudioClip>();

    private void Start()
    {
        var soundManager = GameObject.FindGameObjectWithTag("SoundManager");
        _audioSource = soundManager.GetComponent<AudioSource>();
    }

    public static void Play(string soundName)
    {
        AudioClip sound;
        if(!_audioCache.TryGetValue(soundName, out sound))
        {
            sound = ResourceUtils.LoadSound(soundName);
            if(sound == null)
            {
                Debug.Log($"Sound {soundName} not found");
                return;
            }

            _audioCache[soundName] = sound;
        }

        _audioSource.clip = sound;
        _audioSource.Play();
    }
}
