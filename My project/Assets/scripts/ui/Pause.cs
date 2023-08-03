using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GamePaused;
    public GameObject PauseUI;
    plaerHealth health;


    private void Awake()
    {
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
                else
                {
                    Pauze();
                }
            }else Resume();
        }


    }

   public void Resume()
    {
        Time.timeScale = 1;
        GamePaused = false;
        PauseUI.SetActive(false);
    }

   public void Pauze()
    {
        Time.timeScale = 0;
        GamePaused = true;
        PauseUI.SetActive(true);
    }

    public void quitToMenu()
    {
        Debug.Log("Exiting to menu");
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }

    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

}
