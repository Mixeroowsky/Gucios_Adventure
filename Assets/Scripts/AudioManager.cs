﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    private AudioSource source;
    public AudioMixerGroup audioMixer;
    public bool loop = false;
    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixer;
    }  
    public void Play()
    {
        source.Play();
    }
    internal void Stop()
    {
        source.Stop();
    }
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]
    Sound[] sounds;
    private void Awake()
    {
        if(instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
        PlaySound("music");
    }
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.Log("no sound: " + _name);
    }
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.Log("no sound: " + _name);
    }
}
