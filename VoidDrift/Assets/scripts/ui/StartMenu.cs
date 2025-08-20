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
    PlayerActions MyInput;
    [SerializeField] Texture2D BaseCursor;
    [SerializeField] SettingsValues Values;

    public GameObject Settings;
    public GameObject StartMenuObject;

    [SerializeField] Sprite Checked;
    [SerializeField] Sprite UnChecked;

    [SerializeField] Image SettingsButton;
    [SerializeField] Image BackButton;

    [SerializeField] TMP_Text Text;
    [SerializeField] TMP_Text TextMusic;

    [SerializeField] GameObject StartPanel;
    [SerializeField] Volume Volume;

    [SerializeField] List<GameObject> StartButtons = new List<GameObject>();

    [SerializeField] ProgressionState Progress;
    [SerializeField] PlayerPrefs Prefs;

    [SerializeField] Toggle FullScreenToggle;
    [SerializeField] Toggle VSyncToggle;

    private void Awake()
    {
        MyInput = new PlayerActions();
        MyInput.Enable();
    }

    private void Start()
    {
        SettingsValues TempValues = SaveManager.LoadSettings();
        if (TempValues != null)
        {
            Values = TempValues;
        }
            
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

        switch (Values.MusicVolume)
        {
            case 1f:
                TextMusic.text = "100%";
                break;
            case 0f:
                TextMusic.text = "0%";
                break;
            case 0.1f:
                TextMusic.text = "10%";
                break;
            case 0.25f:
                TextMusic.text = "25%";
                break;
            case 0.5f:
                TextMusic.text = "50%";
                break;
            case 0.75f:
                TextMusic.text = "75%";
                break;
        }


        //SaveManager.SavePlayerProgress(Progress);
        ProgressionState TempP = SaveManager.LoadPlayerProgress();
        PlayerPrefs TempPref = SaveManager.LoadPlayerPrefs();

        if (TempPref != null)
        {
            Prefs = TempPref;
        }

        if(TempP != null)
        {
            Debug.Log("LoadingP");
            Progress = TempP;
        }

        //if(Progress == null)
        //{
        //    Progress = new ProgressionState();
        //}

        //if(Prefs == null)
        //{
        //    Prefs = new PlayerPrefs();
        //}

        if(Values.IsFullScreen)
        {
            FullScreenToggle.isOn = true;
        }
        else FullScreenToggle.isOn = false;

        if (Values.VSync == 1)
        {
            VSyncToggle.isOn = true;
        }
        else VSyncToggle.isOn = false;

        //Debug.Log(Progress.UnlockedWeapeons.Count);

        Vector2 cursorHotSpot = new Vector2(BaseCursor.width / 2, BaseCursor.height / 2);
        Cursor.SetCursor(BaseCursor, cursorHotSpot, CursorMode.Auto);

        if (AudioManager.instance.IsMusicPlaying(1) == false)
        {
            AudioManager.instance.PlayMusicId(1);
        }
    }

    private void Update()
    {
        if (MyInput.Gameplay.Pause.triggered && MyInput.Gameplay.Pause.ReadValue<float>() > 0)
        {
            if(Settings.activeSelf)
            {
                CloseSettings();
            }
        }
    }

    public void startGame()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
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

        SaveManager.SavePlayerProgress(Progress);
        SaveManager.SavePlayerPrefs(Prefs);

        Application.Quit();
    }

    public void OpenSettings()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        SettingsButton.color = new Color32(255, 0, 33,255);
        StartMenuObject.SetActive(false);
        Settings.SetActive(true);
    }

    public void FullScreenChange(bool isFullScreen)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        Debug.Log(isFullScreen);
        Values.IsFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
        //Debug.Log("FullScreen");
    }

    public void VSyncChange(bool isOn)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        if (isOn)
        {
            Values.VSync = 1;
            QualitySettings.vSyncCount = 1;
        }else
        {
            Values.VSync = 0;
            QualitySettings.vSyncCount = 0;
        }
       
        Debug.Log(Values.VSync);
    }

    public void SetVolume()
    {
        if (AudioManager.instance != null) 
        {
            AudioManager.instance.PlayId(10);
        }
        
        switch (Values.MasterVolume)
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

        AudioManager.instance.ResetVolume(Values);
        Debug.Log("Volume Change");
    }

    public void SetMusicVolume()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }

        switch (Values.MusicVolume)
        {
            case 1f:
                Values.MusicVolume = 0.0f;
                TextMusic.text = "0%";
                break;
            case 0f:
                Values.MusicVolume = 0.1f;
                TextMusic.text = "10%";
                break;
            case 0.1f:
                Values.MusicVolume = 0.25f;
                TextMusic.text = "25%";
                break;
            case 0.25f:
                Values.MusicVolume = 0.5f;
                TextMusic.text = "50%";
                break;
            case 0.5f:
                Values.MusicVolume = 0.75f;
                TextMusic.text = "75%";
                break;
            case 0.75f:
                Values.MusicVolume = 1f;
                TextMusic.text = "100%";
                break;
        }

        AudioManager.instance.ResetVolume(Values);
        Debug.Log("Volume Change");
    }

    public void CloseSettings()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        SaveManager.SaveSettings(Values);
        BackButton.color = new Color32(255, 0, 33, 255);
        StartMenuObject.SetActive(true );
        Settings.SetActive(false);
    }
   
}
