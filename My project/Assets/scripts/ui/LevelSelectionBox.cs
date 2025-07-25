using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionBox : MonoBehaviour
{
    public int LevelId;
    [SerializeField] ProgressionState progressionState;
    [SerializeField] GameObject SelectedImg;
    [SerializeField] GameObject LockedImage;
    [SerializeField] Image MapImage;
    [SerializeField] MapMenuManager MenuManager;
    public bool Unlocked;
    Color32 BaseColor;

    void Start()
    {
        BaseColor = MapImage.color;
        if(progressionState.UnlockedLevels.Contains(LevelId))
        {
            Unlock();
        }else
        {
            Lock();
        }

        DeselectLevel();
    }

    public void Unlock()
    {
        Unlocked = true;
        LockedImage.SetActive(false);
        MapImage.color = BaseColor;
    }

    public void Lock()
    {
        Unlocked = false;
        LockedImage.SetActive(true);
        MapImage.color = new Color32(255, 255, 255, 115);
    }

    public void SelectLevel()
    {
        if(AudioManager.instance != null)
        {
            AudioManager.instance.PlayId(10);
        }

        if(Unlocked)
        {
            MenuManager.DeselectAll();
            SelectedImg.SetActive(true);
            MenuManager.SelectedLevel = LevelId;
        }
    }

    public void DeselectLevel()
    {
        SelectedImg.SetActive(false);
    }
}
