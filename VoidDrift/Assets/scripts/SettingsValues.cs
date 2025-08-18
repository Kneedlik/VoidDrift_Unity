using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Values", menuName = "Values")]

[System.Serializable]
public class SettingsValues : ScriptableObject
{
    public bool IsFullScreen; 
    public int VSync;
    public float MasterVolume = 1.0f;
    public float MusicVolume = 1.0f;
}
