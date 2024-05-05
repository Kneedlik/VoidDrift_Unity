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
        SettingsValues Settings = SaveManager.LoadSettings();

        instance = this;
        foreach (Sound s in Sounds.clipList)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            if (Settings != null)
            {
                s.source.volume = s.volume * Settings.MasterVolume;
            }
            else s.source.volume = s.volume;

            s.source.pitch = s.pitch;
        }
    }

    public void PlayName(string name)
    {
        for (int i = 0; i < Sounds.clipList.Count; i++)
        {
            if (Sounds.clipList[i].name == name)
            {
                Sounds.clipList[i].source.Play();
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



}
