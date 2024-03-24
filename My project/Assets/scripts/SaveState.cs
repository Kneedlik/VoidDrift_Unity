using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveState : MonoBehaviour
{
    [SerializeField]  TMP_Text text;

    public void SaveExecute()
    {
       // StateItem State = KnedlikLib.CreateUpgradeList()
        
       
        //SaveManager.SaveState(text.text, State, false);
    }

    public void Cancel()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
