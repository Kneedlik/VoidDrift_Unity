using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapMenuManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    // Start is called before the first frame update

    public void StartLevel()
    {
        if(inputField.text == "")
        {
            return;
        }

        int LevelId = int.Parse(inputField.text);    
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

}
