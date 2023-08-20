using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpSettings : MonoBehaviour
{
    [SerializeField] SettingsValues Values;

    private void Awake()
    {
        if(Values != null)
        {
            if(Values.IsFullScreen)
            {
                Screen.fullScreen = Values.IsFullScreen;
            }

            QualitySettings.vSyncCount = Values.VSync;
        }
    }


}
