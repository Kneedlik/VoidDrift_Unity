using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class Pause : MonoBehaviour
{
    public static bool GamePaused;
    public GameObject PauseUI;
    plaerHealth health;
    Volume volume;

    [SerializeField] List<GameObject> DissableObj = new List<GameObject>();


    private void Awake()
    {
        volume = GameObject.FindGameObjectWithTag("GlobalVolume").GetComponent<Volume>();
        health = GameObject.FindWithTag("Player").GetComponent<plaerHealth>();
        GamePaused = false;
        Resume();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (health.health > 0)
            {
                if (GamePaused)
                {
                    Resume();
                }
                else if (Time.timeScale != 0)
                {
                    Pauze();
                }
            }else Resume();
        }


    }

   public void Resume()
   {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        Time.timeScale = 1;
        GamePaused = false;

        Canvas canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;

        volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);
        depthOfField.active = false;

        for (int i = 0; i < DissableObj.Count; i++)
        {
            DissableObj[i].SetActive(true);
        }

        PauseUI.SetActive(false);
    }

   public void Pauze()
    {
        Time.timeScale = 0;
        GamePaused = true;

        Canvas canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        volume.profile.TryGet<DepthOfField>(out DepthOfField depthOfField);
        depthOfField.active = true;

        for (int i = 0; i < DissableObj.Count; i++)
        {
            DissableObj[i].SetActive(false);
        }

        PauseUI.SetActive(true);
    }

    public void quitToMenu()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        Debug.Log("Exiting to menu");
        //SceneManager.LoadScene("StartMenu");
        AchiavementManager.instance.CheckAll(false);
        AchiavementManager.instance.SaveAllToProgress();
        SaveManager.SavePlayerProgress(AchiavementManager.instance.progressionState);
        AudioManager.instance.PlayMusicId(1);
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void restartScene()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

}
