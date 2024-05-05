using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Texture2D BaseCursor;
    [SerializeField] SettingsValues Values;

    public GameObject Settings;
    public GameObject StartMenuObject;

    [SerializeField] Sprite Checked;
    [SerializeField] Sprite UnChecked;

    [SerializeField] Image SettingsButton;
    [SerializeField] Image BackButton;

    [SerializeField] TMP_Text Text;

    [SerializeField] GameObject StartPanel;
    [SerializeField] Volume Volume;

    [SerializeField] List<GameObject> StartButtons = new List<GameObject>();

    private void Start()
    {
        SettingsValues TempValues = SaveManager.LoadSettings();
        if(TempValues != null)
        {
            Values = TempValues;
            switch (Values.MasterVolume)
            {
                case 1f:
                    Text.text = "100%";
                    break;
                case 0f:
                    Text.text = "0%";
                    break;
                case 0.1f:
                    Text.text = "10%";
                    break;
                case 0.25f:
                    Text.text = "25%";
                    break;
                case 0.5f:
                    Text.text = "50%";
                    break;
                case 0.75f:
                    Text.text = "75%";
                    break;
            }
        }

        Vector2 cursorHotSpot = new Vector2(BaseCursor.width / 2, BaseCursor.height / 2);
        Cursor.SetCursor(BaseCursor, cursorHotSpot, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Settings.activeSelf)
            {
                CloseSettings();
            }
        }
    }

    public void startGame()
    {
        StartPanel.SetActive(true);
        Volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);
        depthOfField.active = true;
        StartCoroutine(StartAnim(depthOfField));
        //depthOfField.focusDistance.Override(0.6f);
    }

    IEnumerator StartAnim(DepthOfField depthOfField)
    {
        for (int i = 0; i < StartButtons.Count; i++)
        {
            StartButtons[i].SetActive(false); 
        }

        Image StartPanelImg = StartPanel.GetComponent<Image>();
        float pomBlur = 0.6f;
        depthOfField.focusDistance.Override(3f);
        float pomBlurValue = 2.5f;
        byte pomPanel = 100;
        byte pomPanelValue = 0;
        StartPanelImg.color = new Color32(40,30,40,0);

        while (pomPanelValue < pomPanel || pomBlurValue > pomBlur)
        {
            if (pomPanelValue < pomPanel)
            {
                pomPanelValue += 14;
                StartPanelImg.color = new Color32(40, 30, 40, pomPanelValue);
            }

            if (pomBlurValue >= pomBlur)
            {
                pomBlurValue -= 0.25f;
                depthOfField.focusDistance.Override(pomBlurValue);
            }

            yield return new WaitForSeconds(0.05f);
        }
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

    public void SetVolume()
    {
        switch(Values.MasterVolume)
        {
            case 1f:
                Values.MasterVolume = 0.0f;
                Text.text = "0%";
                break;
            case 0f:
                Values.MasterVolume = 0.1f;
                Text.text = "10%";
                break;
            case 0.1f:
                Values.MasterVolume = 0.25f;
                Text.text = "25%";
                break;
            case 0.25f:
                Values.MasterVolume = 0.5f;
                Text.text = "50%";
                break;
            case 0.5f:
                Values.MasterVolume = 0.75f;
                Text.text = "75%";
                break;
            case 0.75f:
                Values.MasterVolume = 1f;
                Text.text = "100%";
                break;
        }

        Debug.Log("Volume Change");
    }

    public void CloseSettings()
    {
        SaveManager.SaveSettings(Values);
        BackButton.color = new Color32(255, 0, 33, 255);
        StartMenuObject.SetActive(true );
        Settings.SetActive(false);
    }
   
}
