using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] SettingsValues Values;

    public GameObject Settings;
    public GameObject StartMenuObject;

    public GameObject FullScreenCheckBox;
    [SerializeField] Sprite Checked;
    [SerializeField] Sprite UnChecked;

    [SerializeField] Image SettingsButton;
    [SerializeField] Image BackButton;

    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ShutDownGame()
    {
        Debug.Log("Exiting");
        Application.Quit();
    }

    public void OpenSettings()
    {
        SettingsButton.color = new Color32(255, 0, 33,255);
        StartMenuObject.SetActive(false);
        Settings.SetActive(true);
    }

    public void FullScreenChange(bool isFullScreen)
    {
        Values.IsFullScreen = isFullScreen;
       Screen.fullScreen = isFullScreen;
       Debug.Log("FullScreen");

    }

    public void VSyncChange(bool isOn)
    {
        if(isOn)
        {
            Values.VSync = 1;
            QualitySettings.vSyncCount = 1;
        }else
        {
            Values.VSync = 0;
            QualitySettings.vSyncCount = 0;
        }
       
       Debug.Log("Vsync");
    }

    public void CloseSettings()
    {
        BackButton.color = new Color32(255, 0, 33, 255);
        StartMenuObject.SetActive(true );
        Settings.SetActive(false);
    }
   
}
