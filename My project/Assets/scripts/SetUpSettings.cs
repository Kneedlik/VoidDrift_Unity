using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpSettings : MonoBehaviour
{
    [SerializeField] SettingsValues Values;

    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

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
