using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
//using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Texture2D BaseCursor;
    [SerializeField] SettingsValues Values;

    public GameObject Settings;
    public GameObject StartMenuObject;

    public GameObject FullScreenCheckBox;
    [SerializeField] Sprite Checked;
    [SerializeField] Sprite UnChecked;

    [SerializeField] Image SettingsButton;
    [SerializeField] Image BackButton;

    [SerializeField] GameObject StartPanel;
    [SerializeField] Volume Volume;

    private void Start()
    {
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
        //depthOfField.focusDistance.Override(1);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator StartAnim(DepthOfField depthOfField)
    {
        Image StartPanelImg = StartPanel.GetComponent<Image>();
        float pomBlur = depthOfField.focusDistance.value;
        depthOfField.focusDistance.Override(3f);
        float pomBlurValue = 0;
        byte pomPanel = 140;
        byte pomPanelValue = 0;
        StartPanelImg.color = new Color32(40,30,40,0);

        while (pomPanelValue < pomPanel || pomBlurValue > pomBlur)
        {
            pomPanelValue += 16;
            StartPanelImg.color = new Color32(40, 30, 40, pomPanelValue);
            Debug.Log(pomPanelValue);

            pomBlurValue -= 0.01f;
            //depthOfField.focusDistance.Override(pomBlurValue);

            yield return new WaitForSeconds(0.05f);
        }   
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
