using UnityEngine;
using UnityEngine.Audio;



[System.Serializable]
public class Sound
{
    public int Id;
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;
    public bool Loop;

    [HideInInspector]
    public AudioSource source;

    public Sound()
    {
        volume = 0.5f;
        pitch = 1f;
        Loop = false;
    }
}
