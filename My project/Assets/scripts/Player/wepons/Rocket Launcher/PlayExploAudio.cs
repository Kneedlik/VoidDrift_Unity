using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayExploAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(17);
        }
    }
}
