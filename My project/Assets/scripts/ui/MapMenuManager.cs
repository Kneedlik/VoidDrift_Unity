using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapMenuManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    public List<LevelSelectionBox> LevelBoxes = new List<LevelSelectionBox>();
    public int SelectedLevel = 0;

    private void Start()
    {
        SelectedLevel = 0;
    }

    public void StartLevel()
    {
        if(inputField.text == "")
        {
            return;
        }

        int LevelId = SelectedLevel;
        if(LevelId == 1)
        {
            SceneManager.LoadScene(3);
        }
        else if(LevelId == 2)
        {
            SceneManager.LoadScene(4);
        }
        else if(LevelId == 3)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void ExitToPlayerMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void DeselectAll()
    {
        for(int i = 0; i < LevelBoxes.Count; i++)
        {
            SelectedLevel = 0;
            LevelBoxes[i].DeselectLevel();
        }
    }

}
