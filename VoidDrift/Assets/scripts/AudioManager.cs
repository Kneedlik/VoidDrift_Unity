using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //public List<Sound> Sounds = new List<Sound>();
    public AudioLibrary Sounds;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SettingsValues Settings = SaveManager.LoadSettings();

        instance = this;
        foreach (Sound s in Sounds.clipList)
        {
            if (s.clip != null)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;
                if (Settings != null)
                {
                    s.source.volume = s.volume * Settings.MasterVolume;
                }
                else s.source.volume = s.volume;

                s.source.pitch = s.pitch;
                s.source.loop = s.source.loop;
            }
        }

        foreach (Sound s in Sounds.MusicList)
        {
            if (s.clip != null)
            {
                s.source = gameObject.AddComponent<AudioSource>();

                s.source.clip = s.clip;
                if (Settings != null)
                {
                    s.source.volume = s.volume * Settings.MasterVolume;
                }
                else s.source.volume = s.volume;

                s.source.pitch = s.pitch;
                s.source.loop = s.Loop;
            }
        }
    }

    public void PlayName(string name,bool Interupt = false)
    {
        for (int i = 0; i < Sounds.clipList.Count; i++)
        {
            if (Sounds.clipList[i].name == name)
            {
                if(Interupt)
                {
                    Sounds.clipList[i].source.Stop();
                }
                Sounds.clipList[i].source.Play();
                break;
            }
        }
    }

    public void StopName(string name)
    {
        for (int i = 0; i < Sounds.clipList.Count; i++)
        {
            if (Sounds.clipList[i].name == name)
            {
                Sounds.clipList[i].source.Stop();
                break;
            }
        }
    }

    public void PlayId(int Id)
    {
        for (int i = 0; i < Sounds.clipList.Count; i++)
        {
            if (Sounds.clipList[i].Id == Id)
            {
                Sounds.clipList[i].source.Play();
                break;
            }
        }
    }

    public void PlayMusicId(int Id)
    {
        for (int i = 0; i < Sounds.MusicList.Count; i++)
        {
            if (Sounds.MusicList[i].source != null)
            {
                Sounds.MusicList[i].source.Stop();
            }
        }

        for (int i = 0; i < Sounds.MusicList.Count; i++)
        {
            if (Sounds.MusicList[i].Id == Id)
            {
                Sounds.MusicList[i].source.Play();
                break;
            }
        }
    }



}
